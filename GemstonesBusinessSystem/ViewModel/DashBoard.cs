using GemstonesBusinessSystem.Model;
using GemstonesBusinessSystem.Utils;
using LiveCharts;
using LiveCharts.Wpf;
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
    class DashBoard : BaseViewModel
    {
        #region List binding
        //private IEnumerable<ProductTypeModel> _listProduct;
        //public IEnumerable<ProductTypeModel> listProduct { get => _listProduct; set { _listProduct = value; OnPropertyChanged(); } }

        //private ObservableCollection<SANPHAM> _DSSanPhamDB;
        //public ObservableCollection<SANPHAM> DSSanPhamDB { get => _DSSanPhamDB; set { _DSSanPhamDB = value; OnPropertyChanged(); } }

        //private IEnumerable<SANPHAM> _DSSanPham;
        //public IEnumerable<SANPHAM> DSSanPham { get => _DSSanPham; set { _DSSanPham = value; OnPropertyChanged(); } }

        ////private ObservableCollection<LOAISANPHAM> _DSLoaiSanPham;
        ////public ObservableCollection<LOAISANPHAM> DSLoaiSanPham { get => _DSLoaiSanPham; set { _DSLoaiSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<Consumo> _DoanhSoChart;
        public ObservableCollection<Consumo> DoanhSoChart { get => _DoanhSoChart; set { _DoanhSoChart = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUBANHANG> _DSPhieuBanHangDB;
        public ObservableCollection<PHIEUBANHANG> DSPhieuBanHangDB { get => _DSPhieuBanHangDB; set { _DSPhieuBanHangDB = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUDICHVU> _DSPhieuDichVuDB;
        public ObservableCollection<PHIEUDICHVU> DSPhieuDichVuDB { get => _DSPhieuDichVuDB; set { _DSPhieuDichVuDB = value; OnPropertyChanged(); } }

        private SeriesCollection _ChartLoiNhuan;
        public SeriesCollection ChartLoiNhuan { get => _ChartLoiNhuan; set { _ChartLoiNhuan = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _DSNam;
        public ObservableCollection<string> DSNam { get => _DSNam; set { _DSNam = value; OnPropertyChanged(); } }

        private SeriesCollection _ChartTopSP;
        public SeriesCollection ChartTopSP { get => _ChartTopSP; set { _ChartTopSP = value; OnPropertyChanged(); } }

        #endregion

        #region Thuộc tính Binding
        private int _TongSanPham;
        public int TongSanPham { get => _TongSanPham; set { _TongSanPham = value; OnPropertyChanged(); } }

        private int _TongDichVu;
        public int TongDichVu { get => _TongDichVu; set { _TongDichVu = value; OnPropertyChanged(); } }

        private int _TongNhanVien;
        public int TongNhanVien { get => _TongNhanVien; set { _TongNhanVien = value; OnPropertyChanged(); } }

        private int _TongNCC;
        public int TongNCC { get => _TongNCC; set { _TongNCC = value; OnPropertyChanged(); } }

        private double _TongThuVe;
        public double TongThuVe { get => _TongThuVe; set { _TongThuVe = value; OnPropertyChanged(); } }

        private double _SoSPBanRa;
        public double SoSPBanRa { get => _SoSPBanRa; set { _SoSPBanRa = value; OnPropertyChanged(); } }

        private double _TienSPBanRa;
        public double TienSPBanRa { get => _TienSPBanRa; set { _TienSPBanRa = value; OnPropertyChanged(); } }

        private double _SoDVThucHien;
        public double SoDVThucHien { get => _SoDVThucHien; set { _SoDVThucHien = value; OnPropertyChanged(); } }

        private double _TienDVThucHien;
        public double TienDVThucHien { get => _TienDVThucHien; set { _TienDVThucHien = value; OnPropertyChanged(); } }

        private double _TongDoanhThuSPChart;
        public double TongDoanhThuSPChart { get => _TongDoanhThuSPChart; set { _TongDoanhThuSPChart = value; OnPropertyChanged(); } }

        private double _TongDoanhThuDVChart;
        public double TongDoanhThuDVChart { get => _TongDoanhThuDVChart; set { _TongDoanhThuDVChart = value; OnPropertyChanged(); } }

        private DateTime _NgayBatDauDoanhSo;
        public DateTime NgayBatDauDoanhSo { get => _NgayBatDauDoanhSo; set { _NgayBatDauDoanhSo = value; OnPropertyChanged(); } }

        private DateTime _NgayKetThucDoanhSo;
        public DateTime NgayKetThucDoanhSo { get => _NgayKetThucDoanhSo; set { _NgayKetThucDoanhSo = value; OnPropertyChanged(); } }

        private string _NamDaChon;
        public string NamDaChon { get => _NamDaChon; set { _NamDaChon = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        #region Command

        public ICommand ToanBoDoanhSoCommand { get; set; }
        public ICommand TuyChinhDoanhSoCommand { get; set; }

        public ICommand Filter { get; set; }

        //public ICommand TimKiemCommand { get; set; }
        //public ICommand XoaSanPhamCommand { get; set; }
        //public ICommand ThemSanPhamCommand { get; set; }
        //public ICommand LoaiSanPhamCommand { get; set; }
        //public ICommand ChiTietSPCommand { get; set; }
        //public ICommand DSHoatDongCommand { get; set; }
        //public ICommand DSNgungHoatDongCommand { get; set; }
        //public ICommand DVTCommand { get; set; }
        //public ICommand KhoiPhucSanPhamCommand { get; set; }
        public ICommand ReloadCommand { get; set; }

        #endregion

        public string[] LabelsLoiNhuan { get; set; }
        public Func<double, string> FormatterLoiNhuan { get; set; }

        public string[] LabelsTopSP { get; set; }
        public Func<double, string> FormatterTopSP { get; set; }

        public DashBoard()
        {
            status = 1;
            #region Command
            LoadDuLieu();
            NgayBatDauDoanhSo = DateTime.Now.Date;
            NgayKetThucDoanhSo = DateTime.Now.Date;
            TongDoanhThuSPChart = DSPhieuBanHangDB.Sum(x => x.TongThanhTien).Value;
            TongDoanhThuDVChart = DSPhieuDichVuDB.Sum(x => x.TongThanhTien).Value;
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
                LoadDuLieu();
                NamDaChon = DSNam.Where(x => x == DateTime.Now.Date.Year.ToString()).FirstOrDefault();
                LoadDoanhSoChart();
                LoadLoiNhuanChart();
                LoadTopSPChart();
                status = 0;
            });

            // Doanh số toàn thời gian
            ToanBoDoanhSoCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    TongDoanhThuSPChart = DSPhieuBanHangDB.Sum(x => x.TongThanhTien).Value;
                    TongDoanhThuDVChart = DSPhieuDichVuDB.Sum(x => x.TongThanhTien).Value;
                    LoadDoanhSoChart();
                }
                catch (Exception)
                {
                }

            });

            // Doanh số tùy chỉnh
            TuyChinhDoanhSoCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    TongDoanhThuSPChart = DSPhieuBanHangDB.Where(x => x.NgayLapPhieuBan.Value.Date >= NgayBatDauDoanhSo.Date && x.NgayLapPhieuBan.Value.Date <= NgayKetThucDoanhSo.Date).Sum(x => x.TongThanhTien).Value;
                    TongDoanhThuDVChart = DSPhieuDichVuDB.Where(x => x.NgayLapPhieuDichVu.Value.Date >= NgayBatDauDoanhSo.Date && x.NgayLapPhieuDichVu.Value.Date <= NgayKetThucDoanhSo.Date).Sum(x => x.TongThanhTien).Value;
                    LoadDoanhSoChart();
                }
                catch (Exception)
                {
                }

            });


            // CHọn năm chart lợi nhuận
            Filter = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(NamDaChon) == "")
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                LoadLoiNhuanChart();
            });
            #endregion
        }


        public void LoadDuLieu()
        {
            DSPhieuBanHangDB = new ObservableCollection<PHIEUBANHANG>(DataProvider.Ins.DB.PHIEUBANHANGs);
            DSPhieuDichVuDB = new ObservableCollection<PHIEUDICHVU>(DataProvider.Ins.DB.PHIEUDICHVUs);

            DSNam = new ObservableCollection<string>();
            foreach (var item in DSPhieuBanHangDB)
            {
                if (!DSNam.Contains(item.NgayLapPhieuBan.Value.Year.ToString().Trim()))
                    DSNam.Add(item.NgayLapPhieuBan.Value.Year.ToString().Trim());
            }
            foreach (var item in DSPhieuDichVuDB)
            {
                if (!DSNam.Contains(item.NgayLapPhieuDichVu.Value.Year.ToString().Trim()))
                    DSNam.Add(item.NgayLapPhieuDichVu.Value.Year.ToString().Trim());
            }

            TongSanPham = DataProvider.Ins.DB.SANPHAMs.Where(x => x.TrangThaiSanPham == Constant.ACTIVE_STATUS).Count();
            TongDichVu = DataProvider.Ins.DB.DICHVUs.Where(x => x.TrangThaiDV == Constant.ACTIVE_STATUS).Count();

            // Cần thêm trạng thái
            TongNhanVien = DataProvider.Ins.DB.NHANVIENs.Count();

            TongNCC = DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.TrangThaiNCC == Constant.ACTIVE_STATUS).Count();

            SoSPBanRa = 0;
            SoDVThucHien = 0;
            TienSPBanRa = 0;
            TongThuVe = 0;

            try
            {
                SoSPBanRa = DataProvider.Ins.DB.PHIEUBANHANGs.Sum(x => x.TongSoLuongBan).Value;
            }
            catch (Exception)
            {
            }
            try
            {
                SoDVThucHien = DataProvider.Ins.DB.PHIEUDICHVUs.Sum(x => x.TongSoLuongDichVu).Value;
            }
            catch (Exception)
            {
            }
            try
            {
                TienSPBanRa = DataProvider.Ins.DB.PHIEUBANHANGs.Sum(x => x.TongThanhTien).Value;
            }
            catch (Exception)
            {
            }
            try
            {
                TienDVThucHien = DataProvider.Ins.DB.PHIEUDICHVUs.Sum(x => x.TongThanhTien).Value;
            }
            catch (Exception)
            {
            }

            TongThuVe = TienSPBanRa + TienDVThucHien;


        }
        public void LoadDoanhSoChart()
        {

            var TongDoanhThu = TongDoanhThuSPChart + TongDoanhThuDVChart;
            if (TongDoanhThu == 0)
            {
                TongDoanhThu = 1;
            }

            DoanhSoChart = new ObservableCollection<Consumo>();
            // Thêm thông tin về sản phẩm
            Consumo SP = new Consumo();
            SP.Percentage = Math.Truncate((double)(TongDoanhThuSPChart / TongDoanhThu) * 10000) / 100;
            SP.Titile = "Sản phẩm";
            if (SP.Percentage > 0)
            {
                DoanhSoChart.Add(SP);
            }

            // Thêm thông tin về dịch vụ
            Consumo DV = new Consumo();
            DV.Percentage = Math.Truncate((double)(TongDoanhThuDVChart / TongDoanhThu) * 10000) / 100;
            DV.Titile = "Dịch vụ";
            if (DV.Percentage > 0)
            {
                DoanhSoChart.Add(DV);
            }
        }

        public void LoadLoiNhuanChart()
        {
            ObservableCollection<CT_PBH> DSCT_PBH = new ObservableCollection<CT_PBH>(DataProvider.Ins.DB.CT_PBH.Where(x => x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year.ToString().Equals(NamDaChon)));
            //var SPQUY1 = DSCT_PBH.Where(x => x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month >= 1 && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month <= 3).Sum(x=>x.);
            double LoiNhuanSPQuy1 = 0;
            double LoiNhuanSPQuy2 = 0;
            double LoiNhuanSPQuy3 = 0;
            double LoiNhuanSPQuy4 = 0;
            foreach (var item in DSCT_PBH)
            {
                if (item.PHIEUBANHANG.NgayLapPhieuBan.Value.Month >= 1 && item.PHIEUBANHANG.NgayLapPhieuBan.Value.Month <= 3)
                {
                    LoiNhuanSPQuy1 += (item.ThanhTien - item.DonGiaNhapSPHienTai * item.SoLuongBan).Value;
                }
                else if (item.PHIEUBANHANG.NgayLapPhieuBan.Value.Month >= 4 && item.PHIEUBANHANG.NgayLapPhieuBan.Value.Month <= 6)
                {
                    LoiNhuanSPQuy2 += (item.ThanhTien - item.DonGiaNhapSPHienTai * item.SoLuongBan).Value;

                }
                else if (item.PHIEUBANHANG.NgayLapPhieuBan.Value.Month >= 7 && item.PHIEUBANHANG.NgayLapPhieuBan.Value.Month <= 9)
                {
                    LoiNhuanSPQuy3 += (item.ThanhTien - item.DonGiaNhapSPHienTai * item.SoLuongBan).Value;

                }
                else if (item.PHIEUBANHANG.NgayLapPhieuBan.Value.Month >= 10 && item.PHIEUBANHANG.NgayLapPhieuBan.Value.Month <= 12)
                {
                    LoiNhuanSPQuy4 += (item.ThanhTien - item.DonGiaNhapSPHienTai * item.SoLuongBan).Value;
                }
            }

            ObservableCollection<PHIEUDICHVU> DSPDV = new ObservableCollection<PHIEUDICHVU>(DataProvider.Ins.DB.PHIEUDICHVUs.Where(x => x.NgayLapPhieuDichVu.Value.Year.ToString().Equals(NamDaChon)));
            double LoiNhuanDVQuy1 = 0;
            double LoiNhuanDVQuy2 = 0;
            double LoiNhuanDVQuy3 = 0;
            double LoiNhuanDVQuy4 = 0;
            foreach (var item in DSPDV)
            {
                if (item.NgayLapPhieuDichVu.Value.Month >= 1 && item.NgayLapPhieuDichVu.Value.Month <= 3)
                {
                    LoiNhuanDVQuy1 += item.TongThanhTien.Value;
                }
                else if (item.NgayLapPhieuDichVu.Value.Month >= 4 && item.NgayLapPhieuDichVu.Value.Month <= 6)
                {
                    LoiNhuanDVQuy2 += item.TongThanhTien.Value;

                }
                else if (item.NgayLapPhieuDichVu.Value.Month >= 7 && item.NgayLapPhieuDichVu.Value.Month <= 9)
                {
                    LoiNhuanDVQuy3 += item.TongThanhTien.Value;

                }
                else if (item.NgayLapPhieuDichVu.Value.Month >= 10 && item.NgayLapPhieuDichVu.Value.Month <= 2)
                {
                    LoiNhuanDVQuy4 += item.TongThanhTien.Value;
                }
            }

            ChartLoiNhuan = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Sản phẩm",
                    Values = new ChartValues<double> { LoiNhuanSPQuy1, LoiNhuanSPQuy2, LoiNhuanSPQuy3 , LoiNhuanSPQuy4 }
                }
            };

            //adding series will update and animate the chart automatically
            ChartLoiNhuan.Add(new ColumnSeries
            {
                Title = "Dịch vụ",
                Values = new ChartValues<double> { LoiNhuanDVQuy1, LoiNhuanDVQuy2, LoiNhuanDVQuy3, LoiNhuanDVQuy4 }
            });

            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            LabelsLoiNhuan = new[] { "Quý 1", "Quý 2", "Quý 3", "Quý 4" };
            //Formatter = value => value.ToString("N");
            FormatterLoiNhuan = value => value.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN"));
        }
        public void LoadTopSPChart()
        {
            // Lấy ra thời gian 3 tháng gần nhất
            DateTime timeNow = DateTime.Now.Date;
            DateTime timeBeforeOneMonth = timeNow.AddMonths(-1);
            DateTime timeBeforeTwoMonth = timeNow.AddMonths(-2);

            // Chọn ra tất cả các chi tiết bill trong 3 tháng đó
            ObservableCollection<CT_PBH> DSCT_PBH = new ObservableCollection<CT_PBH>(DataProvider.Ins.DB.CT_PBH.Where(x =>
            (x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeNow.Year || x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeBeforeOneMonth.Year || x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeBeforeTwoMonth.Year)
            && (x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeNow.Month || x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeBeforeOneMonth.Month || x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeBeforeTwoMonth.Month)
            ));

            // Tìm ra top sản phẩm bán chạy nhất
            List<TopSP> DSTopSPBanChay = new List<TopSP>();
            foreach (var item in DSCT_PBH)
            {
                TopSP ItemSP = new TopSP() { TenSanPham = item.SANPHAM.TenSanPham, TongSoLuongBan = item.SoLuongBan.Value };
                var xx = DSTopSPBanChay.Where(x => x.TenSanPham.Equals(ItemSP.TenSanPham)).FirstOrDefault();
                if (DSTopSPBanChay.Contains(xx))
                {
                    var temp = DSTopSPBanChay.Where(x => x.TenSanPham == ItemSP.TenSanPham).FirstOrDefault();
                    temp.TongSoLuongBan += ItemSP.TongSoLuongBan;
                }
                else
                {
                    DSTopSPBanChay.Add(ItemSP);
                }
            }
            while (DSTopSPBanChay.Count <= 3)
            {
                DSTopSPBanChay.Add(new TopSP() { TenSanPham = "Trống", TongSoLuongBan = 0 });
            }
            DSTopSPBanChay.Sort();

            ObservableCollection<CT_PBH> DSTop1 = new ObservableCollection<CT_PBH>(DSCT_PBH.Where(x => x.SANPHAM.TenSanPham.Equals(DSTopSPBanChay[0].TenSanPham)));

            ChartTopSP = new SeriesCollection
            {
                new RowSeries
                {
                    Title ="T"+ timeNow.Month.ToString() + "/" + timeNow.Year.ToString() ,
                    Values = new ChartValues<double> {
                        DSCT_PBH.Where(x=>x.SANPHAM.TenSanPham.Equals(DSTopSPBanChay[0].TenSanPham) && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeNow.Month && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeNow.Year).Sum(x=>x.SoLuongBan).Value,
                        DSCT_PBH.Where(x=>x.SANPHAM.TenSanPham.Equals(DSTopSPBanChay[1].TenSanPham) && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeNow.Month && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeNow.Year).Sum(x=>x.SoLuongBan).Value,
                        DSCT_PBH.Where(x=>x.SANPHAM.TenSanPham.Equals(DSTopSPBanChay[2].TenSanPham) && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeNow.Month && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeNow.Year).Sum(x=>x.SoLuongBan).Value
                    }
                }
            };

            //adding series will update and animate the chart automatically
            ChartTopSP.Add(new RowSeries
            {
                Title = "T" + timeBeforeOneMonth.Month.ToString() + "/" + timeBeforeOneMonth.Year.ToString(),
                Values = new ChartValues<double> {
                        DSCT_PBH.Where(x=>x.SANPHAM.TenSanPham.Equals(DSTopSPBanChay[0].TenSanPham) && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeBeforeOneMonth.Month && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeBeforeOneMonth.Year).Sum(x=>x.SoLuongBan).Value,
                        DSCT_PBH.Where(x=>x.SANPHAM.TenSanPham.Equals(DSTopSPBanChay[1].TenSanPham) && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeBeforeOneMonth.Month && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeBeforeOneMonth.Year).Sum(x=>x.SoLuongBan).Value,
                        DSCT_PBH.Where(x=>x.SANPHAM.TenSanPham.Equals(DSTopSPBanChay[2].TenSanPham) && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeBeforeOneMonth.Month && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeBeforeOneMonth.Year).Sum(x=>x.SoLuongBan).Value
                    }
            });

            ChartTopSP.Add(new RowSeries
            {
                Title = "T" + timeBeforeTwoMonth.Month.ToString() + "/" + timeBeforeTwoMonth.Year.ToString(),
                Values = new ChartValues<double> {
                        DSCT_PBH.Where(x=>x.SANPHAM.TenSanPham.Equals(DSTopSPBanChay[0].TenSanPham) && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeBeforeTwoMonth.Month && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeBeforeTwoMonth.Year).Sum(x=>x.SoLuongBan).Value,
                        DSCT_PBH.Where(x=>x.SANPHAM.TenSanPham.Equals(DSTopSPBanChay[1].TenSanPham) && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeBeforeTwoMonth.Month && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeBeforeTwoMonth.Year).Sum(x=>x.SoLuongBan).Value,
                        DSCT_PBH.Where(x=>x.SANPHAM.TenSanPham.Equals(DSTopSPBanChay[2].TenSanPham) && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Month == timeBeforeTwoMonth.Month && x.PHIEUBANHANG.NgayLapPhieuBan.Value.Year == timeBeforeTwoMonth.Year).Sum(x=>x.SoLuongBan).Value
                    }
            });

            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            LabelsTopSP = new[] { DSTopSPBanChay[0].TenSanPham, DSTopSPBanChay[1].TenSanPham, DSTopSPBanChay[2].TenSanPham };
            FormatterTopSP = value => value.ToString("N0");
        }

        public class Consumo
        {
            public string Titile { get; set; }
            public double Percentage { get; set; }
            public Consumo()
            {
            }
        }
        public class TopSP : IComparable<TopSP>
        {
            public string TenSanPham { get; set; }
            public int TongSoLuongBan { get; set; }
            public TopSP()
            {
            }
            public int CompareTo(TopSP other)
            {
                if (other == null) return 1;
                if (this.TongSoLuongBan < other.TongSoLuongBan) return 1;
                if (this.TongSoLuongBan > other.TongSoLuongBan) return -1;
                return 0;
            }
        }
    }
}
