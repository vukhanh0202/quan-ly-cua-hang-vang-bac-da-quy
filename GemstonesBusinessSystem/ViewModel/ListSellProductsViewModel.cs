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
    class ListSellProductsViewModel : BaseViewModel
    {
        #region List binding
        private ObservableCollection<PHIEUBANHANG> _DSPhieuBanHangDB;
        public ObservableCollection<PHIEUBANHANG> DSPhieuBanHangDB { get => _DSPhieuBanHangDB; set { _DSPhieuBanHangDB = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUBANHANG> _DSPhieuBanHang;
        public IEnumerable<PHIEUBANHANG> DSPhieuBanHang { get => _DSPhieuBanHang; set { _DSPhieuBanHang = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUBANHANG> _DSHDDaChon;
        public ObservableCollection<PHIEUBANHANG> DSHDDaChon { get => _DSHDDaChon; set { _DSHDDaChon = value; OnPropertyChanged(); } }
        #endregion

        #region thuộc tính binding

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private string _TongSLHD;
        public string TongSLHD { get => _TongSLHD; set { _TongSLHD = value; OnPropertyChanged(); } }

        private PHIEUBANHANG _HDDaChon;
        public PHIEUBANHANG HDDaChon { get => _HDDaChon; set { _HDDaChon = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        #region command
        public ICommand TimKiemCommand { get; set; }
        public ICommand ChiTietHDCommand { get; set; }
        public ICommand XoaHDCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public RelayCommand<IList> SelectionChangedCommand { get; set; }
        #endregion
        public ListSellProductsViewModel()
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
                        DSHDDaChon.Add((PHIEUBANHANG)item);
                }
                catch (Exception)
                {
                }

            });

            // Chi tiết bill
            ChiTietHDCommand = new RelayCommand<object>((p) =>
            {
                if (HDDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                DetailSellProductWindow detailProduct = new DetailSellProductWindow();
                (detailProduct.DataContext as DetailSellProductViewModel).LoadChiTietPhieuBanHang(HDDaChon);
                detailProduct.ShowDialog();
                LayDSTuDatabase();
                LoadDSPhieuBanHang();
            });

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertString.convertString(TimKiem) != "" && DSPhieuBanHang != null)
                {
                    return true;
                }
                LoadDSPhieuBanHang();
                return false;
            }, (p) =>
            {
                try
                {
                    DSPhieuBanHang = DSPhieuBanHang.Where(x => (x.MaPhieuBanHang.ToString().Contains(TimKiem.ToLower())
                                                        || x.NgayLapPhieuBan.Value.ToShortDateString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.KHACHHANG.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongSoLuongBan.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongThanhTien.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.NHANVIEN.TenNhanVien.ToLower().Contains(TimKiem.ToLower())));
                    TongSLHD = DSPhieuBanHang.Count().ToString();
                }
                catch (Exception e) { }
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
                            var DSChiTietHD = DataProvider.Ins.DB.CT_PBH.Where(x => x.MaPhieuBanHang == HD.MaPhieuBanHang);
                            // Xóa những chi tiết hóa đơn liên quan
                            foreach (var CTHD in DSChiTietHD)
                            {
                                DataProvider.Ins.DB.CT_PBH.Remove(CTHD);
                            }

                            // Xóa hóa đơn
                            DataProvider.Ins.DB.PHIEUBANHANGs.Remove(HD);
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
            #endregion 
        }
        public void LoadDSPhieuBanHang()
        {
            //DSPhieuBanHang = (from bill in billSellProducts
            //                    join customer in customers on bill.customer_id equals customer.id
            //                    select new BillSellProductModel()
            //                    {
            //                        id = bill.id,
            //                        customerId = (int)bill.customer_id,
            //                        customerName = customer.name,
            //                        totalPrice = (double)bill.totalPrice,
            //                        totalQuantity = (int)bill.totalQuantity,
            //                        //createdBy = Utils.ConvertString.convertString(bill.create_by),
            //                        createdDate = (DateTime)bill.create_date
            //                    });
            DSPhieuBanHang = DSPhieuBanHangDB;
            TongSLHD = DSPhieuBanHang.Count().ToString();
           
        }
        public void LayDSTuDatabase()
        {
            DSPhieuBanHangDB = new ObservableCollection<PHIEUBANHANG>(DataProvider.Ins.DB.PHIEUBANHANGs);
            DSHDDaChon = new ObservableCollection<PHIEUBANHANG>();
        }
    }
}
