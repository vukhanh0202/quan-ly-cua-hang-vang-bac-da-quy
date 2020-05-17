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
    class InventoryReportViewModel : BaseViewModel
    {
        #region Binding
        private int _TongSP;
        public int TongSP { get => _TongSP; set { _TongSP = value; OnPropertyChanged(); } }

        private int _TongSLMuaVao;
        public int TongSLMuaVao { get => _TongSLMuaVao; set { _TongSLMuaVao = value; OnPropertyChanged(); } }

        private int _TongSLBanRa;
        public int TongSLBanRa { get => _TongSLBanRa; set { _TongSLBanRa = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private int _ThangDaChon;
        public int ThangDaChon { get => _ThangDaChon; set { _ThangDaChon = value; OnPropertyChanged(); } }

        private int _NamDaChon;
        public int NamDaChon { get => _NamDaChon; set { _NamDaChon = value; OnPropertyChanged(); } }

        private bool _FlagToanThoiGian;
        public bool FlagToanThoiGian { get => _FlagToanThoiGian; set { _FlagToanThoiGian = value; OnPropertyChanged(); } }


        public static int status;
        #endregion

        #region  List Binding
        private ObservableCollection<BAOCAOTONKHO> _DSBaoCaoTonKhoDB;
        public ObservableCollection<BAOCAOTONKHO> DSBaoCaoTonKhoDB { get => _DSBaoCaoTonKhoDB; set { _DSBaoCaoTonKhoDB = value; OnPropertyChanged(); } }

        private IEnumerable<BAOCAOTONKHO> _DSBaoCaoTonKhoDBIE;
        public IEnumerable<BAOCAOTONKHO> DSBaoCaoTonKhoDBIE { get => _DSBaoCaoTonKhoDBIE; set { _DSBaoCaoTonKhoDBIE = value; OnPropertyChanged(); } }

        private ObservableCollection<BaoCaoTonKhoModel> _DSBaoCaoTonKho;
        public ObservableCollection<BaoCaoTonKhoModel> DSBaoCaoTonKho { get => _DSBaoCaoTonKho; set { _DSBaoCaoTonKho = value; OnPropertyChanged(); } }

        private ObservableCollection<int> _DSThang;
        public ObservableCollection<int> DSThang { get => _DSThang; set { _DSThang = value; OnPropertyChanged(); } }

        private ObservableCollection<int> _DSNam;
        public ObservableCollection<int> DSNam { get => _DSNam; set { _DSNam = value; OnPropertyChanged(); } }
        #endregion

        #region  Command
        public ICommand TimKiemCommand { get; set; }
        public ICommand ToanThoiGianCommand { get; set; }
        public ICommand TuyChinhThoiGianCommand { get; set; }
        public ICommand ChiTietHDCommand { get; set; }
        public ICommand XoaHDCommand { get; set; }
        public ICommand ThemPhieuMHCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        #endregion

        public InventoryReportViewModel()
        {

            #region Command
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
                FlagToanThoiGian = true;
                ThangDaChon = DateTime.Now.Date.AddMonths(-1).Month;
                NamDaChon = DateTime.Now.Date.Year;

                LayDSTuDatabase();
                LoadDS();
                TinhTong();


                DSThang = new ObservableCollection<int>();
                DSNam = new ObservableCollection<int>();

                for (int i = 1; i < 13; i++)
                {
                    DSThang.Add(i);
                }
                foreach (var item in DSBaoCaoTonKhoDB)
                {
                    if (!DSNam.Contains(item.Nam.Value))
                        DSNam.Add(item.Nam.Value);
                }
                status = 0;
            });

            // Tìm kiếm 
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) == "" || DSBaoCaoTonKhoDB == null)
                {
                    LayDSTuDatabase();
                    LoadDS();
                    TinhTong();
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    DSBaoCaoTonKhoDBIE = DSBaoCaoTonKhoDB.Where(x => (x.Thang.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.Nam.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.SLMuaVao.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.SLBanRa.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.TonDau.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.TonCuoi.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.SANPHAM.TenSanPham.ToLower().Contains(TimKiem.ToLower())
                                                        || x.DVT.TenDVT.ToLower().Contains(TimKiem.ToLower())));
                    LoadDS();
                    TinhTong();
                }
                catch (Exception) { }
            });

            // Toàn thời gian
            ToanThoiGianCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    LayDSTuDatabase();
                    LoadDS();
                    TinhTong();
                }
                catch (Exception)
                {
                }

            });


            // Tùy chỉnh thời gian nhập kho
            TuyChinhThoiGianCommand = new RelayCommand<Object>((p) =>
            {
                if (ThangDaChon.ToString() != null && NamDaChon.ToString() !=null && FlagToanThoiGian == false)
                {
                    if (ThangDaChon >= DateTime.Now.Month && NamDaChon >= DateTime.Now.Year)
                    {
                        MessageBox.Show("Chưa có báo cáo phù hợp!");
                        ThangDaChon = DateTime.Now.Date.AddMonths(-1).Month;
                        NamDaChon = DateTime.Now.Date.Year;
                    }
                    return true;
                }
                return false;
            }, (p) =>
            {
                try
                {
                    DSBaoCaoTonKhoDBIE = DSBaoCaoTonKhoDB.Where(x => x.Thang == ThangDaChon && x.Nam == NamDaChon);
                    LoadDS();
                    TinhTong();
                }
                catch (Exception)
                {
                }

            });

            #endregion
        }

        public void LayDSTuDatabase()
        {
            DSBaoCaoTonKhoDB = new ObservableCollection<BAOCAOTONKHO>(DataProvider.Ins.DB.BAOCAOTONKHOes.Where(x=>x.Thang < DateTime.Now.Month && x.Nam < DateTime.Now.Year));
            DSBaoCaoTonKhoDBIE = DSBaoCaoTonKhoDB;
        }
        public void LoadDS()
        {
            DSBaoCaoTonKho = new ObservableCollection<BaoCaoTonKhoModel>();

            foreach (var item in DSBaoCaoTonKhoDBIE)
            {
                DSBaoCaoTonKho.Add(new BaoCaoTonKhoModel()
                {
                    MaBaoCaoTonKho = item.MaBaoCaoTonKho,
                    MaSanPham = item.MaSanPham.Value,
                    SANPHAM = item.SANPHAM,
                    SLBanRa = item.SLBanRa.Value,
                    SLMuaVao = item.SLMuaVao.Value,
                    TenDVT = item.DVT.TenDVT,
                    TonCuoi = item.TonCuoi.Value,
                    TonDau = item.TonDau.Value,
                    ThoiGianTonKho = new DateTime(item.Nam.Value, item.Thang.Value, 1)
                });
            }
        }

        public void TinhTong()
        {
            TongSP = DSBaoCaoTonKho.Count();
            TongSLMuaVao = DSBaoCaoTonKho.Sum(x => x.SLMuaVao);
            TongSLBanRa = DSBaoCaoTonKho.Sum(x => x.SLBanRa);
        }
    }
}
