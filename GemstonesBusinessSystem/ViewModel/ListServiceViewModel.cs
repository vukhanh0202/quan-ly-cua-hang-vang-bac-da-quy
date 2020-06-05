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
    class ListServiceViewModel : BaseViewModel
    {
        #region Chuỗi hằng
        public const string strActive = "Danh sách dịch vụ đang hoạt động";
        public const string strInActive = "Danh sách dịch vụ ngừng hoạt động";
        #endregion

        #region List binding

        private ObservableCollection<DICHVU> _DSDichVuDB;
        public ObservableCollection<DICHVU> DSDichVuDB { get => _DSDichVuDB; set { _DSDichVuDB = value; OnPropertyChanged(); } }

        private IEnumerable<DICHVU> _DSDichVu;
        public IEnumerable<DICHVU> DSDichVu { get => _DSDichVu; set { _DSDichVu = value; OnPropertyChanged(); } }

        private ObservableCollection<DICHVU> _DSDichVuDaChon;
        public ObservableCollection<DICHVU> DSDichVuDaChon { get => _DSDichVuDaChon; set { _DSDichVuDaChon = value; OnPropertyChanged(); } }
        #endregion

        #region Thuộc tính Binding
        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private string _TongSLDichVu;
        public string TongSLDichVu { get => _TongSLDichVu; set { _TongSLDichVu = value; OnPropertyChanged(); } }

        private string _TieuDe;
        public string TieuDe { get => _TieuDe; set { _TieuDe = value; OnPropertyChanged(); } }

        private DICHVU _DVDaChon;
        public DICHVU DVDaChon { get => _DVDaChon; set { _DVDaChon = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        #region Command
        public ICommand TimKiemCommand { get; set; }
        public ICommand XoaDichVuCommand { get; set; }
        public ICommand ThemDichVuCommand { get; set; }
        public ICommand ChiTietCommand { get; set; }
        public ICommand DSDichVuHDCommand { get; set; }
        public ICommand DSDichVuNHDCommand { get; set; }
        public ICommand KhoiPhucCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand XuatExcelCommand { get; set; }
        public RelayCommand<IList> SelectionChangedCommand { get; set; }

        #endregion

        public ListServiceViewModel()
        {

            #region Command
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
                        a.Workbook.Properties.Title = "Danh sách dịch vụ";

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
                                                "Mã dịch vụ",
                                                "Loại dịch vụ",
                                                "Đơn giá dịch vụ"
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = "Thống kê danh sách dịch vụ";
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
                        List<DICHVU> userList = DSDichVu.ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.MaDichVu.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TenDichVu.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.DonGiaDV.ToString();


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
                TieuDe = strActive;
                DSDichVuDaChon = new ObservableCollection<DICHVU>();
                LayDSTuDatabase();
                LoadDSDichVuHD();
                status = 0;
            });

            // Khôi phục dịch vụ
            KhoiPhucCommand = new RelayCommand<IList>((items) =>
            {
                if (TieuDe == strActive)
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ ngưng hoạt động");
                    return false;
                }
                else if (TieuDe == strInActive)
                {
                    if (DSDichVuDaChon.Count > 0)
                    {
                        return true;
                    }
                }
                return false;
            }, (items) =>
            {
                try
                {
                    foreach (var DV in DSDichVuDaChon)
                    {
                        DataProvider.Ins.DB.DICHVUs.Where(x => x.MaDichVu == DV.MaDichVu).SingleOrDefault().TrangThaiDV = Constant.ACTIVE_STATUS;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    LayDSTuDatabase();
                    LoadDSDichVuNHD();
                }
                catch (Exception)
                {
                }

            });

            // Lấy danh sách được chọn từ dataGrids
            SelectionChangedCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                try
                {
                    DSDichVuDaChon.Clear();
                    foreach (var item in items)
                        DSDichVuDaChon.Add((DICHVU)item);
                }
                catch (Exception)
                {
                }

            });

            // Load danh sách đang hoạt động
            DSDichVuHDCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                LoadDSDichVuHD();
            });

            // Load danh sách ngừng hoạt động
            DSDichVuNHDCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                LoadDSDichVuNHD();
            });


            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) != "" && DSDichVu != null)
                {
                    return true;
                }
                else
                {
                    if (TieuDe == strActive)
                    {
                        LoadDSDichVuHD();
                    }
                    else if (TieuDe == strInActive)
                    {
                        LoadDSDichVuNHD();
                    }
                    return false;
                }
            }, (p) =>
            {
                try
                {
                    DSDichVu = DSDichVu.Where(x => (x.MaDichVu.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.TenDichVu.ToLower().Contains(TimKiem.ToLower())
                                                        || x.DonGiaDV.ToString().ToLower().Contains(TimKiem.ToLower())));
                    TongSLDichVu = DSDichVu.Count().ToString();
                }
                catch (Exception e) { }
            });

            // Xóa service
            XoaDichVuCommand = new RelayCommand<Object>((p) =>
            {
                if (DSDichVuDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                if (TieuDe == strActive)
                {
                    MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn có chắc chắn ngưng hoạt động dịch vụ?", "Xác nhận", MessageBoxButton.OKCancel);
                    if (isExit == MessageBoxResult.OK)
                    {
                        try
                        {
                            foreach (var DV in DSDichVuDaChon)
                            {

                                DataProvider.Ins.DB.DICHVUs.Where(x => x.MaDichVu == DV.MaDichVu).SingleOrDefault().TrangThaiDV = Constant.INACTIVE_STATUS;
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            MessageBox.Show("Ngưng hoạt động thành công");
                            LayDSTuDatabase();
                            LoadDSDichVuHD();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Xảy ra một số lỗi, vui lòng kiểm tra lại!");
                        }
                    }
                }
                else if (TieuDe == strInActive)
                {
                    MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn có chắc chắn muốn thực hiện hành động này?", "Xác nhận", MessageBoxButton.OKCancel);
                    if (isExit == MessageBoxResult.OK)
                    {
                        try
                        {
                            foreach (var DV in DSDichVuDaChon)
                            {

                                var HDDichVu = DataProvider.Ins.DB.CT_PDV.Where(x => x.MaDichVu == DV.MaDichVu).FirstOrDefault();

                                if (HDDichVu !=null)
                                {
                                    MessageBox.Show("Dịch vụ đang được sử dụng, vui lòng xóa các dữ liệu liên quan!");
                                }
                                else
                                {
                                    DataProvider.Ins.DB.DICHVUs.Remove(DV);
                                    DataProvider.Ins.DB.SaveChanges();
                                    MessageBox.Show("Xóa thành công");
                                }
                            }
                            LayDSTuDatabase();
                            LoadDSDichVuNHD();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Một số lỗi xảy ra vui lòng kiểm tra lại");
                        }
                    }
                }
            });

            // Thêm service
            ThemDichVuCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                NewServiceWindow newServiceWindow = new NewServiceWindow();
                newServiceWindow.ShowDialog();
                LayDSTuDatabase();
                LoadDSDichVuHD();
            });



            //Chi tiết dịch vụ
            ChiTietCommand = new RelayCommand<object>((p) =>
            {
                if (DVDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                DetailServiceWindow detailService = new DetailServiceWindow();
                (detailService.DataContext as DetailServiceViewModel).LoadChiTietDichVu(DVDaChon);
                detailService.ShowDialog();
                DVDaChon = null;
                LayDSTuDatabase();
                if (TieuDe == strActive)
                {
                    LoadDSDichVuHD();
                }
                else if (TieuDe == strInActive)
                {
                    LoadDSDichVuNHD();
                }
            });
            #endregion
        }
        public void LayDSTuDatabase()
        {
            DSDichVuDB = new ObservableCollection<DICHVU>(DataProvider.Ins.DB.DICHVUs);
        }
        public void LoadDSDichVuHD()
        {
            TieuDe = strActive;
            DSDichVu = DSDichVuDB.Where(x => x.TrangThaiDV == Constant.ACTIVE_STATUS);
            TongSLDichVu = DSDichVu.Count().ToString();
        }

        public void LoadDSDichVuNHD()
        {
            TieuDe = strInActive;
            DSDichVu = DSDichVuDB.Where(x => x.TrangThaiDV == Constant.INACTIVE_STATUS);
            TongSLDichVu = DSDichVu.Count().ToString();
        }
    }
}
