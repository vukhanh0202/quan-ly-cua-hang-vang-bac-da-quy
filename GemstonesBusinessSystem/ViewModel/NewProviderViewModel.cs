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
    class NewProviderViewModel : BaseViewModel
    {
        #region thuộc tính binding
        private NHACUNGCAP _NhaCungCapMoi;
        public NHACUNGCAP NhaCungCapMoi { get => _NhaCungCapMoi; set { _NhaCungCapMoi = value; OnPropertyChanged(); } }
        #endregion

        #region command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }

        #endregion

        public NewProviderViewModel()
        {
            #region command
            // Command khi load vào window
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                NhaCungCapMoi = new NHACUNGCAP();
            });

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertUtils.convertString(NhaCungCapMoi.TenNhaCungCap) != ""
                    && Utils.ConvertUtils.convertString(NhaCungCapMoi.DiaChiNCC) != ""
                    && Utils.ConvertUtils.convertString(NhaCungCapMoi.SoDienThoaiNCC) != ""
                    && Utils.ConvertUtils.convertString(NhaCungCapMoi.EmailNCC) != "")
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
                    NhaCungCapMoi.TongTienBanNCC = 0;
                    NhaCungCapMoi.TrangThaiNCC = Constant.ACTIVE_STATUS;
                    DataProvider.Ins.DB.NHACUNGCAPs.Add(NhaCungCapMoi);
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
