using GemstonesBusinessSystem.Model;
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
    class ListBuyProductViewModel : BaseViewModel
    {
        #region List binding
        private ObservableCollection<PHIEUMUAHANG> _DSPhieuMuaHangDB;
        public ObservableCollection<PHIEUMUAHANG> DSPhieuMuaHangDB { get => _DSPhieuMuaHangDB; set { _DSPhieuMuaHangDB = value; OnPropertyChanged(); } }

        private IEnumerable<CT_PMH> _DSCTPhieuMuaHangDB;
        public IEnumerable<CT_PMH> DSCTPhieuMuaHangDB { get => _DSCTPhieuMuaHangDB; set { _DSCTPhieuMuaHangDB = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUMUAHANG> _DSPhieuMuaHang;
        public IEnumerable<PHIEUMUAHANG> DSPhieuMuaHang { get => _DSPhieuMuaHang; set { _DSPhieuMuaHang = value; OnPropertyChanged(); } }

        private IEnumerable<NHAPKHO> _DSTongSPNhapKho;
        public IEnumerable<NHAPKHO> DSTongSPNhapKho { get => _DSTongSPNhapKho; set { _DSTongSPNhapKho = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUMUAHANG> _DSHDDaChon;
        public ObservableCollection<PHIEUMUAHANG> DSHDDaChon { get => _DSHDDaChon; set { _DSHDDaChon = value; OnPropertyChanged(); } }
        #endregion

        #region thuộc tính binding

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private string _TimKiemNhapKho;
        public string TimKiemNhapKho { get => _TimKiemNhapKho; set { _TimKiemNhapKho = value; OnPropertyChanged(); } }

        private int _TongSLHD;
        public int TongSLHD { get => _TongSLHD; set { _TongSLHD = value; OnPropertyChanged(); } }

        private PHIEUMUAHANG _HDDaChon;
        public PHIEUMUAHANG HDDaChon { get => _HDDaChon; set { _HDDaChon = value; OnPropertyChanged(); } }

        private DateTime _NgayBatDau;
        public DateTime NgayBatDau { get => _NgayBatDau; set { _NgayBatDau = value; OnPropertyChanged(); } }

        private DateTime _NgayKetThuc;
        public DateTime NgayKetThuc { get => _NgayKetThuc; set { _NgayKetThuc = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        #region command
        public ICommand TimKiemCommand { get; set; }
        public ICommand TimKiemNhapKhoCommand { get; set; }
        public ICommand ToanThoiGianCommand { get; set; }
        public ICommand TuyChinhDSNhapKhoCommand { get; set; }
        public ICommand ChiTietHDCommand { get; set; }
        public ICommand XoaHDCommand { get; set; }
        public ICommand ThemPhieuMHCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand XuatExcelCommand { get; set; }
        public ICommand XuatExcelCommandTMH { get; set; }

        public RelayCommand<IList> SelectionChangedCommand { get; set; }
        #endregion
        public ListBuyProductViewModel()
        {
            #region command

            NgayBatDau = DateTime.Now.Date.AddMonths(-1);
            NgayKetThuc = DateTime.Now.Date;
            //Xuất excel PMH
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
                        a.Workbook.Properties.Title = "Danh sách phiếu mua hàng";

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
                                                "Mã phiếu",
                                                "Tên cung cấp",
                                                "Ngày nhập",
                                                "Tổng số lượng",
                                                "Tổng thành tiền"
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = "Thống kê danh sách phiếu mua hàng";
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
                        List<PHIEUMUAHANG> userList = DSPhieuMuaHang.ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.MaPhieuMuaHang.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.NHACUNGCAP.TenNhaCungCap.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.NgayLapPhieuMua.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TongSoLuongMua.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TongThanhTien.ToString();

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
            //Xuất excel TMH
            XuatExcelCommandTMH = new RelayCommand<Object>((p) =>
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
                        a.Workbook.Properties.Title = "Danh sách tổng mua hàng";

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
                                                "Mã sản phẩm",
                                                "Tên sản phẩm",
                                                "Loại sản phẩm",
                                                "Thời gian nhập",
                                                "Số lượng nhập",
                                                "Vốn nhập kho"
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = "Thống kê danh sách sản phẩm";
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
                        List<NHAPKHO> userList = DSTongSPNhapKho.ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.MaSanPham.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.SANPHAM.TenSanPham.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.SANPHAM.LOAISANPHAM.TenLoaiSanPham.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.ThoiGianNhap.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.SoLuongNhap.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.VonNhapKho.ToString();

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
                LayDSTuDatabase();
                LoadDS();
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
                    DSHDDaChon.Clear();
                    foreach (var item in items)
                        DSHDDaChon.Add((PHIEUMUAHANG)item);
                }
                catch (Exception)
                {
                }

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
                }
                catch (Exception)
                {
                }

            });


            // Tùy chỉnh thời gian nhập kho
            TuyChinhDSNhapKhoCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    DSCTPhieuMuaHangDB = DSCTPhieuMuaHangDB.Where(x => x.PHIEUMUAHANG.NgayLapPhieuMua.Value.Date >= NgayBatDau.Date && x.PHIEUMUAHANG.NgayLapPhieuMua.Value.Date <= NgayKetThuc.Date);
                    LoadDS();
                }
                catch (Exception)
                {
                }

            });


            // Chi tiết bill
            ChiTietHDCommand = new RelayCommand<object>((p) =>
            {
                if (HDDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                DetailBuyProductWindow detailProduct = new DetailBuyProductWindow();
                (detailProduct.DataContext as DetailBuyProductViewModel).LoadChiTietPhieuMuaHang(HDDaChon);
                detailProduct.ShowDialog();
                HDDaChon = null;
                LayDSTuDatabase();
                LoadDS();
            });

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) != "" && DSPhieuMuaHang != null)
                {
                    return true;
                }
                LoadDS();
                return false;
            }, (p) =>
            {
                try
                {
                    DSPhieuMuaHang = DSPhieuMuaHang.Where(x => (x.MaPhieuMuaHang.ToString().Contains(TimKiem.ToLower())
                                                        || x.NgayLapPhieuMua.Value.ToShortDateString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.NHACUNGCAP.TenNhaCungCap.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongSoLuongMua.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongThanhTien.ToString().ToLower().Contains(TimKiem.ToLower())
                                                       ));
                    TongSLHD = DSPhieuMuaHang.Count();
                }
                catch (Exception e) { }
            });

            // Lọc dữ liệu từ search
            TimKiemNhapKhoCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiemNhapKho) != "" && DSTongSPNhapKho != null)
                {
                    return true;
                }
                LoadDS();
                return false;
            }, (p) =>
            {
                try
                {
                    DSTongSPNhapKho = DSTongSPNhapKho.Where(x => (x.MaSanPham.ToString().Contains(TimKiemNhapKho.ToLower())
                                                        || x.SANPHAM.TenSanPham.ToLower().Contains(TimKiemNhapKho.ToLower())
                                                        || x.SANPHAM.LOAISANPHAM.TenLoaiSanPham.ToLower().Contains(TimKiemNhapKho.ToLower())
                                                        || x.SoLuongNhap.ToString().ToLower().Contains(TimKiemNhapKho.ToLower())
                                                        || x.VonNhapKho.ToString().ToLower().Contains(TimKiemNhapKho.ToLower())
                                                        || x.ThoiGianNhap.ToShortDateString().ToLower().Contains(TimKiemNhapKho.ToLower())
                                                       ));
                }
                catch (Exception e) { }
            });

            // Xóa bill
            XoaHDCommand = new RelayCommand<Object>((p) =>
            {
                if (DSHDDaChon.Count > 0)
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
                        foreach (var HD in DSHDDaChon)
                        {
                            // Lấy ra danh sách tất cả CT_PBH của Hóa đơn
                            var DSChiTietHD = DataProvider.Ins.DB.CT_PMH.Where(x => x.MaPhieuMuaHang == HD.MaPhieuMuaHang);
                            // Xóa những chi tiết hóa đơn liên quan
                            foreach (var CTHD in DSChiTietHD)
                            {
                                DataProvider.Ins.DB.CT_PMH.Remove(CTHD);
                            }

                            // Xóa hóa đơn
                            DataProvider.Ins.DB.PHIEUMUAHANGs.Remove(HD);
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        MessageBox.Show("Xóa thành công");
                        LayDSTuDatabase();
                        LoadDS();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Xóa thất bại vui lòng thử lại");
                    }
                }
            });

            //Thêm phiếu mua hàng
            ThemPhieuMHCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                NewBillBuyProductWindow billBuyProductWindow = new NewBillBuyProductWindow();
                p.Hide();
                billBuyProductWindow.ShowDialog();
                p.Show();
                // Load lại dữ liệu
                ListBuyProductViewModel.status = 1;
            });
            #endregion 
        }
        public void LoadDS()
        {
            DSPhieuMuaHang = DSPhieuMuaHangDB;
            TongSLHD = DSPhieuMuaHang.Count();

            ObservableCollection<NHAPKHO> DSNhapKhoList = new ObservableCollection<NHAPKHO>();
            foreach (var item in DSCTPhieuMuaHangDB)
            {
                // Sản phẩm chưa tồn tại trong ds nhập kho
                if (DSNhapKhoList.Any(x=>x.MaSanPham == item.MaSanPham) == false)
                {
                    DSNhapKhoList.Add(new NHAPKHO()
                    {
                        MaSanPham = item.MaSanPham.Value,
                        SANPHAM= item.SANPHAM,
                        SoLuongNhap = DSCTPhieuMuaHangDB.Where(x=>x.MaSanPham == item.MaSanPham).Sum(x=>x.SoLuongMua).Value,
                        VonNhapKho = DSCTPhieuMuaHangDB.Where(x=>x.MaSanPham == item.MaSanPham).Sum(x=>x.ThanhTien).Value,
                        ThoiGianNhap = item.PHIEUMUAHANG.NgayLapPhieuMua.Value
                    });
                }
            }

            DSTongSPNhapKho = DSNhapKhoList;
        }
        public void LayDSTuDatabase()
        {
            DSPhieuMuaHangDB = new ObservableCollection<PHIEUMUAHANG>(DataProvider.Ins.DB.PHIEUMUAHANGs.Where(x=>x.MaNhaCungCap !=null));
            DSCTPhieuMuaHangDB = new ObservableCollection<CT_PMH>(DataProvider.Ins.DB.CT_PMH.Where(x => x.PhuongThuc != Constant.methodInitProduct));
            DSHDDaChon = new ObservableCollection<PHIEUMUAHANG>();
        }

        public class NHAPKHO : BaseViewModel
        {
            private int _MaSanPham;
            public int MaSanPham { get => _MaSanPham; set { _MaSanPham = value; OnPropertyChanged(); } }

            private SANPHAM _SANPHAM;
            public SANPHAM SANPHAM { get => _SANPHAM; set { _SANPHAM = value; OnPropertyChanged(); } }

            private int _SoLuongNhap;
            public int SoLuongNhap { get => _SoLuongNhap; set { _SoLuongNhap = value; OnPropertyChanged(); } }

            private double _VonNhapKho;
            public double VonNhapKho { get => _VonNhapKho; set { _VonNhapKho = value; OnPropertyChanged(); } }

            private DateTime _ThoiGianNhap;
            public DateTime ThoiGianNhap { get => _ThoiGianNhap; set { _ThoiGianNhap = value; OnPropertyChanged(); } }

            public NHAPKHO () { }
        }
    }
}
