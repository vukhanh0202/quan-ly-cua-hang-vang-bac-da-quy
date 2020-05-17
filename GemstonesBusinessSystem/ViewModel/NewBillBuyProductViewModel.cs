using GemstonesBusinessSystem.Model;
using GemstonesBusinessSystem.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class NewBillBuyProductViewModel : BaseViewModel
    {
        #region Binding

        private string _TenNCC;
        public string TenNCC { get => _TenNCC; set { _TenNCC = value; OnPropertyChanged(); } }

        private string _SDTNCC;
        public string SDTNCC { get => _SDTNCC; set { _SDTNCC = value; OnPropertyChanged(); } }

        private string _DiaChiNCC;
        public string DiaChiNCC { get => _DiaChiNCC; set { _DiaChiNCC = value; OnPropertyChanged(); } }

        private string _NgayTao;
        public string NgayTao { get => _NgayTao; set { _NgayTao = value; OnPropertyChanged(); } }

        private double _TongThanhTien;
        public double TongThanhTien { get => _TongThanhTien; set { _TongThanhTien = value; OnPropertyChanged(); } }

        private int _TongSoLuong;
        public int TongSoLuong { get => _TongSoLuong; set { _TongSoLuong = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private bool isChanged = false;

        private ChiTietPMHModel _ChiTietHDDaChon;
        public ChiTietPMHModel ChiTietHDDaChon { get => _ChiTietHDDaChon; set { _ChiTietHDDaChon = value; OnPropertyChanged(); } }

        private NHACUNGCAP _NCCDaChon;
        public NHACUNGCAP NCCDaChon { get => _NCCDaChon; set { _NCCDaChon = value; OnPropertyChanged(); } }




        //private PHIEUDICHVU _PhieuDichVuMoi;
        //public PHIEUDICHVU PhieuDichVuMoi { get => _PhieuDichVuMoi; set { _PhieuDichVuMoi = value; OnPropertyChanged(); } }

        //private ChiTietPDVModel _CTHDDaChon;
        //public ChiTietPDVModel CTHDDaChon { get => _CTHDDaChon; set { _CTHDDaChon = value; OnPropertyChanged(); } }

        #endregion

        #region List binding

        private ObservableCollection<SANPHAM> _DSSanPhamDB;
        public ObservableCollection<SANPHAM> DSSanPhamDB { get => _DSSanPhamDB; set { _DSSanPhamDB = value; OnPropertyChanged(); } }

        private ObservableCollection<ChiTietPMHModel> _DSSanPhamDaChon;
        public ObservableCollection<ChiTietPMHModel> DSSanPhamDaChon { get => _DSSanPhamDaChon; set { _DSSanPhamDaChon = value; OnPropertyChanged(); } }

        private IEnumerable<SanPhamModel> _DSSanPham;
        public IEnumerable<SanPhamModel> DSSanPham { get => _DSSanPham; set { _DSSanPham = value; OnPropertyChanged(); } }

        //private ObservableCollection<SanPhamModel> _DSSanPhamCapNhat;
        //public ObservableCollection<SanPhamModel> DSSanPhamCapNhat { get => _DSSanPhamCapNhat; set { _DSSanPhamCapNhat = value; OnPropertyChanged(); } }


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
        public ICommand CapNhatNCCCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        #endregion


        public NewBillBuyProductViewModel()
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

                var SPDaChon = DSSanPham.Where(x => x.MaSanPham.ToString().Equals(p.ToString())).SingleOrDefault();

                if (KTSPTonTai == null)
                {
                    SPDaChon.TongSoLuongTon--;

                    return true;
                }
                else
                {
                //    var SLSPConLai = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaSanPham.ToString().Equals(p.ToString())).SingleOrDefault().TongSoLuongTon;

                //    if (KTSPTonTai.SoLuongBan < SLSPConLai)
                //    {
                //        //KTSPTonTai.SoLuongBan=2;
                //        KTSPTonTai.SoLuongBan++;
                //        KTSPTonTai.SoLuongSPHienTai--;
                //        KTSPTonTai.ThanhTien = ConvertUtils.convertDoubleToMoney(KTSPTonTai.DonGiaSPHienTai * KTSPTonTai.SoLuongBan);
                //        SPDaChon.TongSoLuongTon--;
                //        //listProductTemp.Where(x => x.id.ToString().Equals(p.ToString())).SingleOrDefault().quantity--;
                //        TongSoLuong = TinhTongSoLuong();

                //        TongThanhTien = TinhTongThanhTien();
                //    }
                //    else
                //    {
                //        MessageBox.Show("Số lượng sản phẩm đã vượt quá giới hạn");
                //    }
                //    DSSanPham = DSSanPhamCapNhat;
                    return false;
                }
            }, (p) =>
            {
                var ChiTiet = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaSanPham.ToString().Equals(p.ToString())).SingleOrDefault();

                var CT = new ChiTietPMHModel()
                {
                    MaSanPham = ChiTiet.MaSanPham,
                    SANPHAM = ChiTiet,
                    SoLuongMua = 1,
                    ThanhTien = ChiTiet.DonGiaNhap.Value,
                    SoLuongSPHienTai = ChiTiet.TongSoLuongTon.Value,
                    PhuongThuc = Constant.methodBuyProduct,
                    DonGiaSPHienTai = ChiTiet.DonGiaNhap.Value,
                    DonGiaSPNhapHienTai = ChiTiet.DonGiaNhap.Value
                };
                DSSanPhamDaChon.Add(CT);
                TongSoLuong = TinhTongSoLuong();

                TongThanhTien = TinhTongThanhTien();
            });

            //// Xóa sản phẩm đã chọn khỏi hóa đơn
            //XoaSPTuHDCommand = new RelayCommand<Object>((p) =>
            //{
            //    return true; ;
            //}, (p) =>
            //{
            //    foreach (var item in DSSanPhamCapNhat)
            //    {
            //        var temp = DSSanPhamDaChon.Where(x => x.MaSanPham == item.MaSanPham).Count();
            //        if (temp == 0)
            //        {
            //            item.TongSoLuongTon = (int)DSSanPhamDB.Where(x => x.MaSanPham == item.MaSanPham).SingleOrDefault().TongSoLuongTon;
            //            //listProductTemp.Where(x=>x.id == item.id).SingleOrDefault().quantity = listProductIE.Where(x => x.id == item.id).SingleOrDefault().quantity;
            //        }
            //        TongThanhTien = TinhTongThanhTien();
            //        TongSoLuong = TinhTongSoLuong();

            //    }
            //    DSSanPham = DSSanPhamCapNhat;
            //});

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
                isChanged = true;
            });

            // thực hiện điều chỉnh sau khi chỉnh sửa số lượng trong bill
            ThayDoiSLSPTuHDCommand = new RelayCommand<object>((p) =>
            {
                if (isChanged == true)
                {
                    return true; ;
                }
                return false;
            }, (p) =>
            {
                //int SLSPMuaHienTai = (int)DSSanPhamDaChon.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().SoLuongMua;
                //int SLSPGoc = (int)DSSanPhamDB.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().TongSoLuongTon;
                //double GiaMuaSP = (double)(DSSanPhamDaChon.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().DonGiaSPHienTai);

                if (ChiTietHDDaChon.SoLuongMua <= 0)
                {
                    //DSSanPhamCapNhat.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().TongSoLuongTon = SLSPGoc;
                    //listProductTemp.Where(x => x.id == SelectedDetail.idProduct).SingleOrDefault().quantity = quantityOrigin;
                    DSSanPhamDaChon.Remove(ChiTietHDDaChon);
                }
                else
                {
                    //listProductTemp.Where(x => x.id == SelectedDetail.idProduct).SingleOrDefault().quantity = quantityOrigin - quantityCurrent;
                    DSSanPhamDaChon.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().ThanhTien = ChiTietHDDaChon.SoLuongMua * ChiTietHDDaChon.DonGiaSPHienTai;
                }
                TongSoLuong = TinhTongSoLuong();

                TongThanhTien = TinhTongThanhTien();

                isChanged = false;
            });


            // Giảm sản phẩm trong bill
            GiamSLSPCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    ChiTietHDDaChon.SoLuongMua--;
                    ChiTietHDDaChon.ThanhTien = ChiTietHDDaChon.DonGiaSPHienTai * ChiTietHDDaChon.SoLuongMua;
                    //listProductTemp.Where(x => x.id == SelectedDetail.idProduct).SingleOrDefault().quantity++;
                    TongSoLuong = TinhTongSoLuong();

                    TongThanhTien = TinhTongThanhTien();

                    if (ChiTietHDDaChon.SoLuongMua < 1)
                    {
                        DSSanPhamDaChon.Remove(ChiTietHDDaChon);
                    }

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
                    LoadDSSanPham();
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
                    DSSanPham = DSSanPham.Where(x => x.TenSanPham.ToLower().Contains(TimKiem.ToLower()));

                }
                catch (Exception e) { }
            });


            // Thêm hoặc cập nhật nhà cung cấp 
            CapNhatNCCCommand = new RelayCommand<Window>((items) =>
            {
                return true;
            }, (items) =>
            {
                ChooseProviderWindow chooseProviderWindow = new ChooseProviderWindow();
                chooseProviderWindow.ShowDialog();
                NCCDaChon = (chooseProviderWindow.DataContext as ChooseProviderViewModel).LayNCCHienTai();
                if (NCCDaChon != null)
                {
                    TenNCC = NCCDaChon.TenNhaCungCap;
                    SDTNCC = NCCDaChon.SoDienThoaiNCC;
                    DiaChiNCC = NCCDaChon.DiaChiNCC;
                }
            });

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {

                if (DSSanPhamDaChon.Count() < 1 || NCCDaChon == null)
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin!");
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    PHIEUMUAHANG PhieuMuaHang = new PHIEUMUAHANG()
                    {
                        MaNhaCungCap = NCCDaChon.MaNhaCungCap,
                        NHACUNGCAP = NCCDaChon,
                        NgayLapPhieuMua = DateTime.Now,
                        TongThanhTien = 0,
                        TongSoLuongMua = 0
                        //staff_id = 8 // mặc định, cần chỉnh sửa lại
                    };
                    DataProvider.Ins.DB.PHIEUMUAHANGs.Add(PhieuMuaHang);
                    DataProvider.Ins.DB.SaveChanges();
                    foreach (var SP in DSSanPhamDaChon)
                    {
                        CT_PMH ChiTietPMH = new CT_PMH()
                        {
                            MaSanPham = SP.MaSanPham,
                            SANPHAM = SP.SANPHAM,
                            SoLuongMua = SP.SoLuongMua,
                            ThanhTien = SP.ThanhTien,
                            SoLuongSPHienTai = SP.SANPHAM.TongSoLuongTon + SP.SoLuongMua,
                            PhuongThuc = SP.PhuongThuc,
                            DonGiaSPHienTai = SP.DonGiaSPHienTai
                        };
                        var SanPhamDB = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaSanPham == SP.MaSanPham).FirstOrDefault();

                        ChiTietPMH.MaPhieuMuaHang = PhieuMuaHang.MaPhieuMuaHang;

                        SanPhamDB.TongSoLuongTon = ChiTietPMH.SoLuongSPHienTai;
                        SanPhamDB.DonGiaNhap = ChiTietPMH.DonGiaSPHienTai;
                        SanPhamDB.DonGiaBan = Math.Round(((double)ChiTietPMH.DonGiaSPHienTai + ((double)ChiTietPMH.DonGiaSPHienTai * (double)ChiTietPMH.SANPHAM.LOAISANPHAM.PhanTramLoiNhuan / 100)));


                        PhieuMuaHang.TongThanhTien += ChiTietPMH.ThanhTien;
                        PhieuMuaHang.TongSoLuongMua += ChiTietPMH.SoLuongMua;
                        DataProvider.Ins.DB.CT_PMH.Add(ChiTietPMH);
                        DataProvider.Ins.DB.SaveChanges();


                        BAOCAOTONKHO BaoCaoTonKho = DataProvider.Ins.DB.BAOCAOTONKHOes.Where(x => x.MaSanPham == ChiTietPMH.MaSanPham
                        && x.Thang == ChiTietPMH.PHIEUMUAHANG.NgayLapPhieuMua.Value.Month
                        && x.Nam == ChiTietPMH.PHIEUMUAHANG.NgayLapPhieuMua.Value.Year).FirstOrDefault();
                        if (BaoCaoTonKho == null)
                        {
                            BaoCaoTonKho = new BAOCAOTONKHO()
                            {
                                MaSanPham = ChiTietPMH.MaSanPham,
                                SANPHAM = ChiTietPMH.SANPHAM,
                                TonDau = ChiTietPMH.SoLuongSPHienTai - ChiTietPMH.SoLuongMua,
                                Thang = ChiTietPMH.PHIEUMUAHANG.NgayLapPhieuMua.Value.Month,
                                Nam = ChiTietPMH.PHIEUMUAHANG.NgayLapPhieuMua.Value.Year,
                                TonCuoi = ChiTietPMH.SoLuongSPHienTai - ChiTietPMH.SoLuongMua,
                                SLBanRa = 0,
                                SLMuaVao = 0,
                                MaDVT = ChiTietPMH.SANPHAM.LOAISANPHAM.MaDVT
                            };
                            DataProvider.Ins.DB.BAOCAOTONKHOes.Add(BaoCaoTonKho);
                            DataProvider.Ins.DB.SaveChanges();
                        }

                        BaoCaoTonKho.SLMuaVao += ChiTietPMH.SoLuongMua;
                        BaoCaoTonKho.TonCuoi += ChiTietPMH.SoLuongMua;

                        DataProvider.Ins.DB.SaveChanges();
                    }
                    var NCC = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap == NCCDaChon.MaNhaCungCap).FirstOrDefault();
                    NCC.TongTienBanNCC += PhieuMuaHang.TongThanhTien;
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
            TongThanhTien = 0;
            NCCDaChon = null;

            DSSanPhamDaChon = new ObservableCollection<ChiTietPMHModel>();
            DSSanPham = new ObservableCollection<SanPhamModel>();
        }
        public void LayDSTuDatabase()
        {
            DSSanPhamDB = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(x => x.TrangThaiSanPham == Constant.ACTIVE_STATUS));
        }

        public void LoadDSSanPham()
        {
            DSSanPham = (from SanPham in DSSanPhamDB
                                              select new SanPhamModel()
                                              {
                                                  MaSanPham = SanPham.MaSanPham,
                                                  TenSanPham = ConvertUtils.convertString(SanPham.TenSanPham),
                                                  LOAISANPHAM = SanPham.LOAISANPHAM,
                                                  TenLoaiSanPham =ConvertUtils.convertString(SanPham.LOAISANPHAM.TenLoaiSanPham),
                                                  HinhAnhSP = HandleImage.GetImage(SanPham.HinhAnhSanPham),
                                                  TongSoLuongTon = (int)SanPham.TongSoLuongTon,
                                                  DonGiaNhap = ConvertUtils.convertString(SanPham.DonGiaNhap.ToString()),
                                                  DonGiaBan = ConvertUtils.convertString(ConvertUtils.convertDoubleToMoney(SanPham.DonGiaBan.Value)) + " đ",
                                                  PhanTramLoiNhuan = (double)SanPham.LOAISANPHAM.PhanTramLoiNhuan
                                              });
          
            TongSoLuong = TinhTongSoLuong();
            TongThanhTien = TinhTongThanhTien();
        }

        public double TinhTongThanhTien()
        {
            return (double)DSSanPhamDaChon.Sum(x => x.ThanhTien);
        }

        public int TinhTongSoLuong()
        {
            return DSSanPhamDaChon.Sum(x => x.SoLuongMua);
        }
    }
}
