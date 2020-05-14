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
    class DetailProviderViewModel : BaseViewModel
    {
        private ObservableCollection<PHIEUMUAHANG> _DSPMHNhaCungCap;
        public ObservableCollection<PHIEUMUAHANG> DSPMHNhaCungCap { get => _DSPMHNhaCungCap; set { _DSPMHNhaCungCap = value; OnPropertyChanged(); } }

        private NHACUNGCAP _CTNhaCungCap;
        public NHACUNGCAP CTNhaCungCap { get => _CTNhaCungCap; set { _CTNhaCungCap = value; OnPropertyChanged(); } }

        private PHIEUMUAHANG _PMHDaChon;
        public PHIEUMUAHANG PMHDaChon { get => _PMHDaChon; set { _PMHDaChon = value; OnPropertyChanged(); } }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand ChiTietPMHCommand { get; set; }


        public DetailProviderViewModel()
        {
            #region command

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertUtils.convertString(CTNhaCungCap.TenNhaCungCap) != ""
                    && Utils.ConvertUtils.convertString(CTNhaCungCap.DiaChiNCC) != ""
                    && Utils.ConvertUtils.convertString(CTNhaCungCap.SoDienThoaiNCC) != ""
                    && Utils.ConvertUtils.convertString(CTNhaCungCap.EmailNCC) != "")
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
                    NHACUNGCAP NhaCungCap = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap == CTNhaCungCap.MaNhaCungCap).SingleOrDefault();
                    NhaCungCap = CTNhaCungCap;

                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật thành công");
                    p.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cập nhật thất bại, vui lòng kiểm tra lại");
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

            // Chi tiết hóa đơn bán hàng
            ChiTietPMHCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DetailBuyProductWindow detailProduct = new DetailBuyProductWindow();
                (detailProduct.DataContext as DetailBuyProductViewModel).LoadChiTietPhieuMuaHang(PMHDaChon);
                detailProduct.ShowDialog();
            });
            #endregion
        }
        public void LayDSTuDatabase()
        {
            try
            {
                DSPMHNhaCungCap = new ObservableCollection<PHIEUMUAHANG>(DataProvider.Ins.DB.PHIEUMUAHANGs.Where(x => x.MaNhaCungCap == CTNhaCungCap.MaNhaCungCap));
            }
            catch (Exception) { }
        }
        public void LoadDuLieuTuNhaCungCap(NHACUNGCAP NhaCungCap)
        {
            CTNhaCungCap = NhaCungCap;
            LayDSTuDatabase();
        }
    }
}
