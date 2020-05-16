using GemstonesBusinessSystem.Model;
using GemstonesBusinessSystem.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class CustomerViewModel : BaseViewModel
    {
        #region list binding

        private ObservableCollection<KHACHHANG> _DSKhachHangDB;
        public ObservableCollection<KHACHHANG> DSKhachHangDB { get => _DSKhachHangDB; set { _DSKhachHangDB = value; OnPropertyChanged(); } }

        private IEnumerable<KHACHHANG> _DSKhachHang;
        public IEnumerable<KHACHHANG> DSKhachHang { get => _DSKhachHang; set { _DSKhachHang = value; OnPropertyChanged(); } }

        private ObservableCollection<KHACHHANG> _DSDiaChiKhachHang;
        public ObservableCollection<KHACHHANG> DSDiaChiKhachHang { get => _DSDiaChiKhachHang; set { _DSDiaChiKhachHang = value; OnPropertyChanged(); } }

        private ObservableCollection<KHACHHANG> _DSKHDaChon;
        public ObservableCollection<KHACHHANG> DSKHDaChon { get => _DSKHDaChon; set { _DSKHDaChon = value; OnPropertyChanged(); } }

        #endregion

        #region thuộc tính binding

        private int _TongSoLuong;
        public int TongSoLuong { get => _TongSoLuong; set { _TongSoLuong = value; OnPropertyChanged(); } }

        private double _TongBan;
        public double TongBan { get => _TongBan; set { _TongBan = value; OnPropertyChanged(); } }

        private bool _FlagTatCaKH;
        public bool FlagTatCaKH { get => _FlagTatCaKH; set { _FlagTatCaKH = value; OnPropertyChanged(); } }

        private KHACHHANG _KHTheoDiaChi;
        public KHACHHANG KHTheoDiaChi { get => _KHTheoDiaChi; set { _KHTheoDiaChi = value; OnPropertyChanged(); } }

        //private KHACHHANG _defaultCustomer; // Khách lẻ
        //public KHACHHANG defaultCustomer { get => _defaultCustomer; set { _defaultCustomer = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private KHACHHANG _KHDaChon;
        public KHACHHANG KHDaChon { get => _KHDaChon; set { _KHDaChon = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        #region command
        public ICommand TatCaKHCommand { get; set; }
        public ICommand KhachHangHDCommand { get; set; }
        public ICommand KhachHangNHDCommand { get; set; }
        public ICommand SelectedAddressCommand { get; set; }
        public ICommand XoaLocCommand { get; set; }
        public ICommand TimKiemCommand { get; set; }
        public ICommand ThemKHCommand { get; set; }
        public ICommand ChiTietKHCommand { get; set; }
        public ICommand XoaKHCommand { get; set; }
        public ICommand NgungVaKhoiPhucCommand { get; set; }
        public ICommand ReloadCommand { get; set; }

        public RelayCommand<IList> SelectionChangedCommand { get; set; }


        #endregion
        public CustomerViewModel()
        {


            #region command
            // Load trang sau mỗi lần click mở trang
            ReloadCommand = new RelayCommand<Object>((p) =>
            {
                if (status == 1)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                FlagTatCaKH = true;
                LayDSTuDatabase();
                LoadDanhSach();
                status = 0;
            });

            // Lấy danh sách được chọn từ dataGrids
            SelectionChangedCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                DSKHDaChon.Clear();
                foreach (var item in items)
                    DSKHDaChon.Add((KHACHHANG)item);
            });

            // Chọn lọc theo khu vực
            SelectedAddressCommand = new RelayCommand<Object>((p) =>
            {
                if (KHTheoDiaChi == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    if (KHTheoDiaChi.DiaChiKH.ToLower() == "tất cả")
                    {
                        DSKhachHang = DSKhachHangDB;
                    }
                    else
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.DiaChiKH.ToLower().Contains(Utils.ConvertUtils.convertString(KHTheoDiaChi.DiaChiKH).ToLower()));

                    }
                    TongSoLuong = DSKhachHang.Count();
                    TongBan = TinhTongThanhTien(DSKhachHang);
                }
                catch (Exception) { }
            });

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) != "" && DSKhachHang != null)
                {
                    return true;
                }
                else
                {
                    LoadDanhSach();
                    return false;
                }
            }, (p) =>
            {
                try
                {
                    DSKhachHang = DSKhachHang.Where(x => (x.MaKhachHang.ToString().Contains(TimKiem.ToLower())
                                                        || x.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.SoDienThoaiKH.ToLower().Contains(TimKiem.ToLower())
                                                        || x.DiaChiKH.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongTienMuaKH.ToString().ToLower().Contains(TimKiem.ToLower())));
                    TongSoLuong = DSKhachHang.Count();
                    TongBan = TinhTongThanhTien(DSKhachHang);
                }
                catch (Exception) { }
            });

            // Xóa/ ngưng hoạt động khách hàng
            XoaKHCommand = new RelayCommand<Object>((p) =>
            {
                if (DSKHDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn có chắc chắn thực hiện hành động loại bỏ khách hàng", "Xác nhận", MessageBoxButton.OKCancel);
                if (isExit == MessageBoxResult.OK)
                {
                    try
                    {
                        foreach (var KH in DSKHDaChon)
                        {
                            bool flag = true;
                            if (KH.TrangThaiKH == Constant.ACTIVE_STATUS)
                            {
                                DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == KH.MaKhachHang).SingleOrDefault().TrangThaiKH = Constant.INACTIVE_STATUS;
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            else if (KH.TrangThaiKH == Constant.INACTIVE_STATUS)
                            {
                                // Kiểm tra phiếu bán hàng
                                var PBH = DataProvider.Ins.DB.PHIEUBANHANGs.Where(x => x.MaKhachHang == KH.MaKhachHang).FirstOrDefault();
                                var PDV = DataProvider.Ins.DB.PHIEUDICHVUs.Where(x => x.MaKhachHang == KH.MaKhachHang).FirstOrDefault();

                                DataProvider.Ins.DB.SaveChanges();

                                if (PBH != null || PDV !=null)
                                {
                                    MessageBox.Show("Vui lòng xóa những dữ liệu liên quan đến khách hàng này!");
                                    flag = false;
                                }
                            }
                            if (flag == true)
                            {
                                // XÓa khách hàng
                                DataProvider.Ins.DB.KHACHHANGs.Remove(KH);
                                DataProvider.Ins.DB.SaveChanges();
                                MessageBox.Show("Thành công");
                            }
                        }
                        LayDSTuDatabase();
                        LoadDanhSach();
                        DSKHDaChon.Clear();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Hành động thất bại vui lòng thử lại");
                    }
                }
            });

            // Ngưng hoạt động và khôi phục 
            NgungVaKhoiPhucCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    var KH = DSKhachHang.Where(x => x.MaKhachHang.ToString().Equals(p.ToString())).SingleOrDefault();
                    if (KH.TrangThaiKH == Constant.ACTIVE_STATUS)
                    {
                        DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == KH.MaKhachHang).SingleOrDefault().TrangThaiKH = Constant.INACTIVE_STATUS;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else if(KH.TrangThaiKH == Constant.INACTIVE_STATUS)
                    {
                        DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == KH.MaKhachHang).SingleOrDefault().TrangThaiKH = Constant.ACTIVE_STATUS;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    LayDSTuDatabase();
                    LoadDanhSach();
                }
                catch (Exception)
                {
                    MessageBox.Show("Hành động thất bại vui lòng thử lại");
                }
            });

            // Xóa lọc
            XoaLocCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(KHTheoDiaChi.DiaChiKH).ToLower() == "tất cả")
                    return false;
                return true;
            }, (p) =>
            {
                KHTheoDiaChi = DSDiaChiKhachHang.Where(x => x.DiaChiKH.ToLower() == "tất cả").SingleOrDefault();
                DSKhachHang = DSKhachHangDB;
                TongSoLuong = DSKhachHang.Count();
                TongBan = TinhTongThanhTien(DSKhachHang);
                FlagTatCaKH = true;
            });


            // Tất cả khách hàng
            TatCaKHCommand = new RelayCommand<Object>((p) =>
            {
                if (KHTheoDiaChi != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                try
                {
                    if (KHTheoDiaChi.DiaChiKH.ToLower() == "tất cả")
                    {
                        DSKhachHang = DSKhachHangDB;
                    }
                    else
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.DiaChiKH.ToLower().Contains(Utils.ConvertUtils.convertString(KHTheoDiaChi.DiaChiKH).ToLower()));
                    }
                    TongSoLuong = DSKhachHang.Count();
                    TongBan = TinhTongThanhTien(DSKhachHang);
                }
                catch (Exception)
                {
                }

            });


            // Khách hàng đang hoạt động
            KhachHangHDCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    if (KHTheoDiaChi.DiaChiKH.ToLower() == "tất cả")
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.TrangThaiKH == Constant.ACTIVE_STATUS);
                    }
                    else
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.TrangThaiKH == Constant.ACTIVE_STATUS && x.DiaChiKH.ToLower().Contains(Utils.ConvertUtils.convertString(KHTheoDiaChi.DiaChiKH).ToLower()));
                    }
                    TongSoLuong = DSKhachHang.Count();
                    TongBan = TinhTongThanhTien(DSKhachHang);
                }
                catch (Exception)
                {
                }

            });

            // Khách hàng ngừng hoạt động
            KhachHangNHDCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    if (KHTheoDiaChi.DiaChiKH.ToLower() == "tất cả")
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.TrangThaiKH == Constant.INACTIVE_STATUS);
                    }
                    else
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.TrangThaiKH == Constant.INACTIVE_STATUS && x.DiaChiKH.ToLower().Contains(Utils.ConvertUtils.convertString(KHTheoDiaChi.DiaChiKH).ToLower()));
                    }
                    TongSoLuong = DSKhachHang.Count();
                    TongBan = TinhTongThanhTien(DSKhachHang);
                }
                catch (Exception)
                {
                }

            });

            // Thêm khách hàng
            ThemKHCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                NewCustomerWindow newCustomer = new NewCustomerWindow();
                p.Hide();
                newCustomer.ShowDialog();
                p.Show();
                LayDSTuDatabase();
                FlagTatCaKH = true;
                LoadDanhSach();
            });

            // Chi tiết khách hàng
            ChiTietKHCommand = new RelayCommand<object>((p) =>
            {
                if (KHDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                DetailCustomerWindow detailCustomer = new DetailCustomerWindow();
                (detailCustomer.DataContext as DetailCustomerViewModel).LoadDuLieuTuKhachHang(KHDaChon);
                detailCustomer.ShowDialog();
                KHDaChon = null;
                LayDSTuDatabase();
                FlagTatCaKH = true;
                LoadDanhSach();
            });
            #endregion
        }
        public void LayDSTuDatabase()
        {
            DSKhachHangDB = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.TenKhachHang.ToLower() != "khách lẻ"));
            //defaultCustomer = DSKhachHangDB[0];
            //KHTheoDiaChi = defaultCustomer;
            //DSKhachHangDB.RemoveAt(0);
            DSKHDaChon = new ObservableCollection<KHACHHANG>();
        }
        public void LoadDanhSach()
        {
            DSKhachHang = DSKhachHangDB;
            DSDiaChiKhachHang = new ObservableCollection<KHACHHANG>();
            foreach (var item in DSKhachHang)
            {
                DSDiaChiKhachHang.Add(item);
            }
            DSDiaChiKhachHang.Add(new KHACHHANG() { DiaChiKH = "Tất cả", MaKhachHang = -1 });
            KHTheoDiaChi = DSDiaChiKhachHang.Where(x => x.DiaChiKH.ToLower() == "tất cả").SingleOrDefault();

            TongSoLuong = DSKhachHang.Count();
            TongBan = TinhTongThanhTien(DSKhachHang);
        }
        public double TinhTongThanhTien(IEnumerable<KHACHHANG> DSKhachHang)
        {
            double total = 0;
            foreach (var item in DSKhachHang)
            {
                total += (double)item.TongTienMuaKH;
            }
            return total;
        }
    }
}
