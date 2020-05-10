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
    class ListBillServiceViewModel : BaseViewModel
    {
        #region Binding
        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private string _TongSLHD;
        public string TongSLHD { get => _TongSLHD; set { _TongSLHD = value; OnPropertyChanged(); } }

        private string _TongSLHDHoanThanh;
        public string TongSLHDHoanThanh { get => _TongSLHDHoanThanh; set { _TongSLHDHoanThanh = value; OnPropertyChanged(); } }

        private string _TongSLHDChuaHoanThanh;
        public string TongSLHDChuaHoanThanh { get => _TongSLHDChuaHoanThanh; set { _TongSLHDChuaHoanThanh = value; OnPropertyChanged(); } }

        private PHIEUDICHVU _PhieuDVDaChon;
        public PHIEUDICHVU PhieuDVDaChon { get => _PhieuDVDaChon; set { _PhieuDVDaChon = value; OnPropertyChanged(); } }

        public static int status;

        #endregion

        #region List Binding

        private IEnumerable<PHIEUDICHVU> _DSPhieuDVDB;
        public IEnumerable<PHIEUDICHVU> DVPhieuDVDB { get => _DSPhieuDVDB; set { _DSPhieuDVDB = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUDICHVU> _DVPhieuDV;
        public IEnumerable<PHIEUDICHVU> DSPhieuDV { get => _DVPhieuDV; set { _DVPhieuDV = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUDICHVU> _DSPhieuDVHoanThanh;
        public IEnumerable<PHIEUDICHVU> DSPhieuDVHoanThanh { get => _DSPhieuDVHoanThanh; set { _DSPhieuDVHoanThanh = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUDICHVU> _DSPhieuDVChuaHoanThanh;
        public IEnumerable<PHIEUDICHVU> DSPhieuDVChuaHoanThanh { get => _DSPhieuDVChuaHoanThanh; set { _DSPhieuDVChuaHoanThanh = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUDICHVU> _DSPhieuDVDaChon;
        public ObservableCollection<PHIEUDICHVU> DSPhieuDVDaChon { get => _DSPhieuDVDaChon; set { _DSPhieuDVDaChon = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand TimKiemCommand { get; set; }

        public ICommand XoaPhieuDVCommand { get; set; }

        public ICommand ThemPhieuDVCommand { get; set; }

        public ICommand ChiTietCommand { get; set; }

        public ICommand ReloadCommand { get; set; }

        public RelayCommand<IList> SelectionChangedCommand { get; set; }
        #endregion

        public ListBillServiceViewModel()
        {

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
                DSPhieuDVDaChon = new ObservableCollection<PHIEUDICHVU>();
                LayDSTuDatabase();
                LoadDSPhieuDV();
                LoadDSPhieuDVHoanThanh();
                LoadDSPhieuDVChuaHoanThanh();
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
                    DSPhieuDVDaChon.Clear();
                    foreach (var item in items)
                        DSPhieuDVDaChon.Add((PHIEUDICHVU)item);
                }
                catch (Exception)
                {
                }
            });

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertString.convertString(TimKiem) != "" && DSPhieuDV != null)
                {
                    return true;
                }
                else
                {
                    LoadDSPhieuDV();
                    LoadDSPhieuDVHoanThanh();
                    LoadDSPhieuDVChuaHoanThanh();
                    return false;
                }
            }, (p) =>
            {
                try
                {
                    TimKiem.ToLower();
                    // Search tất cả bill
                    DSPhieuDV = DSPhieuDV.Where(x => (x.MaPhieuDichVu.ToString().Contains(TimKiem.ToLower())
                                                        || x.KHACHHANG.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongSoLuongDichVu.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.NgayLapPhieuDichVu.Value.ToShortDateString().ToLower().Contains(TimKiem.ToLower())));
                    TongSLHD = DSPhieuDV.Count().ToString();

                    // Search bill hoàn thành
                    DSPhieuDVHoanThanh = DSPhieuDVHoanThanh.Where(x => (x.MaPhieuDichVu.ToString().Contains(TimKiem.ToLower())
                                                        || x.KHACHHANG.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongSoLuongDichVu.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.NgayLapPhieuDichVu.Value.ToShortDateString().ToLower().Contains(TimKiem.ToLower())));
                    TongSLHDHoanThanh = DSPhieuDVHoanThanh.Count().ToString();
                    // Search bill chưa hoàn thành

                    DSPhieuDVChuaHoanThanh = DSPhieuDVChuaHoanThanh.Where(x => (x.MaPhieuDichVu.ToString().Contains(TimKiem.ToLower())
                                                        || x.KHACHHANG.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongSoLuongDichVu.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.NgayLapPhieuDichVu.Value.ToShortDateString().ToLower().Contains(TimKiem.ToLower())));
                    TongSLHDChuaHoanThanh = DSPhieuDVChuaHoanThanh.Count().ToString();
                }
                catch (Exception) { }
            });

            XoaPhieuDVCommand = new RelayCommand<Object>((p) =>
            {
                if (DSPhieuDVDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn có chắc chắn xóa bill bạn vừa chọn ?", "Xác nhận", MessageBoxButton.OKCancel);
                if (isExit == MessageBoxResult.OK)
                {
                    try
                    {
                        foreach (var PhieuDV in DSPhieuDVDaChon)
                        {
                            DataProvider.Ins.DB.PHIEUDICHVUs.Remove(PhieuDV);

                            var DSCT_PDV = DataProvider.Ins.DB.CT_PDV.Where(x => x.MaPhieuDichVu == PhieuDV.MaPhieuDichVu);
                            foreach (var i in DSCT_PDV)
                            {
                                DataProvider.Ins.DB.CT_PDV.Remove(i);
                            }
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        MessageBox.Show("Xóa bill thành công");
                        LayDSTuDatabase();
                        LoadDSPhieuDV();
                        LoadDSPhieuDVChuaHoanThanh();
                        LoadDSPhieuDVHoanThanh();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Xóa bill thất bại vui lòng thử lại");
                    }
                }

            });

            //Thêm phiếu dịch vụ
            ThemPhieuDVCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                NewBillServiceWindow billServiceWindow = new NewBillServiceWindow();
                p.Hide();
                billServiceWindow.ShowDialog();
                p.Show();
                // Load lại dữ liệu
                ListBillServiceViewModel.status = 1;
            });



        }
        public void LayDSTuDatabase()
        {
            DVPhieuDVDB = new ObservableCollection<PHIEUDICHVU>(DataProvider.Ins.DB.PHIEUDICHVUs);
        }

        public void LoadDSPhieuDV()
        {
            try
            {
                PhieuDVDaChon = null;
                DSPhieuDV = DVPhieuDVDB;
                TongSLHD = DSPhieuDV.Count().ToString();
            }
            catch (Exception){}
        }

        public void LoadDSPhieuDVHoanThanh()
        {
            try
            {
                PhieuDVDaChon = null;
                DSPhieuDVHoanThanh = DVPhieuDVDB.Where(x => x.TinhTrangPDV == Constant.ACTIVE_STATUS);
                TongSLHDHoanThanh = DSPhieuDVHoanThanh.Count().ToString();
            }
            catch (Exception){}
        }
        public void LoadDSPhieuDVChuaHoanThanh()
        {
            try
            {
                PhieuDVDaChon = null;
                DSPhieuDVChuaHoanThanh = DVPhieuDVDB.Where(x => x.TinhTrangPDV == Constant.INACTIVE_STATUS);
                TongSLHDChuaHoanThanh = DSPhieuDVChuaHoanThanh.Count().ToString();
            }
            catch (Exception) {}
           
        }
    }
}
