using GemstonesBusinessSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class LoginViewModel : BaseViewModel
    {

        public static bool IsLogin { get; set; }
        public static int GetIdInfo { get; set; }

        private string _TenDangNhap;
        public string TenDangNhap { get => _TenDangNhap; set { _TenDangNhap = value; OnPropertyChanged(); } }

        private string _MatKhau;
        public string MatKhau { get => _MatKhau; set { _MatKhau = value; OnPropertyChanged(); } }


        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public bool checkMember = false;

        public bool check = false;
        public LoginViewModel()
        {
            IsLogin = false;
            GetIdInfo = 0;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Login(p);
            });
            //RegisterCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
            //    RegisterWindow registerWindow = new RegisterWindow();
            //    registerWindow.ShowDialog();
            //});
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { MatKhau = p.Password; });


        }
        public void Login(Window p)
        {

            if (p == null)
                return;

            var TaiKhoan = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.TenDangNhap.Equals(TenDangNhap) && x.MatKhau.Equals(MatKhau)).FirstOrDefault();

            if (TaiKhoan != null)
            {
                IsLogin = true;
                GetIdInfo = TaiKhoan.MaNhanVien.Value;
                p.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }

        }

    }
}
