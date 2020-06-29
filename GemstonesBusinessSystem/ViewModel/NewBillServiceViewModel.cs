using GemstonesBusinessSystem.Model;
using GemstonesBusinessSystem.Utils;
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

        private DateTime _NgayTao;
        public DateTime NgayTao { get => _NgayTao; set { _NgayTao = value; OnPropertyChanged(); } }

        private string _TongThanhTien;
        public string TongThanhTien { get => _TongThanhTien; set { _TongThanhTien = value; OnPropertyChanged(); } }

        private string _TongTienTraTruoc;
        public string TongTienTraTruoc { get => _TongTienTraTruoc; set { _TongTienTraTruoc = value; OnPropertyChanged(); } }

        private string _TongTienConLai;
        public string TongTienConLai { get => _TongTienConLai; set { _TongTienConLai = value; OnPropertyChanged(); } }

        private PHIEUDICHVU _PhieuDichVuMoi;
        public PHIEUDICHVU PhieuDichVuMoi { get => _PhieuDichVuMoi; set { _PhieuDichVuMoi = value; OnPropertyChanged(); } }

        private ChiTietPDVModel _CTHDDaChon;
        public ChiTietPDVModel CTHDDaChon { get => _CTHDDaChon; set { _CTHDDaChon = value; OnPropertyChanged(); } }

        #endregion

        #region List binding

        private ObservableCollection<ChiTietPDVModel> _DSCTHDDichVu;
        public ObservableCollection<ChiTietPDVModel> DSCTHDDichVu { get => _DSCTHDDichVu; set { _DSCTHDDichVu = value; OnPropertyChanged(); } }

        //private ObservableCollection<Service> _service;
        //public ObservableCollection<Service> service { get => _service; set { _service = value; OnPropertyChanged(); } }

        private ObservableCollection<ChiTietPDVModel> _DSCTHDDaChon;
        public ObservableCollection<ChiTietPDVModel> DSCTHDDaChon { get => _DSCTHDDaChon; set { _DSCTHDDaChon = value; OnPropertyChanged(); } }

        private KHACHHANG _KHDaChon;
        public KHACHHANG KHDaChon { get => _KHDaChon; set { _KHDaChon = value; OnPropertyChanged(); } }

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
            XemChiTietCTHDCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (CTHDDaChon != null)
                {
                    //int MaCT_PDV = CTHDDaChon.MaCT_PDV;
                    //CT_PDV CT = new CT_PDV();
                    //CT.DICHVU = CTHDDaChon.DICHVU;
                    //CT.MaPhieuDichVu = CTHDDaChon.MaPhieuDichVu;
                    //CT.MaDichVu = CTHDDaChon.MaDichVu;
                    //CT.DonGiaDichVu = CTHDDaChon.DonGiaDichVu;
                    //CT.ChiPhiRieng = CTHDDaChon.ChiPhiRieng;
                    //CT.DonGiaDuocTinh = CTHDDaChon.DonGiaDuocTinh;
                    //CT.SoLuongDichVu = CTHDDaChon.SoLuongDichVu;
                    //CT.ThanhTien = CTHDDaChon.ThanhTien;
                    //CT.TienTraTruoc = CTHDDaChon.TienTraTruoc;
                    //CT.TienConlai = CTHDDaChon.TienConlai;
                    //CT.NgayGiao = CTHDDaChon.NgayGiao;
                    //CT.TinhTrangCT_PDV = CTHDDaChon.TinhTrangCT_PDV;
                    var ChiTiet = new ChiTietPDVModel();
                    ChiTiet.DICHVU = CTHDDaChon.DICHVU;
                    ChiTiet.MaDichVu = CTHDDaChon.MaDichVu;
                    ChiTiet.DonGiaDichVu = CTHDDaChon.DonGiaDichVu;
                    ChiTiet.ChiPhiRieng = CTHDDaChon.ChiPhiRieng;
                    ChiTiet.DonGiaDuocTinh = CTHDDaChon.DonGiaDuocTinh;
                    ChiTiet.SoLuongDichVu = CTHDDaChon.SoLuongDichVu;
                    ChiTiet.ThanhTien = CTHDDaChon.ThanhTien;
                    ChiTiet.TienTraTruoc = CTHDDaChon.TienTraTruoc;
                    ChiTiet.TienConlai = CTHDDaChon.TienConlai;
                    ChiTiet.NgayGiao = CTHDDaChon.NgayGiao;
                    ChiTiet.TinhTrangCT_PDV = CTHDDaChon.TinhTrangCT_PDV;

                    NewDetailBillServiceWindow newDetailBillServiceWindow = new NewDetailBillServiceWindow();
                    (newDetailBillServiceWindow.DataContext as NewDetailBillServiceViewModel).KhoiTaoGiaTri(ChiTiet);
                    newDetailBillServiceWindow.ShowDialog();
                    var CTHD = (newDetailBillServiceWindow.DataContext as NewDetailBillServiceViewModel).LayCTHDMoi();
                    if (CTHD != null)
                    {
                        //CTHDDaChon = DSCTHDDichVu.Where(x => x.MaCT_PDV == CTHDDaChon.MaCT_PDV).SingleOrDefault();
                        //DSCTHDDichVu.Remove(CTHDDaChon);
                        //CTHDDaChon = CT;
                        //DSCTHDDichVu.Add(CTHDDaChon);
                        ////CTHDDaChon = CT;
                        CTHDDaChon.DICHVU = CTHD.DICHVU;
                        CTHDDaChon.MaDichVu = CTHD.MaDichVu;
                        CTHDDaChon.DonGiaDichVu = CTHD.DonGiaDichVu;
                        CTHDDaChon.ChiPhiRieng = CTHD.ChiPhiRieng;
                        CTHDDaChon.DonGiaDuocTinh = CTHD.DonGiaDuocTinh;
                        CTHDDaChon.SoLuongDichVu = CTHD.SoLuongDichVu;
                        CTHDDaChon.ThanhTien = CTHD.ThanhTien;
                        CTHDDaChon.TienTraTruoc = CTHD.TienTraTruoc;
                        CTHDDaChon.TienConlai = CTHD.TienConlai;
                        CTHDDaChon.NgayGiao = CTHD.NgayGiao;
                        CTHDDaChon.TinhTrangCT_PDV = CTHD.TinhTrangCT_PDV;

                        CapNhatGiaTri();
                    }
                }
            });

            // Chọn nhiều bill => để xóa
            SelectionChangedCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                DSCTHDDaChon.Clear();
                foreach (var item in items)
                    DSCTHDDaChon.Add((ChiTietPDVModel)item);
            });

            // Thêm 1 chi tiết dịch vụ mới
            ThemCTHDCommand = new RelayCommand<Window>((items) =>
            {
                return true;
            }, (items) =>
            {
                NewDetailBillServiceWindow newDetailBillServiceWindow = new NewDetailBillServiceWindow();
                (newDetailBillServiceWindow.DataContext as NewDetailBillServiceViewModel).KhoiTaoGiaTri();
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
                Choose chooseCustomerWindow = new Choose();
                chooseCustomerWindow.ShowDialog();
                KHDaChon = (chooseCustomerWindow.DataContext as ChooseCustomerViewModel).LayKhachHangHienTai();
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
                        List<ChiTietPDVModel> elements = new List<ChiTietPDVModel>();
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
                    catch (Exception)
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
                if (Utils.ConvertUtils.convertString(PhieuDichVuMoi.MaKhachHang.ToString()) != "" && DSCTHDDichVu != null)
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
                //PhieuDichVuMoi.NgayLapPhieuDichVu = NgayTao;
                PhieuDichVuMoi.MaNhanVien = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MaNhanVien == LoginViewModel.GetIdInfo).FirstOrDefault().MaNhanVien;
                DataProvider.Ins.DB.PHIEUDICHVUs.Add(PhieuDichVuMoi);
                DataProvider.Ins.DB.SaveChanges();
                foreach (var CTHDModel in DSCTHDDichVu)
                {
                    CT_PDV CTHD = new CT_PDV()
                    {
                        MaDichVu = CTHDModel.MaDichVu,
                        DonGiaDichVu = CTHDModel.DonGiaDichVu,
                        ChiPhiRieng = CTHDModel.ChiPhiRieng,
                        DonGiaDuocTinh = CTHDModel.DonGiaDuocTinh,
                        SoLuongDichVu = CTHDModel.SoLuongDichVu,
                        ThanhTien = CTHDModel.ThanhTien,
                        TienTraTruoc = CTHDModel.TienTraTruoc,
                        TienConlai = CTHDModel.TienConlai,
                        NgayGiao = CTHDModel.NgayGiao,
                        TinhTrangCT_PDV = CTHDModel.TinhTrangCT_PDV
                    };
                    CTHD.MaPhieuDichVu = PhieuDichVuMoi.MaPhieuDichVu;

                    DataProvider.Ins.DB.CT_PDV.Add(CTHD);
                    DataProvider.Ins.DB.SaveChanges();
                }

                var KH = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == KHDaChon.MaKhachHang).FirstOrDefault();
                KH.TongTienMuaKH += PhieuDichVuMoi.TongThanhTien;
                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Thêm thành công!");
                p.Close();

            });
            #endregion
        }

        public void KhoiTaoGiaTri()
        {
            DSCTHDDichVu = new ObservableCollection<ChiTietPDVModel>();
            DSCTHDDaChon = new ObservableCollection<ChiTietPDVModel>();

            PhieuDichVuMoi = new PHIEUDICHVU();
            TenKhachHang = "";
            SDTKhachHang = "";
            //PhieuDichVuMoi.NgayLapPhieuDichVu = DateTime.Now.Date;
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

            NgayTao = DateTime.Now.Date;
            TongThanhTien = ConvertUtils.convertDoubleToMoney(PhieuDichVuMoi.TongThanhTien.Value);
            TongTienTraTruoc = ConvertUtils.convertDoubleToMoney(PhieuDichVuMoi.TongTienTraTruoc.Value);
            TongTienConLai = ConvertUtils.convertDoubleToMoney(PhieuDichVuMoi.TongTienConLai.Value);
        }
    }
}

