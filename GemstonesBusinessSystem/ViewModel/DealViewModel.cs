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
    class DealViewModel : BaseViewModel
    {
        #region Thuộc tính ẩn hiện grid
        public enum EnumChucNang
        {
            SanPham, KhachHang
        };
        private int _GiaoDich;
        public int GiaoDich { get => _GiaoDich; set { _GiaoDich = value; OnPropertyChanged(); } }
        #endregion
        #region list binding

        private ObservableCollection<SANPHAM> _DSSanPhamDB;
        public ObservableCollection<SANPHAM> DSSanPhamDB { get => _DSSanPhamDB; set { _DSSanPhamDB = value; OnPropertyChanged(); } }

        private ObservableCollection<ChiTietPBHModel> _DSSanPhamDaChon;
        public ObservableCollection<ChiTietPBHModel> DSSanPhamDaChon { get => _DSSanPhamDaChon; set { _DSSanPhamDaChon = value; OnPropertyChanged(); } }

        private IEnumerable<SanPhamModel> _DSSanPham;
        public IEnumerable<SanPhamModel> DSSanPham { get => _DSSanPham; set { _DSSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamModel> _DSSanPhamCapNhat;
        public ObservableCollection<SanPhamModel> DSSanPhamCapNhat { get => _DSSanPhamCapNhat; set { _DSSanPhamCapNhat = value; OnPropertyChanged(); } }

        //private ObservableCollection<SanPhamModel> _listProduct;
        //public ObservableCollection<SanPhamModel> listProduct { get => _listProduct; set { _listProduct = value; OnPropertyChanged(); } }

        //private ObservableCollection<SanPhamModel> _listProductTemp;
        //public ObservableCollection<SanPhamModel> listProductTemp { get => _listProductTemp; set { _listProductTemp = value; OnPropertyChanged(); } }

        private ObservableCollection<KHACHHANG> _DSKhachHangDB;
        public ObservableCollection<KHACHHANG> DSKhachHangDB { get => _DSKhachHangDB; set { _DSKhachHangDB = value; OnPropertyChanged(); } }

        private IEnumerable<KHACHHANG> _DSKhachHang;
        public IEnumerable<KHACHHANG> DSKhachHang { get => _DSKhachHang; set { _DSKhachHang = value; OnPropertyChanged(); } }

        #endregion

        #region thuộc tính binding

        private string _TongTien;
        public string TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

        private string _NgayTao;
        public string NgayTao { get => _NgayTao; set { _NgayTao = value; OnPropertyChanged(); } }

        private ChiTietPBHModel _ChiTietHDDaChon;
        public ChiTietPBHModel ChiTietHDDaChon { get => _ChiTietHDDaChon; set { _ChiTietHDDaChon = value; OnPropertyChanged(); } }

        private string _TienKhachTra;
        public string TienKhachTra { get => _TienKhachTra; set { _TienKhachTra = value; OnPropertyChanged(); } }

        private string _TienThua;
        public string TienThua { get => _TienThua; set { _TienThua = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private string _TieuDe;
        public string TieuDe { get => _TieuDe; set { _TieuDe = value; OnPropertyChanged(); } }

        private KHACHHANG _KHDaChon;
        public KHACHHANG KHDaChon { get => _KHDaChon; set { _KHDaChon = value; OnPropertyChanged(); } }

        private bool isChangedQuantity = false;

        public static int status;
        #endregion

        #region command
        public ICommand ThemSPVaoHD { get; set; }
        public ICommand XoaSPTuHDCommand { get; set; }
        public ICommand SuaSLSPTuHDCommand { get; set; }
        public ICommand ThayDoiSLSPTuHDCommand { get; set; }
        public ICommand GiamSLSPCommand { get; set; }
        public ICommand MoneyGiveChangeCommand { get; set; }
        public ICommand FormatMoneyGiveCommand { get; set; }
        public ICommand TotalPriceChangeCommand { get; set; }
        public ICommand DetailCommand { get; set; }
        public ICommand TimKiemCommand { get; set; }
        public ICommand SanPhamCommand { get; set; }
        public ICommand KhachHangCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand ThemKHCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand ReloadPageCommand { get; set; }
        #endregion

        public DealViewModel()
        {


            #region command
            ReloadPageCommand = new RelayCommand<Object>((p) =>
            {
                if (status == 1)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                TieuDe = "Danh sách sản phẩm"; // Khởi tạo
                KhoiTao();
                LayDSTuDatabase();
                LoadDSSanPham();
                LoadDSKhachHang();
                NgayTao = DateTime.Now.Date.ToShortDateString();
                status = 0;
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
                        KTSPTonTai.ThanhTien = ConvertUtils.convertDoubleToMoney(KTSPTonTai.DonGiaSPHienTai * KTSPTonTai.SoLuongBan);
                        SPDaChon.TongSoLuongTon--;
                        //listProductTemp.Where(x => x.id.ToString().Equals(p.ToString())).SingleOrDefault().quantity--;
                        TongTien = TinhTongThanhTien();

                        TinhTienTraLai();
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
                };
                DSSanPhamDaChon.Add(CT);
                TongTien = TinhTongThanhTien();
                TinhTienTraLai();

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
                    TongTien = TinhTongThanhTien();

                    TinhTienTraLai();
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
                    DSSanPhamDaChon.Where(x => x.MaSanPham == ChiTietHDDaChon.MaSanPham).SingleOrDefault().ThanhTien = ConvertUtils.convertDoubleToMoney(SLSPBanHientai * GiaSP);
                }

                TongTien = TinhTongThanhTien();
                TinhTienTraLai();

                isChangedQuantity = false;
                DSSanPham = DSSanPhamCapNhat;
            });


            // nhập số tiền khách đưa
            MoneyGiveChangeCommand = new RelayCommand<Object>((p) =>
            {

                return true;
            }, (p) =>
            {
                TinhTienTraLai();
            });

            // Format tiền khách đưa
            FormatMoneyGiveCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    var temp = ConvertUtils.convertMoneyToDouble(TienKhachTra);
                    TienKhachTra = ConvertUtils.convertDoubleToMoney(temp);
                }
                catch (Exception) { }
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
                    TongTien = TinhTongThanhTien();
                    if (ChiTietHDDaChon.SoLuongBan < 1)
                    {
                        DSSanPhamDaChon.Remove(ChiTietHDDaChon);
                    }
                    TinhTienTraLai();

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

                    LoadDSKhachHang();
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

                    DSKhachHang = DSKhachHangDB.Where(x => (x.MaKhachHang.ToString().Contains(TimKiem.ToLower())
                                                        || x.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.SoDienThoaiKH.ToLower().Contains(TimKiem.ToLower())
                                                        || x.DiaChiKH.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongTienMuaKH.ToString().ToLower().Contains(TimKiem.ToLower())));
                }
                catch (Exception e) { }
            });

            // Thêm khách hàng
            ThemKHCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                NewCustomerWindow newCustomer = new NewCustomerWindow();
                newCustomer.ShowDialog();
                LayDSTuDatabase();
                LoadDSKhachHang();
            });

            // Thanh toán
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {

                if (DSSanPhamDaChon.Count() < 1 || KHDaChon == null || (double.Parse(TienThua.Replace(".", ""))) < 0)
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
                            DonGiaSPHienTai = SP.DonGiaSPHienTai
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
                    // Tải lại trang
                    KhoiTao();
                    LayDSTuDatabase();
                    LoadDSSanPham();
                    LoadDSKhachHang();

                    MessageBox.Show("Thanh toán thành công");
                }
                catch (Exception)
                {
                    MessageBox.Show("Thanh toán thất bại");
                }

            });


            // Tải lại trang
            ReloadCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                KhoiTao();
                LayDSTuDatabase();
                LoadDSSanPham();
                LoadDSKhachHang();
            });

            #endregion

            #region command ẩn hiện 
            SanPhamCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                GiaoDich = (int)EnumChucNang.SanPham;
                TieuDe = "Danh sách sản phẩm";
            });

            KhachHangCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                GiaoDich = (int)EnumChucNang.KhachHang;
                TieuDe = "Danh sách khách hàng";
            });
            #endregion
        }

        public void KhoiTao()
        {
            TienKhachTra = "";
            TongTien = "0";

            DSSanPhamDaChon = new ObservableCollection<ChiTietPBHModel>();
            DSSanPham = new ObservableCollection<SanPhamModel>();
            DSSanPhamCapNhat = new ObservableCollection<SanPhamModel>();
            //listProduct = new ObservableCollection<SanPhamModel>();
            //listProductTemp = new ObservableCollection<SanPhamModel>();
        }
        public void LayDSTuDatabase()
        {
            DSSanPhamDB = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(x => x.TongSoLuongTon > 0 && x.TrangThaiSanPham == Constant.ACTIVE_STATUS));
            DSKhachHangDB = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.TrangThaiKH == Constant.ACTIVE_STATUS));
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
                //foreach (var item in temp)
                //{
                //    DSSanPham.Add(item);
                //}
                //foreach (var item in DSSanPham)
                //{
                //    DSSanPhamCapNhat.Add(item);
                //}
                //DSSanPhamCapNhat = DSSanPham;

                foreach (var item in temp)
                {
                    DSSanPhamCapNhat.Add(item);
                }
                DSSanPham = temp;
            }
            catch (Exception)
            {
            }
            //listProduct.Clear();
            //foreach (var item in DSSanPham)
            //{
            //    listProduct.Add(item);
            //}
            //listProductTemp.Clear();
            //foreach (var item in listProduct)
            //{
            //    listProductTemp.Add(item);
            //}
            TongTien = TinhTongThanhTien();
            TinhTienTraLai();
        }

        public void LoadDSKhachHang()
        {
            DSKhachHang = DSKhachHangDB.Where(x => !x.TenKhachHang.ToLower().Equals("khách lẻ"));
        }
        public string TinhTongThanhTien()
        {
            double ThanhTienDouble = (double)DSSanPhamDaChon.Sum(x => ConvertUtils.convertMoneyToDouble(x.ThanhTien));
            return ConvertUtils.convertDoubleToMoney(ThanhTienDouble);
        }

        public void TinhTienTraLai()
        {
            try
            {
                if (TienKhachTra == "")
                {
                    
                    var temp = (double.Parse("0") - ConvertUtils.convertMoneyToDouble(TinhTongThanhTien()));
                    TienThua = ConvertUtils.convertDoubleToMoney(temp);
                }
                else
                {
                    var temp = (double.Parse(TienKhachTra.Replace(".", "")) - ConvertUtils.convertMoneyToDouble(TinhTongThanhTien()));
                    TienThua = ConvertUtils.convertDoubleToMoney(temp);
                }
            }
            catch (Exception e) { }
        }
    }
}
