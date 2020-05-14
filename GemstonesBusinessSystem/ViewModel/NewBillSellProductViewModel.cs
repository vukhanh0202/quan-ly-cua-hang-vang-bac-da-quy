using GemstonesBusinessSystem.Model;
using GemstonesBusinessSystem.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class NewBillSellProductViewModel : BaseViewModel
    {
        #region Binding

        private string _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }

        private string _SDTKhachHang;
        public string SDTKhachHang { get => _SDTKhachHang; set { _SDTKhachHang = value; OnPropertyChanged(); } }

        private string _NgayTao;
        public string NgayTao { get => _NgayTao; set { _NgayTao = value; OnPropertyChanged(); } }

        private string _TongThanhTien;
        public string TongThanhTien { get => _TongThanhTien; set { _TongThanhTien = value; OnPropertyChanged(); } }

        private int _TongSoLuong;
        public int TongSoLuong { get => _TongSoLuong; set { _TongSoLuong = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private bool isChangedQuantity = false;

        private ChiTietPBHModel _ChiTietHDDaChon;
        public ChiTietPBHModel ChiTietHDDaChon { get => _ChiTietHDDaChon; set { _ChiTietHDDaChon = value; OnPropertyChanged(); } }

        private KHACHHANG _KHDaChon;
        public KHACHHANG KHDaChon { get => _KHDaChon; set { _KHDaChon = value; OnPropertyChanged(); } }





        //private PHIEUDICHVU _PhieuDichVuMoi;
        //public PHIEUDICHVU PhieuDichVuMoi { get => _PhieuDichVuMoi; set { _PhieuDichVuMoi = value; OnPropertyChanged(); } }

        //private ChiTietPDVModel _CTHDDaChon;
        //public ChiTietPDVModel CTHDDaChon { get => _CTHDDaChon; set { _CTHDDaChon = value; OnPropertyChanged(); } }

        #endregion

        #region List binding

        private ObservableCollection<SANPHAM> _DSSanPhamDB;
        public ObservableCollection<SANPHAM> DSSanPhamDB { get => _DSSanPhamDB; set { _DSSanPhamDB = value; OnPropertyChanged(); } }

        private ObservableCollection<ChiTietPBHModel> _DSSanPhamDaChon;
        public ObservableCollection<ChiTietPBHModel> DSSanPhamDaChon { get => _DSSanPhamDaChon; set { _DSSanPhamDaChon = value; OnPropertyChanged(); } }

        private IEnumerable<SanPhamModel> _DSSanPham;
        public IEnumerable<SanPhamModel> DSSanPham { get => _DSSanPham; set { _DSSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamModel> _DSSanPhamCapNhat;
        public ObservableCollection<SanPhamModel> DSSanPhamCapNhat { get => _DSSanPhamCapNhat; set { _DSSanPhamCapNhat = value; OnPropertyChanged(); } }


        #endregion

        #region Command
        public ICommand ThemSPVaoHD { get; set; }
        public ICommand XoaSPTuHDCommand { get; set; }
        public ICommand SuaSLSPTuHDCommand { get; set; }
        public ICommand ThayDoiSLSPTuHDCommand { get; set; }
        public ICommand GiamSLSPCommand { get; set; }
        public ICommand TotalPriceChangeCommand { get; set; }
        public ICommand DetailCommand { get; set; }
        public ICommand TimKiemCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand CapNhatKHCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        #endregion


        public NewBillSellProductViewModel()
        {

            #region command
            LoadedWindowCommand = new RelayCommand<Object>((p) =>
            {

                return true;
            }, (p) =>
            {
                KhoiTao();
                LayDSTuDatabase();
                LoadDSSanPham();
                NgayTao = DateTime.Now.Date.ToShortDateString();
            });
            // Thêm sản phẩm vào bill
            ThemSPVaoHD = new RelayCommand<Object>((p) =>
            {
                var KTSPTonTai = DSSanPhamDaChon.Where(x => x.MaSanPham.ToString().Equals(p.ToString())).SingleOrDefault();

                var SPDaChon = DSSanPhamCapNhat.Where(x => x.MaSanPham.ToString().Equals(p.ToString())).SingleOrDefault();

                if (KTSPTonTai == null)
                {
                    SPDaChon.TongSoLuongTon--;
                    DSSanPham = DSSanPhamCapNhat;
                    //listProductTemp.Where(x => x.id.ToString().Equals(p.ToString())).SingleOrDefault().quantity--;
                    return true;
                }
                else
                {
                    var SLSPConLai = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaSanPham.ToString().Equals(p.ToString())).SingleOrDefault().TongSoLuongTon;

                    if (KTSPTonTai.SoLuongBan < SLSPConLai)
                    {
                        //KTSPTonTai.SoLuongBan=2;
                        KTSPTonTai.SoLuongBan++;
                        KTSPTonTai.SoLuongSPHienTai--;
                        KTSPTonTai.ThanhTien =ConvertUtils.convertDoubleToMoney(KTSPTonTai.DonGiaSPHienTai * KTSPTonTai.SoLuongBan);
                        SPDaChon.TongSoLuongTon--;
                        //listProductTemp.Where(x => x.id.ToString().Equals(p.ToString())).SingleOrDefault().quantity--;
                        TongSoLuong = TinhTongSoLuong();

                        TongThanhTien = TinhTongThanhTien();
                    }
                    else
                    {
                        MessageBox.Show("Số lượng sản phẩm đã vượt quá giới hạn");
                    }
                    DSSanPham = DSSanPhamCapNhat;
                    return false;
                }
            }, (p) =>
            {
                var ChiTiet = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaSanPham.ToString().Equals(p.ToString())).SingleOrDefault();
                var CT = new ChiTietPBHModel()
                {
                    MaSanPham = ChiTiet.MaSanPham,
                    SANPHAM = ChiTiet,
                    SoLuongBan = 1,
                    ThanhTien = ConvertUtils.convertDoubleToMoney(ChiTiet.DonGiaBan.Value),
                    SoLuongSPHienTai = ChiTiet.TongSoLuongTon.Value - 1,
                    PhuongThuc = Constant.methodSellProduct,
                    DonGiaSPHienTai = ChiTiet.DonGiaBan.Value,
                    DonGiaSPNhapHienTai = ChiTiet.DonGiaNhap.Value
                };
                DSSanPhamDaChon.Add(CT);
                TongSoLuong = TinhTongSoLuong();

                TongThanhTien = TinhTongThanhTien();
            });

            // Xóa sản phẩm đã chọn khỏi hóa đơn
            XoaSPTuHDCommand = new RelayCommand<Object>((p) =>
            {
                return true; ;
            }, (p) =>
            {
                foreach (var item in DSSanPhamCapNhat)
                {
                    var temp = DSSanPhamDaChon.Where(x => x.MaSanPham == item.MaSanPham).Count();
                    if (temp == 0)
                    {
                        item.TongSoLuongTon = (int)DSSanPhamDB.Where(x => x.MaSanPham == item.MaSanPham).SingleOrDefault().TongSoLuongTon;
                        //listProductTemp.Where(x=>x.id == item.id).SingleOrDefault().quantity = listProductIE.Where(x => x.id == item.id).SingleOrDefault().quantity;
                    }
                    TongThanhTien = TinhTongThanhTien();
                    TongSoLuong = TinhTongSoLuong();

                }
                DSSanPham = DSSanPhamCapNhat;
            });

            // Chỉnh sửa số lượng
            SuaSLSPTuHDCommand = new RelayCommand<object>((p) =>
            {
                if (ChiTietHDDaChon != null)
                {
                    return true; ;
                }
                return false;
            }, (p) =>
            {
                isChangedQuantity = true;
            });

            // thực hiện điều chỉnh sau khi chỉnh sửa số lượng trong bill
            ThayDoiSLSPTuHDCommand = new RelayCommand<object>((p) =>
            {
                if (isChangedQuantity == true)
                {
                    return true; ;
                }
                return false;
            }, (p) =>
            {
                int SLSPBanHientai = (int)DSSanPhamDaChon.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().SoLuongBan;
                int SLSPGoc = (int)DSSanPhamDB.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().TongSoLuongTon;
                double GiaSP = (double)(DSSanPhamDB.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().DonGiaBan);

                if (SLSPBanHientai <= 0)
                {
                    DSSanPhamCapNhat.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().TongSoLuongTon = SLSPGoc;
                    //listProductTemp.Where(x => x.id == SelectedDetail.idProduct).SingleOrDefault().quantity = quantityOrigin;
                    DSSanPhamDaChon.Remove(ChiTietHDDaChon);
                }
                else if (SLSPBanHientai > SLSPGoc) // lớn hơn số lượng sản phẩm gốc -> gán về giá trị lớn nhất
                {
                    DSSanPhamCapNhat.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().TongSoLuongTon = 0;
                    //listProductTemp.Where(x => x.id == SelectedDetail.idProduct).SingleOrDefault().quantity = 0;
                    DSSanPhamDaChon.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().SoLuongBan = SLSPGoc;
                    DSSanPhamDaChon.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().ThanhTien = ConvertUtils.convertDoubleToMoney(SLSPGoc * GiaSP);
                }
                else
                {
                    DSSanPhamCapNhat.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().TongSoLuongTon = SLSPGoc - SLSPBanHientai;
                    //listProductTemp.Where(x => x.id == SelectedDetail.idProduct).SingleOrDefault().quantity = quantityOrigin - quantityCurrent;
                    DSSanPhamDaChon.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().ThanhTien =ConvertUtils.convertDoubleToMoney(SLSPBanHientai * GiaSP);
                }
                TongSoLuong = TinhTongSoLuong();

                TongThanhTien = TinhTongThanhTien();

                isChangedQuantity = false;
                DSSanPham = DSSanPhamCapNhat;
            });


            // Xóa sản phẩm trong bill
            GiamSLSPCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    DSSanPhamDaChon.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().SoLuongBan--;
                    ChiTietHDDaChon.ThanhTien = ConvertUtils.convertDoubleToMoney(ChiTietHDDaChon.DonGiaSPHienTai * ChiTietHDDaChon.SoLuongBan);
                    DSSanPhamCapNhat.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().TongSoLuongTon++;
                    //listProductTemp.Where(x => x.id == SelectedDetail.idProduct).SingleOrDefault().quantity++;
                    TongSoLuong = TinhTongSoLuong();

                    TongThanhTien = TinhTongThanhTien();
                    if (ChiTietHDDaChon.SoLuongBan < 1)
                    {
                        DSSanPhamDaChon.Remove(ChiTietHDDaChon);
                    }

                    DSSanPham = DSSanPhamCapNhat;
                }
                catch (Exception)
                {
                }

            });

            // Chi tiết sản phẩm
            DetailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SANPHAM SanPham = DSSanPhamDB.Where(x => x.MaSanPham == int.Parse(p.ToString())).SingleOrDefault();
                DetailProduct detailProduct = new DetailProduct();
                (detailProduct.DataContext as DetailProductViewModel).LoadSanPham(SanPham);
                detailProduct.ShowDialog();
            });

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) != "" && DSSanPham != null)
                {
                    return true;
                }
                else
                {
                    //listProduct.Clear();
                    //foreach (var item in listProductTemp)
                    //{
                    //    listProduct.Add(item);
                    //}
                    //loadListCustomer();
                    DSSanPham = DSSanPhamCapNhat;

                    return false;
                }
            }, (p) =>
            {
                try
                {
                    //DSSanPham.Clear();
                    //var temp = DSSanPhamCapNhat.Where(x => (x.TenSanPham.ToLower().Contains(TimKiem.ToLower())));
                    ////listProduct = listProduct.Where(x => (x.name.ToLower().Contains(searchText.ToLower()));
                    //foreach (var item in temp)
                    //{
                    //    DSSanPham.Add(item);
                    //}
                    DSSanPham = DSSanPhamCapNhat.Where(x => x.TenSanPham.ToLower().Contains(TimKiem.ToLower()));
              
                }
                catch (Exception e) { }
            });


            // Thêm hoặc cập nhật khách hàng
            CapNhatKHCommand = new RelayCommand<Window>((items) =>
            {
                return true;
            }, (items) =>
            {
                ChooseCustomerWindow chooseCustomerWindow = new ChooseCustomerWindow();
                chooseCustomerWindow.ShowDialog();
                KHDaChon = (chooseCustomerWindow.DataContext as ChooseCustomerViewModel).LayKhachHangHienTai();
                if (KHDaChon != null)
                {
                    TenKhachHang = KHDaChon.TenKhachHang;
                    SDTKhachHang = KHDaChon.SoDienThoaiKH;
                }
            });

            // Thanh toán
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {

                if (DSSanPhamDaChon.Count() < 1 || KHDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    PHIEUBANHANG PhieuBanHang = new PHIEUBANHANG()
                    {
                        MaKhachHang = KHDaChon.MaKhachHang,
                        KHACHHANG = KHDaChon,
                        NgayLapPhieuBan = DateTime.Now,
                        TongThanhTien = 0,
                        TongSoLuongBan = 0
                        //staff_id = 8 // mặc định, cần chỉnh sửa lại
                    };
                    DataProvider.Ins.DB.PHIEUBANHANGs.Add(PhieuBanHang);
                    DataProvider.Ins.DB.SaveChanges();
                    foreach (var SP in DSSanPhamDaChon)
                    {
                        CT_PBH ChiTietPBH = new CT_PBH()
                        {
                            MaSanPham = SP.MaSanPham,
                            SANPHAM = SP.SANPHAM,
                            SoLuongBan = SP.SoLuongBan,
                            ThanhTien = ConvertUtils.convertMoneyToDouble(SP.ThanhTien),
                            SoLuongSPHienTai = SP.SoLuongSPHienTai,
                            PhuongThuc = SP.PhuongThuc,
                            DonGiaSPHienTai = SP.DonGiaSPHienTai,
                            DonGiaNhapSPHienTai = SP.DonGiaSPNhapHienTai
                        };
                        var SanPhamDB = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaSanPham == SP.MaSanPham).FirstOrDefault();
                    
                        ChiTietPBH.SoLuongSPHienTai = SanPhamDB.TongSoLuongTon - SP.SoLuongBan;
                        ChiTietPBH.MaPhieuBanHang = PhieuBanHang.MaPhieuBanHang;
                        SanPhamDB.TongSoLuongTon = ChiTietPBH.SoLuongSPHienTai;
                        PhieuBanHang.TongThanhTien += ChiTietPBH.ThanhTien;
                        PhieuBanHang.TongSoLuongBan += ChiTietPBH.SoLuongBan;
                        DataProvider.Ins.DB.CT_PBH.Add(ChiTietPBH);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    var KH = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == KHDaChon.MaKhachHang).FirstOrDefault();
                    KH.TongTienMuaKH += PhieuBanHang.TongThanhTien;
                    DataProvider.Ins.DB.SaveChanges();
                    
                    MessageBox.Show("Thành công");
                    p.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Thất bại");
                }

            });

            #endregion
        }

        public void KhoiTao()
        {
            TongThanhTien = "0";
            KHDaChon = null;

            DSSanPhamDaChon = new ObservableCollection<ChiTietPBHModel>();
            DSSanPham = new ObservableCollection<SanPhamModel>();
            DSSanPhamCapNhat = new ObservableCollection<SanPhamModel>();
        }
        public void LayDSTuDatabase()
        {
            DSSanPhamDB = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(x => x.TongSoLuongTon > 0 && x.TrangThaiSanPham == Constant.ACTIVE_STATUS));
        }

        public void LoadDSSanPham()
        {
            IEnumerable<SanPhamModel> temp = (from SanPham in DSSanPhamDB
                                              select new SanPhamModel()
                                              {
                                                  MaSanPham = SanPham.MaSanPham,
                                                  TenSanPham = Utils.ConvertUtils.convertString(SanPham.TenSanPham),
                                                  TenLoaiSanPham = Utils.ConvertUtils.convertString(SanPham.LOAISANPHAM.TenLoaiSanPham),
                                                  HinhAnhSP = Utils.HandleImage.GetImage(SanPham.HinhAnhSanPham),
                                                  TongSoLuongTon = (int)SanPham.TongSoLuongTon,
                                                  DonGiaNhap = Utils.ConvertUtils.convertString(SanPham.DonGiaNhap.ToString()),
                                                  DonGiaBan = Utils.ConvertUtils.convertString(ConvertUtils.convertDoubleToMoney(SanPham.DonGiaBan.Value)) + " đ",
                                                  PhanTramLoiNhuan = (double)SanPham.LOAISANPHAM.PhanTramLoiNhuan
                                              });
            try
            {

                foreach (var item in temp)
                {
                    DSSanPhamCapNhat.Add(item);
                }
                DSSanPham = temp;
            }
            catch (Exception)
            {
            }
            TongSoLuong = TinhTongSoLuong();
            TongThanhTien = TinhTongThanhTien();
        }

        public string TinhTongThanhTien()
        {
            double ThanhTienDouble = (double)DSSanPhamDaChon.Sum(x => ConvertUtils.convertMoneyToDouble(x.ThanhTien));
            return ConvertUtils.convertDoubleToMoney(ThanhTienDouble);
        }

        public int TinhTongSoLuong()
        {
            return DSSanPhamDaChon.Sum(x => x.SoLuongBan);
        }
    }
}
