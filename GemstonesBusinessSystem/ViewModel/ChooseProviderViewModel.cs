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
    class ChooseProviderViewModel : BaseViewModel
    {
        #region Binding
        private NHACUNGCAP _NCCDaChon;
        public NHACUNGCAP NCCDaChon { get => _NCCDaChon; set { _NCCDaChon = value; OnPropertyChanged(); } }

        //private KHACHHANG _customer;
        //public KHACHHANG customer { get => _customer; set { _customer = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private bool flagIsConfirm;
        #endregion

        #region  List Binding
        private IEnumerable<NHACUNGCAP> _DSNhaCungCap;
        public IEnumerable<NHACUNGCAP> DSNhaCungCap { get => _DSNhaCungCap; set { _DSNhaCungCap = value; OnPropertyChanged(); } }

        #endregion

        #region  Command
        public ICommand TimKiemCommand { get; set; }

        public ICommand ThemnCCMoiCommand { get; set; }

        public ICommand HuyBoCommand { get; set; }

        public ICommand ChiTietNCCCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        #endregion

        public ChooseProviderViewModel()
        {

            #region Command
            // Command khi load vào window
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                LayDSTuDatabase();
                NCCDaChon = null;
                flagIsConfirm = false;
            });

            // Tìm kiếm 
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) == "" || DSNhaCungCap == null)
                {
                    LayDSTuDatabase();
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    DSNhaCungCap = DSNhaCungCap.Where(x => (x.MaNhaCungCap.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.TenNhaCungCap.ToLower().Contains(TimKiem.ToLower())
                                                        || x.SoDienThoaiNCC.ToLower().Contains(TimKiem.ToLower())
                                                        || x.EmailNCC.ToLower().Contains(TimKiem.ToLower())));
                }
                catch (Exception) { }
            });

            // Thêm khách hàng mới
            ThemnCCMoiCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                NewProviderWindow newProviderWindow = new NewProviderWindow();
                newProviderWindow.ShowDialog();
                LayDSTuDatabase();
            });

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (NCCDaChon != null)
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
            DSNhaCungCap = new ObservableCollection<NHACUNGCAP>(DataProvider.Ins.DB.NHACUNGCAPs.Where(x=>x.TrangThaiNCC == Constant.ACTIVE_STATUS));
        }
        public NHACUNGCAP LayNCCHienTai()
        {
            // Nếu ấn nút xác nhận mới trả về
            if (flagIsConfirm == true)
            {
                return NCCDaChon;
            }
            return null;
        }
    }
}
