using GemstonesBusinessSystem.Model;
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
    class ChooseCustomerViewModel : BaseViewModel
    {
        #region Binding
        private KHACHHANG _KHDaChon;
        public KHACHHANG KHDaChon { get => _KHDaChon; set { _KHDaChon = value; OnPropertyChanged(); } }

        //private KHACHHANG _customer;
        //public KHACHHANG customer { get => _customer; set { _customer = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private bool flagIsConfirm;
        #endregion

        #region  List Binding
        private IEnumerable<KHACHHANG> _DSKhachHang;
        public IEnumerable<KHACHHANG> DSKhachHang { get => _DSKhachHang; set { _DSKhachHang = value; OnPropertyChanged(); } }
        #endregion

        #region  Command
        public ICommand TimKiemCommand { get; set; }

        public ICommand ThemKHMoiCommand { get; set; }

        public ICommand HuyBoCommand { get; set; }

        public ICommand ChiTietKHCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        #endregion

        public ChooseCustomerViewModel()
        {

            #region Command
            // Command khi load vào window
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                LayDSTuDatabase();
                KHDaChon = null;
            });

            // Tìm kiếm 
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) == "" || DSKhachHang == null)
                {
                    LayDSTuDatabase();
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    DSKhachHang = DSKhachHang.Where(x => (x.MaKhachHang.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.SoDienThoaiKH.ToLower().Contains(TimKiem.ToLower())
                                                        || x.EmailKH.ToLower().Contains(TimKiem.ToLower())));
                }
                catch (Exception) { }
            });

            // Thêm khách hàng mới
            ThemKHMoiCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                NewCustomerWindow newCustomerWindow = new NewCustomerWindow();
                newCustomerWindow.ShowDialog();
                LayDSTuDatabase();
            });

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (KHDaChon != null)
                {
                    flagIsConfirm = true;
                    p.Close();
                }
                else { MessageBox.Show("Vui lòng chọn 1 khách hàng!"); }
            });

            // Hủy bỏ
            HuyBoCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });

            #endregion
        }

        public void LayDSTuDatabase()
        {
            DSKhachHang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x=> !x.TenKhachHang.Equals("Khách lẻ")));
        }
        public KHACHHANG LayKhachHangHienTai()
        {
            // Nếu ấn nút xác nhận mới trả về
            if (flagIsConfirm == true)
            {
                return KHDaChon;
            }
            return null;
        }
    }
}
