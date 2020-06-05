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
    class ListBillServiceViewModel : BaseViewModel
    {
        #region Binding
        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private string _TongSLHD;
        public string TongSLHD { get => _TongSLHD; set { _TongSLHD = value; OnPropertyChanged(); } }

        private string _TongSLHDHoanThanh;
        public string TongSLHDHoanThanh { get => _TongSLHDHoanThanh; set { _TongSLHDHoanThanh = value; OnPropertyChanged(); } }

        private string _TongSLHDChuaHoanThanh;
        public string TongSLHDChuaHoanThanh { get => _TongSLHDChuaHoanThanh; set { _TongSLHDChuaHoanThanh = value; OnPropertyChanged(); } }

        private PHIEUDICHVU _PhieuDVDaChon;
        public PHIEUDICHVU PhieuDVDaChon { get => _PhieuDVDaChon; set { _PhieuDVDaChon = value; OnPropertyChanged(); } }

        public static int status;

        #endregion

        #region List Binding

        private IEnumerable<PHIEUDICHVU> _DSPhieuDVDB;
        public IEnumerable<PHIEUDICHVU> DVPhieuDVDB { get => _DSPhieuDVDB; set { _DSPhieuDVDB = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUDICHVU> _DVPhieuDV;
        public IEnumerable<PHIEUDICHVU> DSPhieuDV { get => _DVPhieuDV; set { _DVPhieuDV = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUDICHVU> _DSPhieuDVHoanThanh;
        public IEnumerable<PHIEUDICHVU> DSPhieuDVHoanThanh { get => _DSPhieuDVHoanThanh; set { _DSPhieuDVHoanThanh = value; OnPropertyChanged(); } }

        private IEnumerable<PHIEUDICHVU> _DSPhieuDVChuaHoanThanh;
        public IEnumerable<PHIEUDICHVU> DSPhieuDVChuaHoanThanh { get => _DSPhieuDVChuaHoanThanh; set { _DSPhieuDVChuaHoanThanh = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUDICHVU> _DSPhieuDVDaChon;
        public ObservableCollection<PHIEUDICHVU> DSPhieuDVDaChon { get => _DSPhieuDVDaChon; set { _DSPhieuDVDaChon = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand TimKiemCommand { get; set; }

        public ICommand XoaPhieuDVCommand { get; set; }

        public ICommand ThemPhieuDVCommand { get; set; }

        public ICommand ChiTietCommand { get; set; }

        public ICommand ReloadCommand { get; set; }
        public ICommand XuatExcelCommand { get; set; }

        public RelayCommand<IList> SelectionChangedCommand { get; set; }
        #endregion

        public ListBillServiceViewModel()
        {
            //Xuất excel
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
                        a.Workbook.Properties.Title = "Danh sách phiếu dịch vụ";

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
                                                "Mã đơn hàng",
                                                "Ngày tạo đơn",
                                                "Tên khách hàng",
                                                "Tổng tiền",
                                                "Tổng trả trước",
                                                "Tổng còn lại"
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = "Thống kê danh sách phiếu dịch vụ";
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
                        List<PHIEUDICHVU> userList = DSPhieuDV.ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.MaPhieuDichVu.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.NgayLapPhieuDichVu.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.KHACHHANG.TenKhachHang.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TongThanhTien.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TongTienTraTruoc.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TongTienConLai.ToString();

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
                DSPhieuDVDaChon = new ObservableCollection<PHIEUDICHVU>();
                LayDSTuDatabase();
                LoadDSPhieuDV();
                LoadDSPhieuDVHoanThanh();
                LoadDSPhieuDVChuaHoanThanh();
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
                    DSPhieuDVDaChon.Clear();
                    foreach (var item in items)
                        DSPhieuDVDaChon.Add((PHIEUDICHVU)item);
                }
                catch (Exception)
                {
                }
            });

            // Chi tiết bill
            ChiTietCommand = new RelayCommand<object>((p) =>
            {
                if (PhieuDVDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                DetailBillServiceWindow Chitiet = new DetailBillServiceWindow();
                (Chitiet.DataContext as DetailBillServiceViewModel).LoadChiTietPhieuDichVu(PhieuDVDaChon);
                Chitiet.ShowDialog();
                ListBillServiceViewModel.status = 1;
            });

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) != "" && DSPhieuDV != null)
                {
                    return true;
                }
                else
                {
                    LoadDSPhieuDV();
                    LoadDSPhieuDVHoanThanh();
                    LoadDSPhieuDVChuaHoanThanh();
                    return false;
                }
            }, (p) =>
            {
                try
                {
                    TimKiem.ToLower();
                    // Search tất cả bill
                    DSPhieuDV = DSPhieuDV.Where(x => (x.MaPhieuDichVu.ToString().Contains(TimKiem.ToLower())
                                                        || x.KHACHHANG.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongSoLuongDichVu.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.NgayLapPhieuDichVu.Value.ToShortDateString().ToLower().Contains(TimKiem.ToLower())));
                    TongSLHD = DSPhieuDV.Count().ToString();

                    // Search bill hoàn thành
                    DSPhieuDVHoanThanh = DSPhieuDVHoanThanh.Where(x => (x.MaPhieuDichVu.ToString().Contains(TimKiem.ToLower())
                                                        || x.KHACHHANG.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongSoLuongDichVu.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.NgayLapPhieuDichVu.Value.ToShortDateString().ToLower().Contains(TimKiem.ToLower())));
                    TongSLHDHoanThanh = DSPhieuDVHoanThanh.Count().ToString();
                    // Search bill chưa hoàn thành

                    DSPhieuDVChuaHoanThanh = DSPhieuDVChuaHoanThanh.Where(x => (x.MaPhieuDichVu.ToString().Contains(TimKiem.ToLower())
                                                        || x.KHACHHANG.TenKhachHang.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongSoLuongDichVu.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.NgayLapPhieuDichVu.Value.ToShortDateString().ToLower().Contains(TimKiem.ToLower())));
                    TongSLHDChuaHoanThanh = DSPhieuDVChuaHoanThanh.Count().ToString();
                }
                catch (Exception) { }
            });

            XoaPhieuDVCommand = new RelayCommand<Object>((p) =>
            {
                if (DSPhieuDVDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn có chắc chắn xóa hóa đơn bạn vừa chọn ?", "Xác nhận", MessageBoxButton.OKCancel);
                if (isExit == MessageBoxResult.OK)
                {
                    try
                    {
                        foreach (var PhieuDV in DSPhieuDVDaChon)
                        {
                            DataProvider.Ins.DB.PHIEUDICHVUs.Remove(PhieuDV);

                            var DSCT_PDV = DataProvider.Ins.DB.CT_PDV.Where(x => x.MaPhieuDichVu == PhieuDV.MaPhieuDichVu);
                            foreach (var i in DSCT_PDV)
                            {
                                DataProvider.Ins.DB.CT_PDV.Remove(i);
                            }
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        MessageBox.Show("Xóa hóa đơn thành công");
                        LayDSTuDatabase();
                        LoadDSPhieuDV();
                        LoadDSPhieuDVChuaHoanThanh();
                        LoadDSPhieuDVHoanThanh();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Xóa bill thất bại vui lòng thử lại");
                    }
                }

            });

            //Thêm phiếu dịch vụ
            ThemPhieuDVCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                NewBillServiceWindow billServiceWindow = new NewBillServiceWindow();
                p.Hide();
                billServiceWindow.ShowDialog();
                p.Show();
                // Load lại dữ liệu
                ListBillServiceViewModel.status = 1;
            });



        }
        public void LayDSTuDatabase()
        {
            DVPhieuDVDB = new ObservableCollection<PHIEUDICHVU>(DataProvider.Ins.DB.PHIEUDICHVUs);
        }

        public void LoadDSPhieuDV()
        {
            try
            {
                PhieuDVDaChon = null;
                DSPhieuDV = DVPhieuDVDB;
                TongSLHD = DSPhieuDV.Count().ToString();
            }
            catch (Exception){}
        }

        public void LoadDSPhieuDVHoanThanh()
        {
            try
            {
                PhieuDVDaChon = null;
                DSPhieuDVHoanThanh = DVPhieuDVDB.Where(x => x.TinhTrangPDV == Constant.ACTIVE_STATUS);
                TongSLHDHoanThanh = DSPhieuDVHoanThanh.Count().ToString();
            }
            catch (Exception){}
        }
        public void LoadDSPhieuDVChuaHoanThanh()
        {
            try
            {
                PhieuDVDaChon = null;
                DSPhieuDVChuaHoanThanh = DVPhieuDVDB.Where(x => x.TinhTrangPDV == Constant.INACTIVE_STATUS);
                TongSLHDChuaHoanThanh = DSPhieuDVChuaHoanThanh.Count().ToString();
            }
            catch (Exception) {}
           
        }
    }
}
