using GemstonesBusinessSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GemstonesBusinessSystem.ViewModel
{
    class MainViewModel : BaseViewModel
    {

        #region Thuộc tính ẩn hiện grid
        public enum EnumChucNang
        {
            TrangChu, DanhSachSP, DonHang, DanhSachDV, DonDichVu, DoiTac, NhapHang, TonKho, Nhanvien,ThietLap, DiemGiaoDich
        };
        private int _ChucNang;
        public int ChucNang { get => _ChucNang; set { _ChucNang = value; OnPropertyChanged(); } }

        public enum EnumChucVu
        {
            Admin, QuanLy, NhanVien
        };

        private int _ChucVu;
        public int ChucVu { get => _ChucVu; set { _ChucVu = value; OnPropertyChanged(); } }

        #endregion

        #region Command ẩn hiện Grid
        public ICommand TrangChuCommand { get; set; }
        public ICommand DanhSachSPCommand { get; set; }
        public ICommand DonHangCommand { get; set; }
        public ICommand DanhSachDVCommand { get; set; }
        public ICommand DonDichVuCommand { get; set; }
        public ICommand DoiTacCommand { get; set; }
        public ICommand NhapHangCommand { get; set; }
        public ICommand TonKhoCommand { get; set; }
        public ICommand NhanvienCommand { get; set; }
        public ICommand ThietLapCommand { get; set; }
        public ICommand DiemGiaoDichCommand { get; set; }


        #endregion

        #region Command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ChiTietNVCommand { get; set; }
        public ICommand DoiMatKhauCommand { get; set; }
        public ICommand DangXuatCommand { get; set; }
        #endregion

        #region Thuộc tính
        public bool IsLoaded = false;
        private string _TenDayDu;
        public string TenDayDu { get => _TenDayDu; set { _TenDayDu = value; OnPropertyChanged(); } }

        private ImageSource _imageSource;
        public ImageSource imageSource { get => _imageSource; set { _imageSource = value; OnPropertyChanged(); } }

        #endregion

        public MainViewModel()
        {
            #region command
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IsLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                

                CapNhatThongTin();
                //var memberLoginVM = loginWindow.DataContext as LoginViewModel;
                //if (isLoginAgain == false)
                //{
                //    loginWindow.ShowDialog();
                //    memberLoginVM.LoadImage();
                //}

                //if (loginWindow.DataContext == null)
                //    return;
                //var memberLoginVM = loginWindow.DataContext as LoginViewModel;
                //IdAccount = memberLoginVM.GetIdUser;
                if (LoginViewModel.IsLogin)
                {

                    p.Show();
                    //var memberList = DataProvider.Ins.DB.Members;
                    //bool checkMember = false;

                    //foreach (var item in memberList)
                    //{
                    //    if (item.IdAccount == IdAccount)
                    //        checkMember = true; // đã có thông tin
                    //}
                    //if (checkMember == false) // chưa có thông tin 
                    //{
                    //    var user = new Member() { IdAccount = IdAccount };
                    //    DataProvider.Ins.DB.Members.Add(user);
                    //    DataProvider.Ins.DB.SaveChanges();
                    //}
                    //LoadDataJob();
                }
                else
                {
                    p.Close();
                }

                loginWindow.Close();

            }
            );

            // Chi tiết nhân viên
            ChiTietNVCommand = new RelayCommand<Object>((p) =>
            {
                if (LoginViewModel.GetIdInfo == 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                InfoAccountWindow infoAccount = new InfoAccountWindow();
                ChiTietNhanVienModel model = new ChiTietNhanVienModel();
                model = model.convert(DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == LoginViewModel.GetIdInfo).FirstOrDefault());
                (infoAccount.DataContext as InfoStaffViewModel).LoadNhanVien(model);
                infoAccount.ShowDialog();
                CapNhatThongTin();
            });

            // Đổi mật khẩu
            DoiMatKhauCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChangePasswordWindow changePassword = new ChangePasswordWindow();
                changePassword.ShowDialog();
            });

            // Chi tiết nhân viên
            DangXuatCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                var mv = new MainWindow();
                p.Close();
                mv.Show();
                //p.Close();   
            });

            #endregion
            #region Các giá trị khởi tạo
            #endregion
            #region Xử lí ẩn hiện Grid
            TrangChuCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.TrangChu;
                DashBoard.status = 1;
            });

            DanhSachSPCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DanhSachSP;
                ListProductsViewModel.status = 1;
            });

            DonHangCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DonHang;
                ListSellProductsViewModel.status = 1;
            });

            DanhSachDVCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DanhSachDV;
                ListServiceViewModel.status = 1;
            });
            DonDichVuCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DonDichVu;
                ListBillServiceViewModel.status = 1;
            });

            DoiTacCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DoiTac;
                CustomerViewModel.status = 1;
                ProviderViewModel.status = 1;
            });
            NhapHangCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.NhapHang;
                ListBuyProductViewModel.status = 1;
            });
            TonKhoCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.TonKho;
                InventoryReportViewModel.status = 1;
            });

            NhanvienCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.Nhanvien;
                StaffViewModel.status = 1;
            });

            ThietLapCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.ThietLap;
                SettingViewModel.status = 1;
            });

            DiemGiaoDichCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DiemGiaoDich;
                DealViewModel.status = 1;

            });

            #endregion

        }
        public void CapNhatThongTin()
        {
            if (LoginViewModel.GetIdInfo != 0)
            {
                var Nhanvien = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == LoginViewModel.GetIdInfo).FirstOrDefault();
                TenDayDu = Nhanvien.TenNhanVien;
                imageSource = Utils.HandleImage.GetImage(Nhanvien.HinhAnhNV);
                ChucVu = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.MaNhanVien == LoginViewModel.GetIdInfo).FirstOrDefault().MaChucVu.Value;
            }
        }
    }


}
