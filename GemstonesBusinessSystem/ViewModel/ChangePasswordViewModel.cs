using GemstonesBusinessSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class ChangePasswordViewModel : BaseViewModel
    {
        private string _MatKhauCu;
        public string MatKhauCu { get => _MatKhauCu; set { _MatKhauCu = value; OnPropertyChanged(); } }


        private string _MatKhauMoi;
        public string MatKhauMoi { get => _MatKhauMoi; set { _MatKhauMoi = value; OnPropertyChanged(); } }

        private string _NhapLaiMatKhau;
        public string NhapLaiMatKhau { get => _NhapLaiMatKhau; set { _NhapLaiMatKhau = value; OnPropertyChanged(); } }

        #region Command
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand OldPasswordChangedCommand { get; set; }
        public ICommand NewPasswordChangedCommand { get; set; }
        public ICommand NewPasswordAgainChangedCommand { get; set; }
        #endregion
        public ChangePasswordViewModel()
        {
            #region Command

            TAIKHOAN TaiKhoan = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.MaNhanVien == LoginViewModel.GetIdInfo).FirstOrDefault();

            OldPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { MatKhauCu = p.Password; });
            NewPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { MatKhauMoi = p.Password; });
            NewPasswordAgainChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { NhapLaiMatKhau = p.Password; });
            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertUtils.convertString(MatKhauCu) != ""
                   &&Utils.ConvertUtils.convertString(MatKhauMoi) != ""
                   &&Utils.ConvertUtils.convertString(NhapLaiMatKhau) != "")
                {

                    if (!MatKhauCu.Equals(TaiKhoan.MatKhau))
                    {
                        MessageBox.Show("Sai mật khẩu! Vui Long nhập lại mật khẩu hiện tại!");
                        return false;
                    }
                    else if (!MatKhauMoi.Equals(NhapLaiMatKhau))
                    {
                        MessageBox.Show("Mật khẩu mới trùng khớp! Vui lòng nhập lại");
                        return false;
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                    return false;
                }
            }, (p) =>
            {

                try
                {
                    TaiKhoan.MatKhau = MatKhauMoi;

                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Đổi mật khẩu thành công!");
                    p.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Thất bại");
                }
            });

            HuyBoCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
            #endregion
        }

    }
}
