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
    class SalaryViewModel : BaseViewModel
    {
        #region command
        public ICommand Filter { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }

        #endregion

        #region list binding
        private ObservableCollection<string> _DSThang;
        public ObservableCollection<string> DSThang { get => _DSThang; set { _DSThang = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _DSNam;
        public ObservableCollection<string> DSNam { get => _DSNam; set { _DSNam = value; OnPropertyChanged(); } }

        private IEnumerable<LuongNhanVienModel> _DSLuongNV;
        public IEnumerable<LuongNhanVienModel> DSLuongNV { get => _DSLuongNV; set { _DSLuongNV = value; OnPropertyChanged(); } }

        private ObservableCollection<NHANVIEN> _DSNhanVien;
        public ObservableCollection<NHANVIEN> DSNhanVien { get => _DSNhanVien; set { _DSNhanVien = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUBANHANG> _DSPhieuBanHang;
        public IEnumerable<PHIEUBANHANG> DSPhieuBanHang { get => _DSPhieuBanHang; set { _DSPhieuBanHang = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUBANHANG> _DSPhieuBanHangDB;
        public IEnumerable<PHIEUBANHANG> DSPhieuBanHangDB { get => _DSPhieuBanHangDB; set { _DSPhieuBanHangDB = value; OnPropertyChanged(); } }

        #endregion

        private string _ThangDaChon;
        public string ThangDaChon { get => _ThangDaChon; set { _ThangDaChon = value; OnPropertyChanged(); } }

        private string _NamDaChon;
        public string NamDaChon { get => _NamDaChon; set { _NamDaChon = value; OnPropertyChanged(); } }


        public SalaryViewModel()
        {
            // Command khi load vào window
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                getListFromDB();
                DSLuongNV = null;
                DSNam = new ObservableCollection<string>();
                DSThang = new ObservableCollection<string>();

                foreach (var item in DSPhieuBanHangDB)
                {
                    if (!DSNam.Contains(item.NgayLapPhieuBan.Value.Year.ToString().Trim()))
                        DSNam.Add(item.NgayLapPhieuBan.Value.Year.ToString().Trim());
                    if (!DSThang.Contains(item.NgayLapPhieuBan.Value.Month.ToString().Trim()))
                        DSThang.Add(item.NgayLapPhieuBan.Value.Month.ToString().Trim());
                }
            });

            // lọc
            Filter = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(ThangDaChon) == "" || Utils.ConvertUtils.convertString(NamDaChon) == "")
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                DSPhieuBanHang = DSPhieuBanHangDB.Where(x => x.NgayLapPhieuBan.Value.Month.ToString().Equals(ThangDaChon) && x.NgayLapPhieuBan.Value.Year.ToString().Equals(NamDaChon));
                IEnumerable<IDSTAFF> listID = (from NV in DSNhanVien
                                               select new IDSTAFF()
                                               {
                                                   MaNhanVien = NV.MaNhanVien,
                                                   TongBanCuaNV = 0,
                                                   TongSLBan = 0
                                               });
                foreach (var item in listID)
                {
                    item.TongBanCuaNV = (double)DSPhieuBanHang.Where(x => x.MaNhanVien == item.MaNhanVien).Sum(x=>x.TongThanhTien);
                    item.TongSLBan = (int)DSPhieuBanHang.Where(x => x.MaNhanVien == item.MaNhanVien).Sum(x => x.TongSoLuongBan);
                }
                try
                {
                    DSLuongNV = (from NV in DSNhanVien
                                       join temp in listID on NV.MaNhanVien equals temp.MaNhanVien
                                       select new LuongNhanVienModel()
                                       {
                                           MaNhanVien = NV.MaNhanVien,
                                           TenNhanVien = Utils.ConvertUtils.convertString(NV.TenNhanVien),
                                           LuongCoBan = (double)NV.LuongCoBan,
                                           TongSLBan = temp.TongSLBan,
                                           PhanTramHoaHong = Math.Round((double)(temp.TongBanCuaNV * NV.PhanTramHoaHong / 100)),
                                           TongLuong = Math.Round((double)(NV.LuongCoBan + (double)(temp.TongBanCuaNV * NV.PhanTramHoaHong / 100)))
                                       });
                }
                catch (Exception)
                {

                }


            });
            HuyBoCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

        }
        public void getListFromDB()
        {
            //var RoleAdmin = DataProvider.Ins.DB.AccountRoles.Where(x => x.code == Constant.codeRoleADMIN).SingleOrDefault();
            //var AccountADMIN = DataProvider.Ins.DB.Accounts.Where(x => x.account_roles_id == RoleAdmin.id).SingleOrDefault();
            //staffs = new ObservableCollection<Staff>(DataProvider.Ins.DB.Staffs.Where(x => x.account_id != AccountADMIN.id));
            //list = new ObservableCollection<BillSellProduct>(DataProvider.Ins.DB.BillSellProducts);
            DSNhanVien = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            DSPhieuBanHangDB = new ObservableCollection<PHIEUBANHANG>(DataProvider.Ins.DB.PHIEUBANHANGs);
        }

    }
    class IDSTAFF : BaseViewModel
    {
        private int _MaNhanVien;
        public int MaNhanVien { get => _MaNhanVien; set { _MaNhanVien = value; OnPropertyChanged(); } }

        private double _TongBanCuaNV;
        public double TongBanCuaNV { get => _TongBanCuaNV; set { _TongBanCuaNV = value; OnPropertyChanged(); } }

        private int _TongSLBan;
        public int TongSLBan { get => _TongSLBan; set { _TongSLBan = value; OnPropertyChanged(); } }
    }
}
