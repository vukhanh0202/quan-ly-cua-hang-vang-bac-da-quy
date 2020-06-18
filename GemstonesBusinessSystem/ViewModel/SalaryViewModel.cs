using GemstonesBusinessSystem.Model;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
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
    class SalaryViewModel : BaseViewModel
    {
        #region command
        public ICommand Filter { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XuatExcelCommand { get; set; }

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
            ////Xuất excel
            //XuatExcelCommand = new RelayCommand<Object>((p) =>
            //{
            //    return true;
            //}, (p) =>
            //{
            //    string filePath = "";
            //    // tạo SaveFileDialog để lưu file excel
            //    SaveFileDialog dialog = new SaveFileDialog();

            //    // chỉ lọc ra các file có định dạng Excel
            //    dialog.Filter = "Excel | *.xlsx | Excel 2019 | *.xls";

            //    // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
            //    if (dialog.ShowDialog() == true)
            //    {
            //        filePath = dialog.FileName;
            //    }

            //    // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            //    if (string.IsNullOrEmpty(filePath))
            //    {
            //        MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
            //        return;
            //    }

            //    try
            //    {
            //        using (ExcelPackage a = new ExcelPackage())
            //        {
            //            // đặt tiêu đề cho file
            //            a.Workbook.Properties.Title = "Danh sách tính lương nhân viên";

            //            //Tạo một sheet để làm việc trên đó
            //            a.Workbook.Worksheets.Add("Danh sách");

            //            // lấy sheet vừa add ra để thao tác
            //            ExcelWorksheet ws = a.Workbook.Worksheets[1];

            //            // đặt tên cho sheet
            //            ws.Name = "Danh sách";
            //            // fontsize mặc định cho cả sheet
            //            ws.Cells.Style.Font.Size = 11;
            //            // font family mặc định cho cả sheet
            //            ws.Cells.Style.Font.Name = "Calibri";

            //            // Tạo danh sách các column header
            //            string[] arrColumnHeader = {
            //                                    "Mã NV",
            //                                    "Tên nhân viên",
            //                                    "Lương cơ bản",
            //                                    "Số sản phẩm bán",
            //                                    "Tổng tiền thưởng",
            //                                    "Tổng lương"
            //    };

            //            // lấy ra số lượng cột cần dùng dựa vào số lượng header
            //            var countColHeader = arrColumnHeader.Count();

            //            // merge các column lại từ column 1 đến số column header
            //            // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
            //            ws.Cells[1, 1].Value = "Thống kê danh sách tính lương nhân viên";
            //            ws.Cells[1, 1, 1, countColHeader].Merge = true;
            //            // in đậm
            //            ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
            //            // căn giữa
            //            ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //            int colIndex = 1;
            //            int rowIndex = 2;

            //            //tạo các header từ column header đã tạo từ bên trên
            //            foreach (var item in arrColumnHeader)
            //            {
            //                var cell = ws.Cells[rowIndex, colIndex];

            //                //set màu thành gray
            //                //var fill = cell.Style.Fill;
            //                //fill.PatternType = ExcelFillStyle.Solid;
            //                //fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

            //                //căn chỉnh các border
            //                //var border = cell.Style.Border;
            //                //border.Bottom.Style =
            //                //    border.Top.Style =
            //                //    border.Left.Style =
            //                //    border.Right.Style = ExcelBorderStyle.Thin;

            //                //gán giá trị
            //                cell.Value = item;

            //                colIndex++;
            //            }

            //            // lấy ra danh sách UserInfo từ ItemSource của DataGrid
            //            List<LuongNhanVienModel> userList = DSLuongNV.ToList();

            //            // với mỗi item trong danh sách sẽ ghi trên 1 dòng
            //            foreach (var item in userList)
            //            {
            //                // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
            //                colIndex = 1;

            //                // rowIndex tương ứng từng dòng dữ liệu
            //                rowIndex++;

            //                //gán giá trị cho từng cell                      
            //                ws.Cells[rowIndex, colIndex++].Value = item.MaNhanVien.ToString();

            //                ws.Cells[rowIndex, colIndex++].Value = item.TenNhanVien.ToString();

            //                ws.Cells[rowIndex, colIndex++].Value = item.LuongCoBan.ToString();

            //                ws.Cells[rowIndex, colIndex++].Value = item.TongSLBan.ToString();

            //                ws.Cells[rowIndex, colIndex++].Value = item.PhanTramHoaHong.ToString();

            //                ws.Cells[rowIndex, colIndex++].Value = item.TongLuong.ToString();

            //            }

            //            //Lưu file lại
            //            Byte[] bin = a.GetAsByteArray();
            //            File.WriteAllBytes(filePath, bin);
            //        }
            //        MessageBox.Show("Xuất excel thành công!");
            //    }
            //    catch (Exception EE)
            //    {
            //        MessageBox.Show("Có lỗi khi lưu file!");
            //    }
            //});
            //// Command khi load vào window
            //LoadedWindowCommand = new RelayCommand<Window>((p) =>
            //{
            //    return true;
            //}, (p) =>
            //{
            //    getListFromDB();
            //    DSLuongNV = null;
            //    DSNam = new ObservableCollection<string>();
            //    DSThang = new ObservableCollection<string>();

            //    foreach (var item in DSPhieuBanHangDB)
            //    {
            //        if (!DSNam.Contains(item.NgayLapPhieuBan.Value.Year.ToString().Trim()))
            //            DSNam.Add(item.NgayLapPhieuBan.Value.Year.ToString().Trim());
            //        if (!DSThang.Contains(item.NgayLapPhieuBan.Value.Month.ToString().Trim()))
            //            DSThang.Add(item.NgayLapPhieuBan.Value.Month.ToString().Trim());
            //    }
            //});

            //// lọc
            //Filter = new RelayCommand<Object>((p) =>
            //{
            //    if (Utils.ConvertUtils.convertString(ThangDaChon) == "" || Utils.ConvertUtils.convertString(NamDaChon) == "")
            //    {
            //        return false;
            //    }
            //    return true;
            //}, (p) =>
            //{
            //    DSPhieuBanHang = DSPhieuBanHangDB.Where(x => x.NgayLapPhieuBan.Value.Month.ToString().Equals(ThangDaChon) && x.NgayLapPhieuBan.Value.Year.ToString().Equals(NamDaChon));
            //    IEnumerable<IDSTAFF> listID = (from NV in DSNhanVien
            //                                   select new IDSTAFF()
            //                                   {
            //                                       MaNhanVien = NV.MaNhanVien,
            //                                       TongBanCuaNV = 0,
            //                                       TongSLBan = 0
            //                                   });
            //    foreach (var item in listID)
            //    {
            //        item.TongBanCuaNV = (double)DSPhieuBanHang.Where(x => x.MaNhanVien == item.MaNhanVien).Sum(x=>x.TongThanhTien);
            //        item.TongSLBan = (int)DSPhieuBanHang.Where(x => x.MaNhanVien == item.MaNhanVien).Sum(x => x.TongSoLuongBan);
            //    }
            //    try
            //    {
            //        DSLuongNV = (from NV in DSNhanVien
            //                           join temp in listID on NV.MaNhanVien equals temp.MaNhanVien
            //                           select new LuongNhanVienModel()
            //                           {
            //                               MaNhanVien = NV.MaNhanVien,
            //                               TenNhanVien = Utils.ConvertUtils.convertString(NV.TenNhanVien),
            //                               LuongCoBan = (double)NV.LuongCoBan,
            //                               TongSLBan = temp.TongSLBan,
            //                               TongLuong = Math.Round((double)(NV.LuongCoBan + (double)(temp.TongBanCuaNV * NV.PhanTramHoaHong / 100)))
            //                           });
            //    }
            //    catch (Exception)
            //    {

            //    }


            //});
            //HuyBoCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

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
