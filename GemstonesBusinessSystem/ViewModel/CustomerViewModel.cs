using GemstonesBusinessSystem.Model;
using GemstonesBusinessSystem.Utils;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class CustomerViewModel : BaseViewModel
    {
        #region list binding

        private ObservableCollection<KHACHHANG> _DSKhachHangDB;
        public ObservableCollection<KHACHHANG> DSKhachHangDB { get => _DSKhachHangDB; set { _DSKhachHangDB = value; OnPropertyChanged(); } }

        private IEnumerable<KHACHHANG> _DSKhachHang;
        public IEnumerable<KHACHHANG> DSKhachHang { get => _DSKhachHang; set { _DSKhachHang = value; OnPropertyChanged(); } }

        private ObservableCollection<KHACHHANG> _DSDiaChiKhachHang;
        public ObservableCollection<KHACHHANG> DSDiaChiKhachHang { get => _DSDiaChiKhachHang; set { _DSDiaChiKhachHang = value; OnPropertyChanged(); } }

        private ObservableCollection<KHACHHANG> _DSKHDaChon;
        public ObservableCollection<KHACHHANG> DSKHDaChon { get => _DSKHDaChon; set { _DSKHDaChon = value; OnPropertyChanged(); } }

        #endregion

        #region thuộc tính binding

        private int _TongSoLuong;
        public int TongSoLuong { get => _TongSoLuong; set { _TongSoLuong = value; OnPropertyChanged(); } }

        private double _TongBan;
        public double TongBan { get => _TongBan; set { _TongBan = value; OnPropertyChanged(); } }

        private bool _FlagTatCaKH;
        public bool FlagTatCaKH { get => _FlagTatCaKH; set { _FlagTatCaKH = value; OnPropertyChanged(); } }

        private KHACHHANG _KHTheoDiaChi;
        public KHACHHANG KHTheoDiaChi { get => _KHTheoDiaChi; set { _KHTheoDiaChi = value; OnPropertyChanged(); } }

        //private KHACHHANG _defaultCustomer; // Khách lẻ
        //public KHACHHANG defaultCustomer { get => _defaultCustomer; set { _defaultCustomer = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private KHACHHANG _KHDaChon;
        public KHACHHANG KHDaChon { get => _KHDaChon; set { _KHDaChon = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        #region command
        public ICommand TatCaKHCommand { get; set; }
        public ICommand KhachHangHDCommand { get; set; }
        public ICommand KhachHangNHDCommand { get; set; }
        public ICommand SelectedAddressCommand { get; set; }
        public ICommand XoaLocCommand { get; set; }
        public ICommand TimKiemCommand { get; set; }
        public ICommand ThemKHCommand { get; set; }
        public ICommand ChiTietKHCommand { get; set; }
        public ICommand XoaKHCommand { get; set; }
        public ICommand NgungVaKhoiPhucCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand XuatExcelCommand { get; set; }

        public RelayCommand<IList> SelectionChangedCommand { get; set; }


        #endregion
        public CustomerViewModel()
        {


            #region command
            //Xuất Excel
            XuatExcelCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel | *.xlsx | Excel 2019 | *.xls";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialog.ShowDialog() == true)
                {
                    filePath = dialog.FileName;
                }

                // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                    return;
                }

                try
                {
                    using (ExcelPackage a = new ExcelPackage())
                    {
                        // đặt tiêu đề cho file
                        a.Workbook.Properties.Title = "Danh sách khách hàng";

                        //Tạo một sheet để làm việc trên đó
                        a.Workbook.Worksheets.Add("Danh sách");

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = a.Workbook.Worksheets[1];

                        // đặt tên cho sheet
                        ws.Name = "Danh sách";
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 11;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                                "Mã khách hàng",
                                                "Tên khách hàng",
                                                "Số điện thoại",
                                                "Địa chỉ",
                                                "Tổng tiền mua",
                                                "Trạng thái",
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = "Thống kê danh sách khách hàng";
                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        // in đậm
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                        // căn giữa
                        ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        int colIndex = 1;
                        int rowIndex = 2;

                        //tạo các header từ column header đã tạo từ bên trên
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];

                            //set màu thành gray
                            //var fill = cell.Style.Fill;
                            //fill.PatternType = ExcelFillStyle.Solid;
                            //fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                            //căn chỉnh các border
                            //var border = cell.Style.Border;
                            //border.Bottom.Style =
                            //    border.Top.Style =
                            //    border.Left.Style =
                            //    border.Right.Style = ExcelBorderStyle.Thin;

                            //gán giá trị
                            cell.Value = item;

                            colIndex++;
                        }

                        // lấy ra danh sách UserInfo từ ItemSource của DataGrid
                        List<KHACHHANG> userList = DSKhachHang.ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.MaKhachHang.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TenKhachHang.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.SoDienThoaiKH.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.DiaChiKH.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TongTienMuaKH.ToString();

                            if (item.TrangThaiKH == 1)
                                ws.Cells[rowIndex, colIndex++].Value = "Đang hoạt động";
                            else
                                ws.Cells[rowIndex, colIndex++].Value = "Ngưng hoạt động";


                        }

                        //Lưu file lại
                        Byte[] bin = a.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    MessageBox.Show("Xuất excel thành công!");
                }
                catch (Exception EE)
                {
                    MessageBox.Show("Có lỗi khi lưu file!");
                }
            });
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
                FlagTatCaKH = true;
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
                DSKHDaChon.Clear();
                foreach (var item in items)
                    DSKHDaChon.Add((KHACHHANG)item);
            });

            // Chọn lọc theo khu vực
            SelectedAddressCommand = new RelayCommand<Object>((p) =>
            {
                if (KHTheoDiaChi == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    if (KHTheoDiaChi.DiaChiKH.ToLower() == "tất cả")
                    {
                        DSKhachHang = DSKhachHangDB;
                    }
                    else
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.DiaChiKH.ToLower().Contains(Utils.ConvertUtils.convertString(KHTheoDiaChi.DiaChiKH).ToLower()));

                    }
                    TongSoLuong = DSKhachHang.Count();
                    TongBan = TinhTongThanhTien(DSKhachHang);
                }
                catch (Exception) { }
            });

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) != "" && DSKhachHang != null)
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
                    DSKhachHang = DSKhachHang.Where(x => (x.MaKhachHang.ToString().Contains(TimKiem.ToLower())
                                                        || x.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.SoDienThoaiKH.ToLower().Contains(TimKiem.ToLower())
                                                        || x.DiaChiKH.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongTienMuaKH.ToString().ToLower().Contains(TimKiem.ToLower())));
                    TongSoLuong = DSKhachHang.Count();
                    TongBan = TinhTongThanhTien(DSKhachHang);
                }
                catch (Exception) { }
            });

            // Xóa/ ngưng hoạt động khách hàng
            XoaKHCommand = new RelayCommand<Object>((p) =>
            {
                if (DSKHDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn có chắc chắn thực hiện hành động loại bỏ khách hàng", "Xác nhận", MessageBoxButton.OKCancel);
                if (isExit == MessageBoxResult.OK)
                {
                    try
                    {
                        foreach (var KH in DSKHDaChon)
                        {
                            bool flag = true;
                            if (KH.TrangThaiKH == Constant.ACTIVE_STATUS)
                            {
                                DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == KH.MaKhachHang).SingleOrDefault().TrangThaiKH = Constant.INACTIVE_STATUS;
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            else if (KH.TrangThaiKH == Constant.INACTIVE_STATUS)
                            {
                                // Kiểm tra phiếu bán hàng
                                var PBH = DataProvider.Ins.DB.PHIEUBANHANGs.Where(x => x.MaKhachHang == KH.MaKhachHang).FirstOrDefault();
                                var PDV = DataProvider.Ins.DB.PHIEUDICHVUs.Where(x => x.MaKhachHang == KH.MaKhachHang).FirstOrDefault();

                                DataProvider.Ins.DB.SaveChanges();

                                if (PBH != null || PDV !=null)
                                {
                                    MessageBox.Show("Vui lòng xóa những dữ liệu liên quan đến khách hàng này!");
                                    flag = false;
                                }
                            }
                            if (flag == true)
                            {
                                // XÓa khách hàng
                                DataProvider.Ins.DB.KHACHHANGs.Remove(KH);
                                DataProvider.Ins.DB.SaveChanges();
                                MessageBox.Show("Thành công");
                            }
                        }
                        LayDSTuDatabase();
                        LoadDanhSach();
                        DSKHDaChon.Clear();
                    }
                    catch (Exception e)
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
                    var KH = DSKhachHang.Where(x => x.MaKhachHang.ToString().Equals(p.ToString())).SingleOrDefault();
                    if (KH.TrangThaiKH == Constant.ACTIVE_STATUS)
                    {
                        DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == KH.MaKhachHang).SingleOrDefault().TrangThaiKH = Constant.INACTIVE_STATUS;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else if(KH.TrangThaiKH == Constant.INACTIVE_STATUS)
                    {
                        DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MaKhachHang == KH.MaKhachHang).SingleOrDefault().TrangThaiKH = Constant.ACTIVE_STATUS;
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
                if (Utils.ConvertUtils.convertString(KHTheoDiaChi.DiaChiKH).ToLower() == "tất cả")
                    return false;
                return true;
            }, (p) =>
            {
                KHTheoDiaChi = DSDiaChiKhachHang.Where(x => x.DiaChiKH.ToLower() == "tất cả").SingleOrDefault();
                DSKhachHang = DSKhachHangDB;
                TongSoLuong = DSKhachHang.Count();
                TongBan = TinhTongThanhTien(DSKhachHang);
                FlagTatCaKH = true;
            });


            // Tất cả khách hàng
            TatCaKHCommand = new RelayCommand<Object>((p) =>
            {
                if (KHTheoDiaChi != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                try
                {
                    if (KHTheoDiaChi.DiaChiKH.ToLower() == "tất cả")
                    {
                        DSKhachHang = DSKhachHangDB;
                    }
                    else
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.DiaChiKH.ToLower().Contains(Utils.ConvertUtils.convertString(KHTheoDiaChi.DiaChiKH).ToLower()));
                    }
                    TongSoLuong = DSKhachHang.Count();
                    TongBan = TinhTongThanhTien(DSKhachHang);
                }
                catch (Exception)
                {
                }

            });


            // Khách hàng đang hoạt động
            KhachHangHDCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    if (KHTheoDiaChi.DiaChiKH.ToLower() == "tất cả")
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.TrangThaiKH == Constant.ACTIVE_STATUS);
                    }
                    else
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.TrangThaiKH == Constant.ACTIVE_STATUS && x.DiaChiKH.ToLower().Contains(Utils.ConvertUtils.convertString(KHTheoDiaChi.DiaChiKH).ToLower()));
                    }
                    TongSoLuong = DSKhachHang.Count();
                    TongBan = TinhTongThanhTien(DSKhachHang);
                }
                catch (Exception)
                {
                }

            });

            // Khách hàng ngừng hoạt động
            KhachHangNHDCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    if (KHTheoDiaChi.DiaChiKH.ToLower() == "tất cả")
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.TrangThaiKH == Constant.INACTIVE_STATUS);
                    }
                    else
                    {
                        DSKhachHang = DSKhachHangDB.Where(x => x.TrangThaiKH == Constant.INACTIVE_STATUS && x.DiaChiKH.ToLower().Contains(Utils.ConvertUtils.convertString(KHTheoDiaChi.DiaChiKH).ToLower()));
                    }
                    TongSoLuong = DSKhachHang.Count();
                    TongBan = TinhTongThanhTien(DSKhachHang);
                }
                catch (Exception)
                {
                }

            });

            // Thêm khách hàng
            ThemKHCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                NewCustomerWindow newCustomer = new NewCustomerWindow();
                p.Hide();
                newCustomer.ShowDialog();
                p.Show();
                LayDSTuDatabase();
                FlagTatCaKH = true;
                LoadDanhSach();
            });

            // Chi tiết khách hàng
            ChiTietKHCommand = new RelayCommand<object>((p) =>
            {
                if (KHDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                DetailCustomerWindow detailCustomer = new DetailCustomerWindow();
                (detailCustomer.DataContext as DetailCustomerViewModel).LoadDuLieuTuKhachHang(KHDaChon);
                detailCustomer.ShowDialog();
                KHDaChon = null;
                LayDSTuDatabase();
                FlagTatCaKH = true;
                LoadDanhSach();
            });
            #endregion
        }
        public void LayDSTuDatabase()
        {
            DSKhachHangDB = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.TenKhachHang.ToLower() != "khách lẻ"));
            //defaultCustomer = DSKhachHangDB[0];
            //KHTheoDiaChi = defaultCustomer;
            //DSKhachHangDB.RemoveAt(0);
            DSKHDaChon = new ObservableCollection<KHACHHANG>();
        }
        public void LoadDanhSach()
        {
            DSKhachHang = DSKhachHangDB;
            DSDiaChiKhachHang = new ObservableCollection<KHACHHANG>();
            foreach (var item in DSKhachHang)
            {
                DSDiaChiKhachHang.Add(item);
            }
            DSDiaChiKhachHang.Add(new KHACHHANG() { DiaChiKH = "Tất cả", MaKhachHang = -1 });
            KHTheoDiaChi = DSDiaChiKhachHang.Where(x => x.DiaChiKH.ToLower() == "tất cả").SingleOrDefault();

            TongSoLuong = DSKhachHang.Count();
            TongBan = TinhTongThanhTien(DSKhachHang);
        }
        public double TinhTongThanhTien(IEnumerable<KHACHHANG> DSKhachHang)
        {
            double total = 0;
            foreach (var item in DSKhachHang)
            {
                total += (double)item.TongTienMuaKH;
            }
            return total;
        }
    }
}
