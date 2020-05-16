using GemstonesBusinessSystem.Model;
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
    class DetailCustomerViewModel : BaseViewModel
    {
        #region list binding

        //private ObservableCollection<Product_Customer> _detaiLBillSell;
        //public ObservableCollection<Product_Customer> detaiLBillSell { get => _detaiLBillSell; set { _detaiLBillSell = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUBANHANG> _DSPBHKhachHang;
        public ObservableCollection<PHIEUBANHANG> DSPBHKhachHang { get => _DSPBHKhachHang; set { _DSPBHKhachHang = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUDICHVU> _DSPDVKhachHang;
        public ObservableCollection<PHIEUDICHVU> DSPDVKhachHang { get => _DSPDVKhachHang; set { _DSPDVKhachHang = value; OnPropertyChanged(); } }


        #endregion

        #region thuộc tính binding
        private KHACHHANG _CTKhachHang;
        public KHACHHANG CTKhachHang { get => _CTKhachHang; set { _CTKhachHang = value; OnPropertyChanged(); } }

        

        private string _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }

        private string _DiaChiKH;
        public string DiaChiKH { get => _DiaChiKH; set { _DiaChiKH = value; OnPropertyChanged(); } }

        private string _SoDienThoaiKH;
        public string SoDienThoaiKH { get => _SoDienThoaiKH; set { _SoDienThoaiKH = value; OnPropertyChanged(); } }

        private string _EmailKH;
        public string EmailKH { get => _EmailKH; set { _EmailKH = value; OnPropertyChanged(); } }



        private PHIEUBANHANG _PBHDaChon;
        public PHIEUBANHANG PBHDaChon { get => _PBHDaChon; set { _PBHDaChon = value; OnPropertyChanged(); } }

        private PHIEUDICHVU _PDVDaChon;
        public PHIEUDICHVU PDVDaChon { get => _PDVDaChon; set { _PDVDaChon = value; OnPropertyChanged(); } }
        #endregion

        #region command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand ChiTietPBHCommand { get; set; }
        public ICommand ChiTietPDVCommand { get; set; }

        #endregion
        public DetailCustomerViewModel()
        {

            #region command

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertUtils.convertString(CTKhachHang.TenKhachHang) != ""
                    && Utils.ConvertUtils.convertString(CTKhachHang.DiaChiKH) != ""
                    && Utils.ConvertUtils.convertString(CTKhachHang.SoDienThoaiKH) != ""
                    && Utils.ConvertUtils.convertString(CTKhachHang.EmailKH) != "")
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
                    KHACHHANG KhachHang = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == CTKhachHang.MaKhachHang).SingleOrDefault();
                    KhachHang = CTKhachHang;
                    KhachHang.TenKhachHang = TenKhachHang;
                    KhachHang.DiaChiKH = DiaChiKH;
                    KhachHang.SoDienThoaiKH = SoDienThoaiKH;
                    KhachHang.EmailKH = EmailKH;


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
            ChiTietPBHCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DetailSellProductWindow detailProduct = new DetailSellProductWindow();
                (detailProduct.DataContext as DetailSellProductViewModel).LoadChiTietPhieuBanHang(PBHDaChon);
                detailProduct.ShowDialog();
            });

            // Chi tiết hóa đơn dịch vụ
            ChiTietPDVCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DetailBillServiceWindow Chitiet = new DetailBillServiceWindow();
                (Chitiet.DataContext as DetailBillServiceViewModel).LoadChiTietPhieuDichVu(PDVDaChon);
                Chitiet.ShowDialog();
            });
            #endregion
        }
        public void LayDSTuDatabase()
        {
            try
            {
                DSPBHKhachHang = new ObservableCollection<PHIEUBANHANG>(DataProvider.Ins.DB.PHIEUBANHANGs.Where(x => x.MaKhachHang == CTKhachHang.MaKhachHang));
                DSPDVKhachHang = new ObservableCollection<PHIEUDICHVU>(DataProvider.Ins.DB.PHIEUDICHVUs.Where(x => x.MaKhachHang == CTKhachHang.MaKhachHang));
            }
            catch (Exception){ }
        }
        public void LoadDuLieuTuKhachHang(KHACHHANG KhachHang)
        {
            CTKhachHang = KhachHang;
            TenKhachHang = CTKhachHang.TenKhachHang;
            DiaChiKH = CTKhachHang.DiaChiKH;
            SoDienThoaiKH = CTKhachHang.SoDienThoaiKH;
            EmailKH = CTKhachHang.EmailKH;

            LayDSTuDatabase();
        }
    }
}
