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
    class NewTypeProductViewModel : BaseViewModel
    {
        #region Thuộc tính binding

        //private string _name;
        //public string name { get => _name; set { _name = value; OnPropertyChanged(); } }

        //private string _description;
        //public string description { get => _description; set { _description = value; OnPropertyChanged(); } }

        //private string _unit;
        //public string unit { get => _unit; set { _unit = value; OnPropertyChanged(); } }

        //private double _profitPercent;
        //public double profitPercent { get => _profitPercent; set { _profitPercent = value; OnPropertyChanged(); } }
        private LOAISANPHAM _LoaiSanPhamMoi;
        public LOAISANPHAM LoaiSanPhamMoi { get => _LoaiSanPhamMoi; set { _LoaiSanPhamMoi = value; OnPropertyChanged(); } }

        private string _TimKiemLSPNgungHD;
        public string TimKiemLSPNgungHD { get => _TimKiemLSPNgungHD; set { _TimKiemLSPNgungHD = value; OnPropertyChanged(); } }

        private string _TimKiemLSPHD;
        public string TimKiemLSPHD { get => _TimKiemLSPHD; set { _TimKiemLSPHD = value; OnPropertyChanged(); } }

        private string _SLLSPHoatDong;
        public string SLLSPHoatDong { get => _SLLSPHoatDong; set { _SLLSPHoatDong = value; OnPropertyChanged(); } }

        private string _SLLSPNgungHD;
        public string SLLSPNgungHD { get => _SLLSPNgungHD; set { _SLLSPNgungHD = value; OnPropertyChanged(); } }

        private DVT _DVTDaChon;
        public DVT DVTDaChon { get => _DVTDaChon; set { _DVTDaChon = value; OnPropertyChanged(); } }

        private LOAISANPHAM _LSPDaChon;
        public LOAISANPHAM LSPDaChon { get => _LSPDaChon; set { _LSPDaChon = value; OnPropertyChanged(); } }

        public bool isChanged = false;
        #endregion
        #region Danh sách
        private IEnumerable<LOAISANPHAM> _DSLoaiSanPhamDB;
        public IEnumerable<LOAISANPHAM> DSLoaiSanPhamDB { get => _DSLoaiSanPhamDB; set { _DSLoaiSanPhamDB = value; OnPropertyChanged(); } }

        private ObservableCollection<LOAISANPHAM> _DSLoaiSanPhamHD;
        public ObservableCollection<LOAISANPHAM> DSLoaiSanPhamHD { get => _DSLoaiSanPhamHD; set { _DSLoaiSanPhamHD = value; OnPropertyChanged(); } }

        private IEnumerable<LOAISANPHAM> _DSLoaiSanPhamNgungHD;
        public IEnumerable<LOAISANPHAM> DSLoaiSanPhamNgungHD { get => _DSLoaiSanPhamNgungHD; set { _DSLoaiSanPhamNgungHD = value; OnPropertyChanged(); } }

        private ObservableCollection<LOAISANPHAM> _DSSanPhamHDDaChon;
        public ObservableCollection<LOAISANPHAM> DSSanPhamHDDaChon { get => _DSSanPhamHDDaChon; set { _DSSanPhamHDDaChon = value; OnPropertyChanged(); } }

        private ObservableCollection<LOAISANPHAM> _DSSanPhamNgungHDDaChon;
        public ObservableCollection<LOAISANPHAM> DSSanPhamNgungHDDaChon { get => _DSSanPhamNgungHDDaChon; set { _DSSanPhamNgungHDDaChon = value; OnPropertyChanged(); } }

        private ObservableCollection<DVT> _DSDVT;
        public ObservableCollection<DVT> DSDVT { get => _DSDVT; set { _DSDVT = value; OnPropertyChanged(); } }

        #endregion

        #region Command

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand XoaLSPCommand { get; set; }
        public ICommand NgungHoatDongCommand { get; set; }
        public ICommand TimKiemLSPHDCommand { get; set; }
        public ICommand TimKiemLSPNgungHDCommand { get; set; }
        public ICommand KhoiPhucCommand { get; set; }
        public ICommand ThemDVTCommand { get; set; }
        public ICommand SuaLSPCommand { get; set; }
        public ICommand XacNhanSuaLSPCommand { get; set; }
        public ICommand CapNhatCommand { get; set; }
        public RelayCommand<IList> SelectionChangedActiveCommand { get; set; }
        public RelayCommand<IList> SelectionChangedInActiveCommand { get; set; }

        #endregion

        public NewTypeProductViewModel()
        {
            #region Command
            // Command khi load vào window
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
             {

                 DSSanPhamHDDaChon = new ObservableCollection<LOAISANPHAM>();
                 DSSanPhamNgungHDDaChon = new ObservableCollection<LOAISANPHAM>();
                 DVTDaChon = null;
                 LayDSTuDatabase();
                 LoadDSHoatDong();
                 LoadDSNgungHoatDong();
                 LoaiSanPhamMoi = new LOAISANPHAM();
             });

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertString.convertString(LoaiSanPhamMoi.TenLoaiSanPham) != ""
                    && Utils.ConvertString.convertString(DVTDaChon.MaDVT.ToString()) != ""
                    && Utils.ConvertString.convertString(LoaiSanPhamMoi.PhanTramLoiNhuan.ToString()) != "")
                {
                    if (LoaiSanPhamMoi.PhanTramLoiNhuan < 0)
                    {
                        MessageBox.Show("Vui lòng nhập phần trăm lợi nhuận lớn hơn 0");
                        return false;
                    }
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
                    //ProductType productType = new ProductType()
                    //{
                    //    name = name,
                    //    descriptions = Utils.ConvertString.convertString(description),
                    //    unit = Utils.ConvertString.convertString(unit),
                    //    status = 1,
                    //    profit_percent = (double)profitPercent,
                    //    create_date = DateTime.Now.Date
                    //};
                    LoaiSanPhamMoi.TrangThaiLoaiSanPham = Constant.ACTIVE_STATUS;
                    LoaiSanPhamMoi.MaDVT = DVTDaChon.MaDVT;
                    LoaiSanPhamMoi.DVT = DVTDaChon;
                    DataProvider.Ins.DB.LOAISANPHAMs.Add(LoaiSanPhamMoi);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Thêm thành công");
                    p.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Thêm thất bại, vui lòng kiểm tra lại");
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

            // Ngưng hoạt động
            NgungHoatDongCommand = new RelayCommand<Window>((p) =>
            {
                if (DSSanPhamHDDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn chắc chắn muốn ngưng hoạt động?", "Xác nhận", MessageBoxButton.OKCancel);
                if (isExit == MessageBoxResult.OK)
                {
                    try
                    {
                        foreach (var item in DSSanPhamHDDaChon)
                        {
                            DataProvider.Ins.DB.LOAISANPHAMs.Where(x => x.MaLoaiSanPham == item.MaLoaiSanPham).SingleOrDefault().TrangThaiLoaiSanPham = Constant.INACTIVE_STATUS;
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        MessageBox.Show("Ngưng hoạt động thành công");
                        LayDSTuDatabase();
                        LoadDSHoatDong();
                        LoadDSNgungHoatDong();
                    }
                    catch (Exception)
                    {
                    }
                }
            });

            // Cập nhật
            CapNhatCommand = new RelayCommand<Window>((p) =>
            {
                return false;
            }, (p) =>
            {
                try
                {
                    foreach (var item in DSLoaiSanPhamHD)
                    {
                        var temp = DataProvider.Ins.DB.LOAISANPHAMs.Where(x => x.MaLoaiSanPham == item.MaLoaiSanPham).SingleOrDefault();
                        temp = item;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    MessageBox.Show("Thành Công");
                    LayDSTuDatabase();
                    LoadDSHoatDong();
                    LoadDSNgungHoatDong();
                }
                catch (Exception)
                {
                }
            });

            // Lấy danh sách được chọn từ dataGrid
            SelectionChangedActiveCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                try
                {
                    DSSanPhamHDDaChon.Clear();
                    foreach (var item in items)
                        DSSanPhamHDDaChon.Add((LOAISANPHAM)item);
                }
                catch (Exception)
                {
                }
            });

            SelectionChangedInActiveCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                try
                {
                    DSSanPhamNgungHDDaChon.Clear();
                    foreach (var item in items)
                        DSSanPhamNgungHDDaChon.Add((LOAISANPHAM)item);
                }
                catch (Exception)
                {
                }
            });

            // Xóa product
            XoaLSPCommand = new RelayCommand<Object>((p) =>
            {
                if (DSSanPhamNgungHDDaChon.Count > 0)
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
                        foreach (var SPDaChon in DSSanPhamNgungHDDaChon)
                        {

                            DataProvider.Ins.DB.LOAISANPHAMs.Remove(DSLoaiSanPhamDB.Where(x => x.MaLoaiSanPham == SPDaChon.MaLoaiSanPham).SingleOrDefault());
                            // Xóa product liên quan
                            var DSSanPham = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaLoaiSanPham == SPDaChon.MaLoaiSanPham);
                            foreach (var i in DSSanPham)
                            {
                                // Xóa các hóa đơn liên quan
                                var DSHDPhieuMuaHang = DataProvider.Ins.DB.CT_PMH.Where(x => x.MaSanPham == i.MaSanPham);
                                foreach (var j in DSHDPhieuMuaHang)
                                {
                                    DataProvider.Ins.DB.CT_PMH.Remove(j);
                                }
                                var DSHoaDonPhieuBanHang = DataProvider.Ins.DB.CT_PBH.Where(x => x.MaSanPham == i.MaSanPham);
                                foreach (var j in DSHoaDonPhieuBanHang)
                                {
                                    DataProvider.Ins.DB.CT_PBH.Remove(j);
                                }
                                DataProvider.Ins.DB.SANPHAMs.Remove(i);
                            }
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        MessageBox.Show("Xóa thành công");
                        LayDSTuDatabase();
                        LoadDSNgungHoatDong();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Xóa thất bại vui lòng thử lại");
                    }
                }
            });

            // Khôi phục
            KhoiPhucCommand = new RelayCommand<Object>((p) =>
            {
                if (DSSanPhamNgungHDDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn muốn khôi dụng dữ liệu?", "Xác nhận", MessageBoxButton.OKCancel);
                if (isExit == MessageBoxResult.OK)
                {
                    try
                    {
                        foreach (var item in DSSanPhamNgungHDDaChon)
                        {
                            DataProvider.Ins.DB.LOAISANPHAMs.Where(x => x.MaLoaiSanPham == item.MaLoaiSanPham).SingleOrDefault().TrangThaiLoaiSanPham = Constant.ACTIVE_STATUS;
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        MessageBox.Show("Khôi phục thành công");
                        LayDSTuDatabase();
                        LoadDSHoatDong();
                        LoadDSNgungHoatDong();
                    }
                    catch (Exception)
                    {
                    }
                }
            });

            //Lọc dữ liệu
            TimKiemLSPHDCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertString.convertString(TimKiemLSPHD) != "" && DSLoaiSanPhamHD != null)
                {
                    return true;
                }
                else
                {
                    LoadDSHoatDong();
                    return false;
                }

            }, (p) =>

            {
                try
                {
                    DSLoaiSanPhamHD = new ObservableCollection<LOAISANPHAM>( DSLoaiSanPhamHD.Where(x => (x.MaLoaiSanPham.ToString().Contains(TimKiemLSPHD.ToLower())
                                                        || x.TenLoaiSanPham.ToLower().Contains(TimKiemLSPHD.ToLower())
                                                        || x.DVT.TenDVT.ToLower().Contains(TimKiemLSPHD.ToLower())
                                                        || x.PhanTramLoiNhuan.ToString().ToLower().Contains(TimKiemLSPHD.ToLower()))));
                    SLLSPHoatDong = DSLoaiSanPhamHD.Count().ToString();
                }
                catch (Exception) { }
            });

            TimKiemLSPNgungHDCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertString.convertString(TimKiemLSPNgungHD) != "" && DSLoaiSanPhamNgungHD != null)
                {
                    return true;
                }
                else
                {
                    LoadDSNgungHoatDong();
                    return false;
                }

            }, (p) =>

            {
                try
                {
                    DSLoaiSanPhamNgungHD = DSLoaiSanPhamNgungHD.Where(x => (x.MaLoaiSanPham.ToString().Contains(TimKiemLSPHD.ToLower())
                                                        || x.TenLoaiSanPham.ToLower().Contains(TimKiemLSPHD.ToLower())
                                                        || x.DVT.TenDVT.ToLower().Contains(TimKiemLSPHD.ToLower())
                                                        || x.PhanTramLoiNhuan.ToString().ToLower().Contains(TimKiemLSPHD.ToLower())));

                    SLLSPNgungHD = DSLoaiSanPhamNgungHD.Count().ToString();
                }
                catch (Exception e) { }
            });

            // Thêm Đơn vị tính
            ThemDVTCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                UnitWindow unitWindow = new UnitWindow();
                unitWindow.ShowDialog();
                LayDSTuDatabase();
            });


            // Chỉnh sửa loại sản phẩm
            SuaLSPCommand = new RelayCommand<object>((p) =>
            {
                if (LSPDaChon != null)
                {
                    return true; ;
                }
                return false;
            }, (p) =>
            {
                isChanged = true;
            });

            // thực hiện điều chỉnh sau khi chỉnh sửa
            XacNhanSuaLSPCommand = new RelayCommand<object>((p) =>
            {
                if (isChanged == true)
                {
                    return true; ;
                }
                return false;
            }, (p) =>
            {
                try
                {
                    var LSPHD = DSLoaiSanPhamHD.Where(x => x.MaLoaiSanPham == LSPDaChon.MaLoaiSanPham).SingleOrDefault();
                    if (LSPHD != null)
                    {
                        LSPHD = LSPDaChon;
                    }

                    isChanged = false;
                }
                catch (Exception)
                {
                }
                

            });
            #endregion
        }
        //public void resetData()
        //{
        //    name = "";
        //    description = "";
        //    unit = "";
        //    profitPercent = 0;
        //}
        public void LayDSTuDatabase()
        {
            DSLoaiSanPhamDB = new ObservableCollection<LOAISANPHAM>(DataProvider.Ins.DB.LOAISANPHAMs);
            DSDVT = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
        }

        public void LoadDSHoatDong()
        {
            DSLoaiSanPhamHD = new ObservableCollection<LOAISANPHAM>(DSLoaiSanPhamDB.Where(x => x.TrangThaiLoaiSanPham == Constant.ACTIVE_STATUS));
            SLLSPHoatDong = DSLoaiSanPhamHD.Count().ToString();
        }

        public void LoadDSNgungHoatDong()
        {
            DSLoaiSanPhamNgungHD = DSLoaiSanPhamDB.Where(x => x.TrangThaiLoaiSanPham == Constant.INACTIVE_STATUS);
            SLLSPNgungHD = DSLoaiSanPhamNgungHD.Count().ToString();

        }
    }
}
