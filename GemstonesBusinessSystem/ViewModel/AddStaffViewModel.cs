using GemstonesBusinessSystem.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GemstonesBusinessSystem.ViewModel
{
    class AddStaffViewModel : BaseViewModel
    {
        #region thuộc tính binding
        private NHANVIEN _NhanVienMoi;
        public NHANVIEN NhanVienMoi { get => _NhanVienMoi; set { _NhanVienMoi = value; OnPropertyChanged(); } }

        private ImageSource _imageSource;
        public ImageSource imageSource { get => _imageSource; set { _imageSource = value; OnPropertyChanged(); } }
        #endregion

        #region command
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ThayDoiHinhAnhCommand { get; set; }


        #endregion

        public AddStaffViewModel()
        {

            #region command

            // Command khi load vào window
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                NhanVienMoi = new NHANVIEN();
                NhanVienMoi.HinhAnhNV = Utils.HandleImage.ImageToString("../../Images/Empty.png");
                imageSource = Utils.HandleImage.GetImage(NhanVienMoi.HinhAnhNV);
            });

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertUtils.convertString(NhanVienMoi.TenNhanVien) != ""
                  && Utils.ConvertUtils.convertString(NhanVienMoi.SoDienThoaiNV) != ""
                  && Utils.ConvertUtils.convertString(NhanVienMoi.LuongCoBan.ToString()) != ""
                  && Utils.ConvertUtils.convertString(NhanVienMoi.PhanTramHoaHong.ToString()) != "")
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                    return false;
                }
            }, (p) =>
            {
                DataProvider.Ins.DB.NHANVIENs.Add(NhanVienMoi);
                DataProvider.Ins.DB.SaveChanges();
                //var roleId = DataProvider.Ins.DB.AccountRoles.Where(x => x.code == Constant.codeRoleSTAFF).SingleOrDefault().id;
                //Account account = new Account() { username = NhanVienMoi.code, password = NhanVienMoi.code, status = Constant.ACTIVE_STATUS, account_roles_id = roleId };
                //DataProvider.Ins.DB.Accounts.Add(account);
                //NhanVienMoi.account_id = account.id;
                DataProvider.Ins.DB.SaveChanges();
                //MessageBox.Show("Tài khoản mặc định của bạn là:\nUsername: " + account.username + "\nPassword: " + account.password);
                p.Close();
            });

            HuyBoCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

            // Thêm ảnh
            ThayDoiHinhAnhCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = @"D:\",
                    Title = "Chọn ảnh",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "txt",
                    Filter = "Images (*.JPG;*.PNG)|*.JPG;*.PNG",
                    RestoreDirectory = true,
                    ReadOnlyChecked = true,
                    ShowReadOnly = true
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    NhanVienMoi.HinhAnhNV = Utils.HandleImage.ImageToString(openFileDialog.FileName);
                    imageSource = Utils.HandleImage.GetImage(NhanVienMoi.HinhAnhNV);
                }
            });
            #endregion

        }

    }
}
