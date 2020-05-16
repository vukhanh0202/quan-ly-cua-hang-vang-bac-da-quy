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
    class ProviderViewModel : BaseViewModel
    {
        #region list binding

        private ObservableCollection<NHACUNGCAP> _DSNhaCungCapDB;
        public ObservableCollection<NHACUNGCAP> DSNhaCungCapDB { get => _DSNhaCungCapDB; set { _DSNhaCungCapDB = value; OnPropertyChanged(); } }

        private IEnumerable<NHACUNGCAP> _DSNhaCungCap;
        public IEnumerable<NHACUNGCAP> DSNhaCungCap { get => _DSNhaCungCap; set { _DSNhaCungCap = value; OnPropertyChanged(); } }

        private ObservableCollection<NHACUNGCAP> _DSDiaChiNhaCungCap;
        public ObservableCollection<NHACUNGCAP> DSDiaChiNhaCungCap { get => _DSDiaChiNhaCungCap; set { _DSDiaChiNhaCungCap = value; OnPropertyChanged(); } }

        private ObservableCollection<NHACUNGCAP> _DSNCCDaChon;
        public ObservableCollection<NHACUNGCAP> DSNCCDaChon { get => _DSNCCDaChon; set { _DSNCCDaChon = value; OnPropertyChanged(); } }

        #endregion

        #region thuộc tính binding

        private int _TongSoLuong;
        public int TongSoLuong { get => _TongSoLuong; set { _TongSoLuong = value; OnPropertyChanged(); } }

        private double _TongMua;
        public double TongMua { get => _TongMua; set { _TongMua = value; OnPropertyChanged(); } }

        private bool _FlagTatCaNCC;
        public bool FlagTatCaNCC { get => _FlagTatCaNCC; set { _FlagTatCaNCC = value; OnPropertyChanged(); } }

        private NHACUNGCAP _NCCTheoDiaChi;
        public NHACUNGCAP NCCTheoDiaChi { get => _NCCTheoDiaChi; set { _NCCTheoDiaChi = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private NHACUNGCAP _NCCDaChon;
        public NHACUNGCAP NCCDaChon { get => _NCCDaChon; set { _NCCDaChon = value; OnPropertyChanged(); } }

        public static int status;

        #endregion

        #region command
        public ICommand TatCaNCCCommand { get; set; }
        public ICommand NhaCungCapHDCommand { get; set; }
        public ICommand NhaCungCaNHDCommand { get; set; }
        public ICommand SelectedAddressCommand { get; set; }
        public ICommand XoaLocCommand { get; set; }
        public ICommand TimKiemCommand { get; set; }
        public ICommand ThemNCCCommand { get; set; }
        public ICommand ChiTietNCCCommand { get; set; }
        public ICommand XoaNCCCommand { get; set; }
        public ICommand NgungVaKhoiPhucCommand { get; set; }
        public ICommand ReloadCommand { get; set; }

        public RelayCommand<IList> SelectionChangedCommand { get; set; }


        #endregion

        public ProviderViewModel()
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
                FlagTatCaNCC = true;
                LayDSTuDatabase();
                LoadDanhSach();
                status = 0;
            });

            // Lấy danh sách được chọn từ dataGrids
            SelectionChangedCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                DSNCCDaChon.Clear();
                foreach (var item in items)
                    DSNCCDaChon.Add((NHACUNGCAP)item);
            });

            // Chọn lọc theo khu vực
            SelectedAddressCommand = new RelayCommand<Object>((p) =>
            {
                if (NCCTheoDiaChi == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    if (NCCTheoDiaChi.DiaChiNCC.ToLower() == "tất cả")
                    {
                        DSNhaCungCap = DSNhaCungCapDB;
                    }
                    else
                    {
                        DSNhaCungCap = DSNhaCungCapDB.Where(x => x.DiaChiNCC.ToLower().Contains(Utils.ConvertUtils.convertString(NCCTheoDiaChi.DiaChiNCC).ToLower()));
                    }
                    TongSoLuong = DSNhaCungCap.Count();
                    TongMua = TinhTongGiaTri(DSNhaCungCap);
                }
                catch (Exception) { }
            });

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) != "" && DSNhaCungCap != null)
                {
                    return true;
                }
                else
                {
                    LoadDanhSach();
                    return false;
                }
            }, (p) =>
            {
                try
                {
                    DSNhaCungCap = DSNhaCungCap.Where(x => (x.MaNhaCungCap.ToString().Contains(TimKiem.ToLower())
                                                        || x.TenNhaCungCap.ToLower().Contains(TimKiem.ToLower())
                                                        || x.SoDienThoaiNCC.ToLower().Contains(TimKiem.ToLower())
                                                        || x.DiaChiNCC.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongTienBanNCC.ToString().ToLower().Contains(TimKiem.ToLower())));
                    TongSoLuong = DSNhaCungCap.Count();
                    TongMua = TinhTongGiaTri(DSNhaCungCap);
                }
                catch (Exception) { }
            });

            // Xóa/ ngưng hoạt động nhà cung cấp
            XoaNCCCommand = new RelayCommand<Object>((p) =>
            {
                if (DSNCCDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn có chắc chắn thực hiện hành động loại bỏ nhà cung cấp", "Xác nhận", MessageBoxButton.OKCancel);
                if (isExit == MessageBoxResult.OK)
                {
                    try
                    {
                        foreach (var NCC in DSNCCDaChon)
                        {
                            bool flag = true;
                            if (NCC.TrangThaiNCC == Constant.ACTIVE_STATUS)
                            {
                                DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap == NCC.MaNhaCungCap).SingleOrDefault().TrangThaiNCC = Constant.INACTIVE_STATUS;
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            else if (NCC.TrangThaiNCC == Constant.INACTIVE_STATUS)
                            {
                                // Kiểm tra phiếu mua hàng
                                var PMH = DataProvider.Ins.DB.PHIEUMUAHANGs.Where(x => x.MaNhaCungCap == NCC.MaNhaCungCap).FirstOrDefault();

                                if (PMH != null)
                                {
                                    MessageBox.Show("Vui lòng xóa những dữ liệu liên quan đến nhà cung cấp này!");
                                    flag = false;
                                }

                                if (flag == true)
                                {
                                    // XÓa khách hàng
                                    DataProvider.Ins.DB.NHACUNGCAPs.Remove(NCC);
                                    DataProvider.Ins.DB.SaveChanges();
                                    MessageBox.Show("Thành công");
                                }
                            }
                        }
                        LayDSTuDatabase();
                        LoadDanhSach();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Hành động thất bại vui lòng thử lại");
                    }
                }
            });

            // Ngưng hoạt động và khôi phục 
            NgungVaKhoiPhucCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    var NCC = DSNhaCungCap.Where(x => x.MaNhaCungCap.ToString().Equals(p.ToString())).SingleOrDefault();

                    if (NCC.TrangThaiNCC == Constant.ACTIVE_STATUS)
                    {
                        DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap == NCC.MaNhaCungCap).SingleOrDefault().TrangThaiNCC = Constant.INACTIVE_STATUS;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else if (NCC.TrangThaiNCC == Constant.INACTIVE_STATUS)
                    {
                        DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MaNhaCungCap == NCC.MaNhaCungCap).SingleOrDefault().TrangThaiNCC = Constant.ACTIVE_STATUS;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    LayDSTuDatabase();
                    LoadDanhSach();
                }
                catch (Exception)
                {
                    MessageBox.Show("Hành động thất bại vui lòng thử lại");
                }
            });

            // Xóa lọc
            XoaLocCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(NCCTheoDiaChi.DiaChiNCC).ToLower() == "tất cả")
                    return false;
                return true;
            }, (p) =>
            {
                try
                {
                    NCCTheoDiaChi = DSDiaChiNhaCungCap.Where(x => x.DiaChiNCC.ToLower() == "tất cả").SingleOrDefault();
                    DSNhaCungCap = DSNhaCungCapDB;
                    TongSoLuong = DSNhaCungCap.Count();
                    TongMua = TinhTongGiaTri(DSNhaCungCap);
                    FlagTatCaNCC = true;
                }
                catch (Exception)
                {
                }
            });


            // Tất cả nhà cung cấp
            TatCaNCCCommand = new RelayCommand<Object>((p) =>
            {
                if (NCCTheoDiaChi != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                try
                {
                    if (NCCTheoDiaChi.DiaChiNCC.ToLower() == "tất cả")
                    {
                        DSNhaCungCap = DSNhaCungCapDB;
                    }
                    else
                    {
                        DSNhaCungCap = DSNhaCungCapDB.Where(x => x.DiaChiNCC.ToLower().Contains(Utils.ConvertUtils.convertString(NCCTheoDiaChi.DiaChiNCC).ToLower()));
                    }
                    TongSoLuong = DSNhaCungCap.Count();
                    TongMua = TinhTongGiaTri(DSNhaCungCap);
                }
                catch (Exception)
                {
                }
            });


            // Nhà cung cấp đang hoạt động
            NhaCungCapHDCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    if (NCCTheoDiaChi.DiaChiNCC.ToLower() == "tất cả")
                    {
                        DSNhaCungCap = DSNhaCungCapDB.Where(x => x.TrangThaiNCC == Constant.ACTIVE_STATUS);
                    }
                    else
                    {
                        DSNhaCungCap = DSNhaCungCapDB.Where(x => x.TrangThaiNCC == Constant.ACTIVE_STATUS && x.DiaChiNCC.ToLower().Contains(Utils.ConvertUtils.convertString(NCCTheoDiaChi.DiaChiNCC).ToLower()));
                    }
                    TongSoLuong = DSNhaCungCap.Count();
                    TongMua = TinhTongGiaTri(DSNhaCungCap);
                }
                catch (Exception)
                {
                }

            });

            // Nhà cung cấp ngừng hoạt động
            NhaCungCaNHDCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    if (NCCTheoDiaChi.DiaChiNCC.ToLower() == "tất cả")
                    {
                        DSNhaCungCap = DSNhaCungCapDB.Where(x => x.TrangThaiNCC == Constant.INACTIVE_STATUS);
                    }
                    else
                    {
                        DSNhaCungCap = DSNhaCungCapDB.Where(x => x.TrangThaiNCC == Constant.INACTIVE_STATUS && x.DiaChiNCC.ToLower().Contains(Utils.ConvertUtils.convertString(NCCTheoDiaChi.DiaChiNCC).ToLower()));
                    }
                    TongSoLuong = DSNhaCungCap.Count();
                    TongMua = TinhTongGiaTri(DSNhaCungCap);
                }
                catch (Exception)
                {
                }

            });

            // Thêm nhà cung cấp
            ThemNCCCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                NewProviderWindow newProvider = new NewProviderWindow();
                p.Hide();
                newProvider.ShowDialog();
                p.Show();
                LayDSTuDatabase();
                FlagTatCaNCC = true;
                LoadDanhSach();
            });

            // Chi tiết nhà cung cấp
            ChiTietNCCCommand = new RelayCommand<object>((p) =>
            {
                if (NCCDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                DetailProviderWindow detailProvider = new DetailProviderWindow();
                (detailProvider.DataContext as DetailProviderViewModel).LoadDuLieuTuNhaCungCap(NCCDaChon);
                detailProvider.ShowDialog();
                NCCDaChon = null;
                LayDSTuDatabase();
                FlagTatCaNCC = true;
                LoadDanhSach();
            });
            #endregion
        }

        public void LayDSTuDatabase()
        {
            DSNhaCungCapDB = new ObservableCollection<NHACUNGCAP>(DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.TenNhaCungCap.ToLower() != "nhà cung cấp lẻ"));
            DSNCCDaChon = new ObservableCollection<NHACUNGCAP>();
        }
        public void LoadDanhSach()
        {
            DSNhaCungCap = DSNhaCungCapDB;
            DSDiaChiNhaCungCap = new ObservableCollection<NHACUNGCAP>();
            foreach (var item in DSNhaCungCap)
            {
                DSDiaChiNhaCungCap.Add(item);
            }
            DSDiaChiNhaCungCap.Add(new NHACUNGCAP() { DiaChiNCC = "Tất cả", MaNhaCungCap = -1 });
            NCCTheoDiaChi = DSDiaChiNhaCungCap.Where(x => x.DiaChiNCC.ToLower() == "tất cả").SingleOrDefault();

            TongSoLuong = DSNhaCungCap.Count();
            TongMua = TinhTongGiaTri(DSNhaCungCap);
        }
        public double TinhTongGiaTri(IEnumerable<NHACUNGCAP> DSNhaCungCap)
        {
            double total = 0;
            foreach (var item in DSNhaCungCap)
            {
                total += (double)item.TongTienBanNCC;
            }
            return total;
        }
    }
}
