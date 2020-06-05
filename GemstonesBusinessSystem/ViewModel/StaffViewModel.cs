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
    class StaffViewModel : BaseViewModel
    {

        #region command
        public ICommand ThemNVCommand { get; set; }

        public ICommand XoaNhanVien { get; set; }

        public ICommand ChiTietNVCommand { get; set; }

        public ICommand TimKiemCommand { get; set; }

        public ICommand XemLuongNVCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand XuatExcelCommand { get; set; }
        public RelayCommand<IList> SelectionChangedCommand { get; set; }
        #endregion

        #region list binding

        private IEnumerable<NHANVIEN> _DSNhanVien;
        public IEnumerable<NHANVIEN> DSNhanVien { get => _DSNhanVien; set { _DSNhanVien = value; OnPropertyChanged(); } }

        private ObservableCollection<NHANVIEN> _DSNVDaChon;
        public ObservableCollection<NHANVIEN> DSNVDaChon { get => _DSNVDaChon; set { _DSNVDaChon = value; OnPropertyChanged(); } }

        #endregion

        #region thuộc tính binding
        private NHANVIEN _NVDaChon;
        public NHANVIEN NVDaChon { get => _NVDaChon; set { _NVDaChon = value; OnPropertyChanged(); } }

        private int _SLNhanVien;
        public int SLNhanVien { get => _SLNhanVien; set { _SLNhanVien = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        public StaffViewModel()
        {


            #region command
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
                        a.Workbook.Properties.Title = "Danh sách nhân viên";

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
                                                "Mã nhân viên",
                                                "Tên nhân viên",
                                                "Số điện thoại",
                                                "Lương cơ bản",
                                                "Phần trăm hoa hồng"
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = "Thống kê danh sách nhân viên";
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
                        List<NHANVIEN> userList = DSNhanVien.ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.MaNhanVien.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TenNhanVien.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.SoDienThoaiNV.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.LuongCoBan.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.PhanTramHoaHong.ToString();

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
                status = 0;
            });

            // Thêm nhân viên
            ThemNVCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                AddStaffWindow addStaffWindow = new AddStaffWindow();
                var addstaffvm = addStaffWindow.DataContext as AddStaffViewModel;
                addStaffWindow.ShowDialog();
                LayDSTuDatabase();
            });

            // Xem lương nhân viên
            XemLuongNVCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                SalaryWindow salaryWindow = new SalaryWindow();
                salaryWindow.ShowDialog();
            });

            // Lấy danh sách được chọn từ dataGrid
            SelectionChangedCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                DSNVDaChon.Clear();
                foreach (var item in items)
                    DSNVDaChon.Add((NHANVIEN)item);
            });

            // Xem lại xóa account không xóa nhân viên
            //DeleteStaffCommand = new RelayCommand<Object>((p) =>
            //{
            //    if (selectedStaff.Count > 0)
            //    {
            //        return true;
            //    }
            //    return false;
            //}, (p) =>
            //{
            //    MessageBoxResult isExit = System.Windows.MessageBox.Show("Dữ liệu đã xóa không thể khôi phục, bạn có chắc chắn xóa?", "Xác nhận", MessageBoxButton.OKCancel);
            //    if (isExit == MessageBoxResult.OK)
            //    {
            //        try
            //        {
            //            foreach (var item in listStaff.ToList())
            //            {
            //                foreach (var selected in selectedStaff)
            //                {
            //                    if (item.id == selected.id)
            //                    {
            //                        DataProvider.Ins.DB.Staffs.Remove(listStaff.Where(x => x.id == item.id).SingleOrDefault());
            //                        DataProvider.Ins.DB.SaveChanges();
            //                    }
            //                }
            //            }
            //            MessageBox.Show("Xóa thành công");
            //        }
            //        catch (Exception e)
            //        {
            //            MessageBox.Show("Xóa thất bại vui lòng thử lại");
            //        }
            //    }
            //    LoadListStaff();
            //});

            // Chi tiết nhân viên
            ChiTietNVCommand = new RelayCommand<Object>((p) => {
                if (NVDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                InfoStaffWindow infoStaffWindow = new InfoStaffWindow();
                (infoStaffWindow.DataContext as InfoStaffViewModel).LoadNhanVien(NVDaChon);
                infoStaffWindow.ShowDialog();
                LayDSTuDatabase();
            });

            // Tìm kiếm
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) != "" && DSNhanVien != null)
                {
                    return true;
                }
                else
                { 
                    LayDSTuDatabase();
                    return false;
                }
            }, (p) =>
            {
                DSNhanVien = DSNhanVien.Where(x => x.MaNhanVien.ToString().Contains(TimKiem.ToLower())
                                                    || x.TenNhanVien.ToLower().Contains(TimKiem.ToLower())
                                                    || x.EmailNV.ToLower().Contains(TimKiem.ToLower())
                                                    || x.SoDienThoaiNV.ToLower().Contains(TimKiem.ToLower())
                                                    || x.DiaChiNV.ToLower().Contains(TimKiem.ToLower()));
                SLNhanVien = DSNhanVien.Count();
            });

            #endregion
        }
        public void LayDSTuDatabase()
        {
            //var RoleAdmin = DataProvider.Ins.DB.AccountRoles.Where(x => x.code == Constant.codeRoleADMIN).SingleOrDefault();
            //var AccountADMIN = DataProvider.Ins.DB.Accounts.Where(x => x.account_roles_id == RoleAdmin.id).SingleOrDefault();
            //DSNhanVien = new ObservableCollection<Staff>(DataProvider.Ins.DB.Staffs.Where(x => x.account_id != AccountADMIN.id));
            DSNhanVien = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            DSNVDaChon = new ObservableCollection<NHANVIEN>();
            SLNhanVien = DSNhanVien.Count();
        }
    }
}
