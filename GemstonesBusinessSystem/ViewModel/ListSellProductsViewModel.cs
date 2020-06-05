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
    class ListSellProductsViewModel : BaseViewModel
    {
        #region List binding
        private ObservableCollection<PHIEUBANHANG> _DSPhieuBanHangDB;
        public ObservableCollection<PHIEUBANHANG> DSPhieuBanHangDB { get => _DSPhieuBanHangDB; set { _DSPhieuBanHangDB = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUBANHANG> _DSPhieuBanHang;
        public IEnumerable<PHIEUBANHANG> DSPhieuBanHang { get => _DSPhieuBanHang; set { _DSPhieuBanHang = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUBANHANG> _DSHDDaChon;
        public ObservableCollection<PHIEUBANHANG> DSHDDaChon { get => _DSHDDaChon; set { _DSHDDaChon = value; OnPropertyChanged(); } }
        #endregion

        #region thuộc tính binding

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private string _TongSLHD;
        public string TongSLHD { get => _TongSLHD; set { _TongSLHD = value; OnPropertyChanged(); } }

        private PHIEUBANHANG _HDDaChon;
        public PHIEUBANHANG HDDaChon { get => _HDDaChon; set { _HDDaChon = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        #region command
        public ICommand TimKiemCommand { get; set; }
        public ICommand ChiTietHDCommand { get; set; }
        public ICommand XoaHDCommand { get; set; }
        public ICommand ThemPhieuBHCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand XuatExcelCommand { get; set; }
        public RelayCommand<IList> SelectionChangedCommand { get; set; }
        #endregion
        public ListSellProductsViewModel()
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
                        a.Workbook.Properties.Title = "Danh sách phiếu bán hàng";

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
                                                "Số phiếu",
                                                "Ngày tạo",
                                                "Tên khách hàng",
                                                "Tổng số lượng",
                                                "Tổng tiền",
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = "Thống kê danh sách phiếu bán hàng";
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
                        List<PHIEUBANHANG> userList = DSPhieuBanHang.ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.MaPhieuBanHang.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.NgayLapPhieuBan.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.KHACHHANG.TenKhachHang.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TongSoLuongBan.ToString();

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
                LoadDSPhieuBanHang();
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
                        DSHDDaChon.Add((PHIEUBANHANG)item);
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
                DetailSellProductWindow detailProduct = new DetailSellProductWindow();
                (detailProduct.DataContext as DetailSellProductViewModel).LoadChiTietPhieuBanHang(HDDaChon);
                detailProduct.ShowDialog();
                LayDSTuDatabase();
                LoadDSPhieuBanHang();
            });

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) != "" && DSPhieuBanHang != null)
                {
                    return true;
                }
                LoadDSPhieuBanHang();
                return false;
            }, (p) =>
            {
                try
                {
                    DSPhieuBanHang = DSPhieuBanHang.Where(x => (x.MaPhieuBanHang.ToString().Contains(TimKiem.ToLower())
                                                        || x.NgayLapPhieuBan.Value.ToShortDateString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.KHACHHANG.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongSoLuongBan.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongThanhTien.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.NHANVIEN.TenNhanVien.ToLower().Contains(TimKiem.ToLower())));
                    TongSLHD = DSPhieuBanHang.Count().ToString();
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
                            var DSChiTietHD = DataProvider.Ins.DB.CT_PBH.Where(x => x.MaPhieuBanHang == HD.MaPhieuBanHang);
                            // Xóa những chi tiết hóa đơn liên quan
                            foreach (var CTHD in DSChiTietHD)
                            {
                                DataProvider.Ins.DB.CT_PBH.Remove(CTHD);
                            }

                            // Xóa hóa đơn
                            DataProvider.Ins.DB.PHIEUBANHANGs.Remove(HD);
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        MessageBox.Show("Xóa thành công");
                        LayDSTuDatabase();
                        LoadDSPhieuBanHang();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Xóa thất bại vui lòng thử lại");
                    }
                }
            });

            //Thêm phiếu bán hàng
            ThemPhieuBHCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                NewBillSellProductWindow billSellProductWindow = new NewBillSellProductWindow();
                p.Hide();
                billSellProductWindow.ShowDialog();
                p.Show();
                // Load lại dữ liệu
                ListSellProductsViewModel.status = 1;
            });
            #endregion 
        }
        public void LoadDSPhieuBanHang()
        {
            DSPhieuBanHang = DSPhieuBanHangDB;
            TongSLHD = DSPhieuBanHang.Count().ToString();
           
        }
        public void LayDSTuDatabase()
        {
            DSPhieuBanHangDB = new ObservableCollection<PHIEUBANHANG>(DataProvider.Ins.DB.PHIEUBANHANGs);
            DSHDDaChon = new ObservableCollection<PHIEUBANHANG>();
        }
    }
}
