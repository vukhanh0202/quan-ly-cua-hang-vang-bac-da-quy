using GemstonesBusinessSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class NewCustomerViewModel : BaseViewModel
    {
        #region thuộc tính binding
        private KHACHHANG _KhachHangMoi;
        public KHACHHANG KhachHangMoi { get => _KhachHangMoi; set { _KhachHangMoi = value; OnPropertyChanged(); } }
        #endregion

        #region command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }

        #endregion
        public NewCustomerViewModel()
        {
            #region command
            // Command khi load vào window
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                KhachHangMoi = new KHACHHANG();
            });

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertString.convertString(KhachHangMoi.TenKhachHang) != ""
                    && Utils.ConvertString.convertString(KhachHangMoi.DiaChiKH) != ""
                    && Utils.ConvertString.convertString(KhachHangMoi.SoDienThoaiKH) != ""
                    && Utils.ConvertString.convertString(KhachHangMoi.EmailKH) != "")
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
                    KhachHangMoi.TongTienMuaKH = 0;
                    KhachHangMoi.TrangThaiKH = Constant.ACTIVE_STATUS;
                    DataProvider.Ins.DB.KHACHHANGs.Add(KhachHangMoi);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    p.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Thêm thất bại, vui lòng kiểm tra lại");
                }
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
    }
}
