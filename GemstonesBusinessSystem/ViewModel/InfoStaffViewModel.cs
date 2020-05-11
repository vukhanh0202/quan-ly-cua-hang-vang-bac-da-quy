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
    class InfoStaffViewModel : BaseViewModel
    {
        #region Thuộc tính binding

        //private int _id;
        //public int id { get => _id; set { _id = value; OnPropertyChanged(); } }

        //private string _name;
        //public string name { get => _name; set { _name = value; OnPropertyChanged(); } }

        //private string _address;
        //public string address { get => _address; set { _address = value; OnPropertyChanged(); } }

        //private string _email;
        //public string email { get => _email; set { _email = value; OnPropertyChanged(); } }

        //private string _phone;
        //public string phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }

        //private string _basicSalary;
        //public string basicSalary { get => _basicSalary; set { _basicSalary = value; OnPropertyChanged(); } }

        //private string _bonusPercent;
        //public string bonusPercent { get => _bonusPercent; set { _bonusPercent = value; OnPropertyChanged(); } }

        //private Staff _detailStaff;
        //public Staff detailStaff { get => _detailStaff; set { _detailStaff = value; OnPropertyChanged(); } }

        //private string _image;
        //public string image { get => _image; set { _image = value; OnPropertyChanged(); } }

        private NHANVIEN _CTNhanVien;
        public NHANVIEN CTNhanVien { get => _CTNhanVien; set { _CTNhanVien = value; OnPropertyChanged(); } }

        private ImageSource _imageSource;
        public ImageSource imageSource { get => _imageSource; set { _imageSource = value; OnPropertyChanged(); } }

        #endregion

        #region command
        public ICommand XacNhanCommand { get; set; }

        public ICommand HuyBoCommand { get; set; }

        public ICommand ThayDoiAnhCommand { get; set; }

        #endregion

        public InfoStaffViewModel()
        {

            #region command
            XacNhanCommand = new RelayCommand<Window>((p) => {

                if (Utils.ConvertUtils.convertString(CTNhanVien.TenNhanVien) != ""
                   && Utils.ConvertUtils.convertString(CTNhanVien.EmailNV) != ""
                   && Utils.ConvertUtils.convertString(CTNhanVien.LuongCoBan.ToString()) != ""
                   && Utils.ConvertUtils.convertString(CTNhanVien.PhanTramHoaHong.ToString()) != "")
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
                    NhanVien = CTNhanVien;
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

            #endregion
        }
        public void LoadNhanVien(NHANVIEN NhanVien)
        {
            CTNhanVien = NhanVien;
            imageSource = Utils.HandleImage.GetImage(NhanVien.HinhAnhNV);
        }
    }
}
