using GemstonesBusinessSystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class ListBuyProductViewModel : BaseViewModel
    {
        #region List binding
        private ObservableCollection<PHIEUMUAHANG> _DSPhieuMuaHangDB;
        public ObservableCollection<PHIEUMUAHANG> DSPhieuMuaHangDB { get => _DSPhieuMuaHangDB; set { _DSPhieuMuaHangDB = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUMUAHANG> _DSPhieuMuaHang;
        public IEnumerable<PHIEUMUAHANG> DSPhieuMuaHang { get => _DSPhieuMuaHang; set { _DSPhieuMuaHang = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUMUAHANG> _DSHDDaChon;
        public ObservableCollection<PHIEUMUAHANG> DSHDDaChon { get => _DSHDDaChon; set { _DSHDDaChon = value; OnPropertyChanged(); } }
        #endregion

        #region thuộc tính binding

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private string _TongSLHD;
        public string TongSLHD { get => _TongSLHD; set { _TongSLHD = value; OnPropertyChanged(); } }

        private PHIEUMUAHANG _HDDaChon;
        public PHIEUMUAHANG HDDaChon { get => _HDDaChon; set { _HDDaChon = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        #region command
        public ICommand TimKiemCommand { get; set; }
        public ICommand ChiTietHDCommand { get; set; }
        public ICommand XoaHDCommand { get; set; }
        public ICommand ThemPhieuMHCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public RelayCommand<IList> SelectionChangedCommand { get; set; }
        #endregion
        public ListBuyProductViewModel()
        {
            #region command

            // Load trang sau mỗi lần click mở trang
            ReloadCommand = new RelayCommand<Object>((p) =>
            {
                if (status == 1)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                LayDSTuDatabase();
                LoadDSPhieuBanHang();
                status = 0;
            });

            // Lấy danh sách được chọn từ dataGrids
            SelectionChangedCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                try
                {
                    DSHDDaChon.Clear();
                    foreach (var item in items)
                        DSHDDaChon.Add((PHIEUMUAHANG)item);
                }
                catch (Exception)
                {
                }

            });

            // Chi tiết bill
            //ChiTietHDCommand = new RelayCommand<object>((p) =>
            //{
            //    if (HDDaChon == null)
            //    {
            //        return false;
            //    }
            //    return true;
            //}, (p) =>
            //{
            //    DetailSellProductWindow detailProduct = new DetailSellProductWindow();
            //    (detailProduct.DataContext as DetailSellProductViewModel).LoadChiTietPhieuBanHang(HDDaChon);
            //    detailProduct.ShowDialog();
            //    LayDSTuDatabase();
            //    LoadDSPhieuBanHang();
            //});

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) != "" && DSPhieuMuaHang != null)
                {
                    return true;
                }
                LoadDSPhieuBanHang();
                return false;
            }, (p) =>
            {
                try
                {
                    DSPhieuMuaHang = DSPhieuMuaHang.Where(x => (x.MaPhieuMuaHang.ToString().Contains(TimKiem.ToLower())
                                                        || x.NgayLapPhieuMua.Value.ToShortDateString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.NHACUNGCAP.TenNhaCungCap.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongSoLuongMua.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongThanhTien.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.NHANVIEN.TenNhanVien.ToLower().Contains(TimKiem.ToLower())));
                    TongSLHD = DSPhieuMuaHang.Count().ToString();
                }
                catch (Exception) { }
            });

            // Xóa bill
            XoaHDCommand = new RelayCommand<Object>((p) =>
            {
                if (DSHDDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                MessageBoxResult isExit = System.Windows.MessageBox.Show("Dữ liệu đã xóa không thể khôi phục, bạn có chắc chắn xóa?", "Xác nhận", MessageBoxButton.OKCancel);
                if (isExit == MessageBoxResult.OK)
                {
                    try
                    {
                        foreach (var HD in DSHDDaChon)
                        {
                            // Lấy ra danh sách tất cả CT_PBH của Hóa đơn
                            var DSChiTietHD = DataProvider.Ins.DB.CT_PMH.Where(x => x.MaPhieuMuaHang == HD.MaPhieuMuaHang);
                            // Xóa những chi tiết hóa đơn liên quan
                            foreach (var CTHD in DSChiTietHD)
                            {
                                DataProvider.Ins.DB.CT_PMH.Remove(CTHD);
                            }

                            // Xóa hóa đơn
                            DataProvider.Ins.DB.PHIEUMUAHANGs.Remove(HD);
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        MessageBox.Show("Xóa thành công");
                        LayDSTuDatabase();
                        LoadDSPhieuBanHang();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Xóa thất bại vui lòng thử lại");
                    }
                }
            });

            //Thêm phiếu mua hàng
            ThemPhieuMHCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                NewBillBuyProductWindow billBuyProductWindow = new NewBillBuyProductWindow();
                p.Hide();
                billBuyProductWindow.ShowDialog();
                p.Show();
                // Load lại dữ liệu
                ListBuyProductViewModel.status = 1;
            });
            #endregion 
        }
        public void LoadDSPhieuBanHang()
        {
            DSPhieuMuaHang = DSPhieuMuaHangDB;
            TongSLHD = DSPhieuMuaHang.Count().ToString();
        }
        public void LayDSTuDatabase()
        {
            DSPhieuMuaHangDB = new ObservableCollection<PHIEUMUAHANG>(DataProvider.Ins.DB.PHIEUMUAHANGs);
            DSHDDaChon = new ObservableCollection<PHIEUMUAHANG>();
        }
    }
}
