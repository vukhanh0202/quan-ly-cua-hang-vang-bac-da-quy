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
    class InfoStaffViewModel : BaseViewModel
    {
        #region Thuộc tính binding

        private ChiTietNhanVienModel _CTNhanVien;
        public ChiTietNhanVienModel CTNhanVien { get => _CTNhanVien; set { _CTNhanVien = value; OnPropertyChanged(); } }

        private TAIKHOAN _TaiKhoan;
        public TAIKHOAN TaiKhoan { get => _TaiKhoan; set { _TaiKhoan = value; OnPropertyChanged(); } }

        private string _TenDangNhap;
        public string TenDangNhap { get => _TenDangNhap; set { _TenDangNhap = value; OnPropertyChanged(); } }

        private string _MatKhau;
        public string MatKhau { get => _MatKhau; set { _MatKhau = value; OnPropertyChanged(); } }


        private ImageSource _imageSource;
        public ImageSource imageSource { get => _imageSource; set { _imageSource = value; OnPropertyChanged(); } }

        private CHUCVU _ChucVuDaChon;
        public CHUCVU ChucVuDaChon { get => _ChucVuDaChon; set { _ChucVuDaChon = value; OnPropertyChanged(); } }

        private ObservableCollection<CHUCVU> _DSChucVu;
        public ObservableCollection<CHUCVU> DSChucVu { get => _DSChucVu; set { _DSChucVu = value; OnPropertyChanged(); } }

        private bool _FLagChucVu;
        public bool FLagChucVu { get => _FLagChucVu; set { _FLagChucVu = value; OnPropertyChanged(); } }
        #endregion

        #region command
        public ICommand XacNhanCommand { get; set; }

        public ICommand HuyBoCommand { get; set; }

        public ICommand ThayDoiAnhCommand { get; set; }
        public ICommand SelectionChangeCommand { get; set; }

        #endregion

        public InfoStaffViewModel()
        {

            #region command
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {

                if (Utils.ConvertUtils.convertString(CTNhanVien.TenNhanVien) != ""
                   && Utils.ConvertUtils.convertString(CTNhanVien.EmailNV) != ""
                   && Utils.ConvertUtils.convertString(CTNhanVien.LuongCoBan.ToString()) != ""
                   && Utils.ConvertUtils.convertString(CTNhanVien.SoDienThoaiNV.ToString()) != ""
                   && Utils.ConvertUtils.convertString(TenDangNhap.ToString()) != ""
                   && Utils.ConvertUtils.convertString(MatKhau.ToString()) != ""
                   && Utils.ConvertUtils.convertString(CTNhanVien.DiaChiNV.ToString()) != "")
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
                try
                {

                    var NhanVien = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == CTNhanVien.MaNhanVien).SingleOrDefault();

                    NhanVien.MaNhanVien = CTNhanVien.MaNhanVien;
                    NhanVien.TenNhanVien = CTNhanVien.TenNhanVien;
                    NhanVien.DiaChiNV = CTNhanVien.DiaChiNV;
                    NhanVien.EmailNV = CTNhanVien.EmailNV;
                    NhanVien.SoDienThoaiNV = CTNhanVien.SoDienThoaiNV;
                    NhanVien.LuongCoBan = CTNhanVien.LuongCoBan;
                    NhanVien.HinhAnhNV = CTNhanVien.HinhAnhNV;

                    TaiKhoan.TenDangNhap = TenDangNhap;
                    TaiKhoan.MatKhau = MatKhau;

                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật thành công");
                    p.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Cập nhật thất bại vui lòng kiểm tra lại");
                }
            });

            HuyBoCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            ThayDoiAnhCommand = new RelayCommand<Object>((p) =>
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
                    CTNhanVien.HinhAnhNV = Utils.HandleImage.ImageToString(openFileDialog.FileName);
                    imageSource = Utils.HandleImage.GetImage(CTNhanVien.HinhAnhNV);
                }
            });
            SelectionChangeCommand = new RelayCommand<Window>((p) =>
            {
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
        public void LoadNhanVien(ChiTietNhanVienModel NhanVien)
        {
            DSChucVu = new ObservableCollection<CHUCVU>(DataProvider.Ins.DB.CHUCVUs);
            TaiKhoan = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.MaNhanVien == NhanVien.MaNhanVien).FirstOrDefault();
            TenDangNhap = TaiKhoan.TenDangNhap;
            MatKhau = TaiKhoan.MatKhau;
            ChucVuDaChon = TaiKhoan.CHUCVU;
            CTNhanVien = NhanVien;
            imageSource = Utils.HandleImage.GetImage(NhanVien.HinhAnhNV);
            if (DataProvider.Ins.DB.TAIKHOANs.Where(x => x.MaNhanVien == LoginViewModel.GetIdInfo).FirstOrDefault().CHUCVU.MaChucVu == 1)
            {
                FLagChucVu = true;
            }
            else
            {
                FLagChucVu = false;
            }
        }

    }
}
