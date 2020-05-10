using GemstonesBusinessSystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class NewBillServiceViewModel : BaseViewModel
    {
        #region Binding

        private string _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }

        private string _SDTKhachHang;
        public string SDTKhachHang { get => _SDTKhachHang; set { _SDTKhachHang = value; OnPropertyChanged(); } }

        private string _NgayTao;
        public string NgayTao { get => _NgayTao; set { _NgayTao = value; OnPropertyChanged(); } }

        private string _TongThanhTien;
        public string TongThanhTien { get => _TongThanhTien; set { _TongThanhTien = value; OnPropertyChanged(); } }

        private string _TongTienTraTruoc;
        public string TongTienTraTruoc { get => _TongTienTraTruoc; set { _TongTienTraTruoc = value; OnPropertyChanged(); } }

        private string _TongTienConLai;
        public string TongTienConLai { get => _TongTienConLai; set { _TongTienConLai = value; OnPropertyChanged(); } }

        private PHIEUDICHVU _PhieuDichVuMoi;
        public PHIEUDICHVU PhieuDichVuMoi { get => _PhieuDichVuMoi; set { _PhieuDichVuMoi = value; OnPropertyChanged(); } }

        private CT_PDV _CTHDDaChon;
        public CT_PDV CTHDDaChon { get => _CTHDDaChon; set { _CTHDDaChon = value; OnPropertyChanged(); } }

        #endregion

        #region List binding

        private ObservableCollection<CT_PDV> _DSCTHDDichVu;
        public ObservableCollection<CT_PDV> DSCTHDDichVu { get => _DSCTHDDichVu; set { _DSCTHDDichVu = value; OnPropertyChanged(); } }

        //private ObservableCollection<Service> _service;
        //public ObservableCollection<Service> service { get => _service; set { _service = value; OnPropertyChanged(); } }

        private ObservableCollection<CT_PDV> _DSCTHDDaChon;
        public ObservableCollection<CT_PDV> DSCTHDDaChon { get => _DSCTHDDaChon; set { _DSCTHDDaChon = value; OnPropertyChanged(); } }


        #endregion

        #region Command
        public ICommand XoaCTHDCommand { get; set; }

        public ICommand ThemCTHDCommand { get; set; }

        public ICommand XemChiTietCTHDCommand { get; set; }

        public ICommand HuyBoCommand { get; set; }

        public ICommand XacNhanCommand { get; set; }

        public ICommand LoadedWindowCommand { get; set; }

        public ICommand CapNhatKHCommand { get; set; }

        public RelayCommand<IList> SelectionChangedCommand { get; set; }
        #endregion


        public NewBillServiceViewModel()
        {

            #region Command

            // Command khi load vào window
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                KhoiTaoGiaTri();
                CapNhatGiaTri();
            });

            // Chi tiết bill
            //DetailServiceCustomerCommand = new RelayCommand<Object>((p) =>
            //{
            //    return true;
            //}, (p) =>
            //{
            //    DetailServiceCustomerWindow detailServiceCustomerWindow = new DetailServiceCustomerWindow();
            //    (detailServiceCustomerWindow.DataContext as DetailServiceCustomerViewModel).LoadServiceCustomer(selectedDetail);
            //    detailServiceCustomerWindow.ShowDialog();
            //    var temp = (detailServiceCustomerWindow.DataContext as DetailServiceCustomerViewModel).getDetailBillService();
            //    if (temp != null)
            //    {
            //        listService.Remove(selectedDetail);
            //        listService.Add(temp);
            //        Load();
            //    }
            //});

            // Chọn nhiều bill => để xóa
            SelectionChangedCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                DSCTHDDaChon.Clear();
                foreach (var item in items)
                    DSCTHDDaChon.Add((CT_PDV)item);
            });

            // Thêm 1 dịch vụ mới
            ThemCTHDCommand = new RelayCommand<Window>((items) =>
            {
                return true;
            }, (items) =>
            {
                NewDetailBillServiceWindow newDetailBillServiceWindow = new NewDetailBillServiceWindow();
                newDetailBillServiceWindow.ShowDialog();
                var CTHD = (newDetailBillServiceWindow.DataContext as NewDetailBillServiceViewModel).LayCTHDMoi();
                if (CTHD != null)
                {
                    DSCTHDDichVu.Add(CTHD);
                    CapNhatGiaTri();
                }
            });

            // Thêm hoặc cập nhật khách hàng
            CapNhatKHCommand = new RelayCommand<Window>((items) =>
            {
                return true;
            }, (items) =>
            {
                ChooseCustomerWindow chooseCustomerWindow = new ChooseCustomerWindow();
                chooseCustomerWindow.ShowDialog();
                var KHDaChon = (chooseCustomerWindow.DataContext as ChooseCustomerViewModel).LayKhachHangHienTai();
                if (KHDaChon != null)
                {
                    PhieuDichVuMoi.MaKhachHang = KHDaChon.MaKhachHang;
                    PhieuDichVuMoi.KHACHHANG = KHDaChon;
                    TenKhachHang = PhieuDichVuMoi.KHACHHANG.TenKhachHang;
                    SDTKhachHang = PhieuDichVuMoi.KHACHHANG.SoDienThoaiKH;
                }
            });


            // Xóa 1 dịch vụ trong bill
            XoaCTHDCommand = new RelayCommand<Object>((p) =>
            {
                if (DSCTHDDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn có chắc chắn xóa dịch vụ khỏi danh sách?", "Xác nhận", MessageBoxButton.OKCancel);
                if (isExit == MessageBoxResult.OK)
                {
                    try
                    {
                        List<CT_PDV> elements = new List<CT_PDV>();
                        foreach (var item in DSCTHDDaChon)
                        {
                            elements.Add(item);
                        }
                        
                        foreach (var CTHD in elements)
                        {
                            DSCTHDDichVu.Remove(CTHD);
                        }
                        CapNhatGiaTri();
                        MessageBox.Show("Xóa dịch vụ thành công");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Xóa dịch vụ thất bại vui lòng thử lại");
                    }
                }
            });

            // Hủy
            HuyBoCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

            // Xác nhận 
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertString.convertString(PhieuDichVuMoi.MaKhachHang.ToString()) != "" && DSCTHDDichVu != null)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Bạn đã nhập thiếu thông tin !");
                    return false;
                }
            }, (p) =>
            {
                PhieuDichVuMoi.TinhTrangPDV = Constant.INACTIVE_STATUS;
                DataProvider.Ins.DB.PHIEUDICHVUs.Add(PhieuDichVuMoi);
                DataProvider.Ins.DB.SaveChanges();
                foreach (var CTHD in DSCTHDDichVu)
                {
                    CTHD.MaPhieuDichVu = PhieuDichVuMoi.MaPhieuDichVu;
                    DataProvider.Ins.DB.CT_PDV.Add(CTHD);
                    DataProvider.Ins.DB.SaveChanges();
                }
                MessageBox.Show("Thêm thành công!");
                p.Close();

            });
            #endregion
        }

        public void KhoiTaoGiaTri()
        {
            DSCTHDDichVu = new ObservableCollection<CT_PDV>();
            DSCTHDDaChon = new ObservableCollection<CT_PDV>();

            PhieuDichVuMoi = new PHIEUDICHVU();
            PhieuDichVuMoi.NgayLapPhieuDichVu = DateTime.Now.Date;
            PhieuDichVuMoi.TongThanhTien = 0;
            PhieuDichVuMoi.TongTienTraTruoc = 0;
            PhieuDichVuMoi.TongTienConLai = 0;
        }
        public void CapNhatGiaTri()
        {
            PhieuDichVuMoi.TongThanhTien = 0;
            PhieuDichVuMoi.TongTienTraTruoc = 0;
            PhieuDichVuMoi.TongTienConLai = 0;
            PhieuDichVuMoi.TongSoLuongDichVu = 0;
            foreach (var CTHD in DSCTHDDichVu)
            {
                PhieuDichVuMoi.TongThanhTien += CTHD.ThanhTien;
                PhieuDichVuMoi.TongTienTraTruoc += CTHD.TienTraTruoc;
                PhieuDichVuMoi.TongTienConLai += CTHD.TienConlai;
                PhieuDichVuMoi.TongSoLuongDichVu += CTHD.SoLuongDichVu;
            }

            NgayTao = PhieuDichVuMoi.NgayLapPhieuDichVu.Value.ToShortDateString();
            TongThanhTien = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", PhieuDichVuMoi.TongThanhTien.Value);
            TongTienTraTruoc = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", PhieuDichVuMoi.TongTienTraTruoc.Value);
            TongTienConLai = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", PhieuDichVuMoi.TongTienConLai.Value);
        }
    }
}

