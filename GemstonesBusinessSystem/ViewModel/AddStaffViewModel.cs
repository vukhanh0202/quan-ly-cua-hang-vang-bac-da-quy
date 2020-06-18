using GemstonesBusinessSystem.Model;
using Microsoft.Win32;
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
    class AddStaffViewModel : BaseViewModel
    {
        #region thuộc tính binding
        private NHANVIEN _NhanVienMoi;
        public NHANVIEN NhanVienMoi { get => _NhanVienMoi; set { _NhanVienMoi = value; OnPropertyChanged(); } }

        private TAIKHOAN _TaiKhoan;
        public TAIKHOAN TaiKhoan { get => _TaiKhoan; set { _TaiKhoan = value; OnPropertyChanged(); } }

        private ImageSource _imageSource;
        public ImageSource imageSource { get => _imageSource; set { _imageSource = value; OnPropertyChanged(); } }

        private CHUCVU _ChucVuDaChon;
        public CHUCVU ChucVuDaChon { get => _ChucVuDaChon; set { _ChucVuDaChon = value; OnPropertyChanged(); } }

        private ObservableCollection<CHUCVU> _DSChucVu;
        public ObservableCollection<CHUCVU> DSChucVu { get => _DSChucVu; set { _DSChucVu = value; OnPropertyChanged(); } }
        #endregion

        #region command
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ThayDoiHinhAnhCommand { get; set; }
        public ICommand SelectionChangeCommand { get; set; }


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
                TaiKhoan = new TAIKHOAN();
                NhanVienMoi.HinhAnhNV = Utils.HandleImage.ImageToString("../../Images/Empty.png");
                imageSource = Utils.HandleImage.GetImage(NhanVienMoi.HinhAnhNV);
                DSChucVu = new ObservableCollection<CHUCVU>(DataProvider.Ins.DB.CHUCVUs);
            });

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertUtils.convertString(NhanVienMoi.TenNhanVien) != ""
                  && Utils.ConvertUtils.convertString(NhanVienMoi.SoDienThoaiNV) != ""
                  && Utils.ConvertUtils.convertString(NhanVienMoi.EmailNV) != ""
                  && Utils.ConvertUtils.convertString(NhanVienMoi.DiaChiNV) != ""
                  && Utils.ConvertUtils.convertString(NhanVienMoi.LuongCoBan.ToString()) != ""
                  && Utils.ConvertUtils.convertString(TaiKhoan.MatKhau.ToString()) != ""
                  && Utils.ConvertUtils.convertString(TaiKhoan.TenDangNhap.ToString()) != ""
                  && Utils.ConvertUtils.convertString(TaiKhoan.MaChucVu.ToString()) != "")
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

                TaiKhoan.MaNhanVien = NhanVienMoi.MaNhanVien;
                DataProvider.Ins.DB.TAIKHOANs.Add(TaiKhoan);

                DataProvider.Ins.DB.SaveChanges();
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

            SelectionChangeCommand = new RelayCommand<Window>((p) =>
            {
                if (ChucVuDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    TaiKhoan.MaChucVu = ChucVuDaChon.MaChucVu;
                }
                catch (Exception) { }
            });
            #endregion

        }

    }
}
