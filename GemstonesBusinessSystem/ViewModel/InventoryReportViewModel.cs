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
    class InventoryReportViewModel : BaseViewModel
    {
        #region Binding
        private int _TongSP;
        public int TongSP { get => _TongSP; set { _TongSP = value; OnPropertyChanged(); } }

        private int _TongSLMuaVao;
        public int TongSLMuaVao { get => _TongSLMuaVao; set { _TongSLMuaVao = value; OnPropertyChanged(); } }

        private int _TongSLBanRa;
        public int TongSLBanRa { get => _TongSLBanRa; set { _TongSLBanRa = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private int _ThangDaChon;
        public int ThangDaChon { get => _ThangDaChon; set { _ThangDaChon = value; OnPropertyChanged(); } }

        private int _NamDaChon;
        public int NamDaChon { get => _NamDaChon; set { _NamDaChon = value; OnPropertyChanged(); } }

        private bool _FlagToanThoiGian;
        public bool FlagToanThoiGian { get => _FlagToanThoiGian; set { _FlagToanThoiGian = value; OnPropertyChanged(); } }


        public static int status;
        #endregion

        #region  List Binding
        private ObservableCollection<BAOCAOTONKHO> _DSBaoCaoTonKhoDB;
        public ObservableCollection<BAOCAOTONKHO> DSBaoCaoTonKhoDB { get => _DSBaoCaoTonKhoDB; set { _DSBaoCaoTonKhoDB = value; OnPropertyChanged(); } }

        private IEnumerable<BAOCAOTONKHO> _DSBaoCaoTonKhoDBIE;
        public IEnumerable<BAOCAOTONKHO> DSBaoCaoTonKhoDBIE { get => _DSBaoCaoTonKhoDBIE; set { _DSBaoCaoTonKhoDBIE = value; OnPropertyChanged(); } }

        private ObservableCollection<BaoCaoTonKhoModel> _DSBaoCaoTonKho;
        public ObservableCollection<BaoCaoTonKhoModel> DSBaoCaoTonKho { get => _DSBaoCaoTonKho; set { _DSBaoCaoTonKho = value; OnPropertyChanged(); } }

        private ObservableCollection<int> _DSThang;
        public ObservableCollection<int> DSThang { get => _DSThang; set { _DSThang = value; OnPropertyChanged(); } }

        private ObservableCollection<int> _DSNam;
        public ObservableCollection<int> DSNam { get => _DSNam; set { _DSNam = value; OnPropertyChanged(); } }
        #endregion

        #region  Command
        public ICommand TimKiemCommand { get; set; }
        public ICommand ToanThoiGianCommand { get; set; }
        public ICommand TuyChinhThoiGianCommand { get; set; }
        public ICommand ChiTietHDCommand { get; set; }
        public ICommand XoaHDCommand { get; set; }
        public ICommand ThemPhieuMHCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand XuatExcelCommand { get; set; }
        #endregion

        public InventoryReportViewModel()
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
                        a.Workbook.Properties.Title = "Danh sách tồn kho";

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
                                                "Sản phẩm",
                                                "Thời gian",
                                                "Tồn đầu",
                                                "SL mua vào",
                                                "SL bán ra",
                                                "Tồn cuối",
                                                "Đơn vị tính"
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = "Thống kê danh sách tồn kho";
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
                        List<BaoCaoTonKhoModel> userList = DSBaoCaoTonKho.ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.SANPHAM.TenSanPham.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.ThoiGianTonKho.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TonDau.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.SLMuaVao.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.SLBanRa.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TonCuoi.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TenDVT.ToString();

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
                FlagToanThoiGian = true;
                ThangDaChon = DateTime.Now.Date.AddMonths(-1).Month;
                NamDaChon = DateTime.Now.Date.Year;

                LayDSTuDatabase();
                LoadDS();
                TinhTong();


                DSThang = new ObservableCollection<int>();
                DSNam = new ObservableCollection<int>();

                for (int i = 1; i < 13; i++)
                {
                    DSThang.Add(i);
                }
                foreach (var item in DSBaoCaoTonKhoDB)
                {
                    if (!DSNam.Contains(item.Nam.Value))
                        DSNam.Add(item.Nam.Value);
                }
                status = 0;
            });

            // Tìm kiếm 
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) == "" || DSBaoCaoTonKhoDB == null)
                {
                    LayDSTuDatabase();
                    LoadDS();
                    TinhTong();
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    DSBaoCaoTonKhoDBIE = DSBaoCaoTonKhoDB.Where(x => (x.Thang.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.Nam.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.SLMuaVao.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.SLBanRa.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.TonDau.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.TonCuoi.ToString().ToLower().Contains(TimKiem.ToLower())
                                                        || x.SANPHAM.TenSanPham.ToLower().Contains(TimKiem.ToLower())
                                                        || x.DVT.TenDVT.ToLower().Contains(TimKiem.ToLower())));
                    LoadDS();
                    TinhTong();
                }
                catch (Exception) { }
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
                    TinhTong();
                }
                catch (Exception)
                {
                }

            });


            // Tùy chỉnh thời gian nhập kho
            TuyChinhThoiGianCommand = new RelayCommand<Object>((p) =>
            {
                if (ThangDaChon.ToString() != null && NamDaChon.ToString() !=null && FlagToanThoiGian == false)
                {
                    if (ThangDaChon >= DateTime.Now.Month && NamDaChon >= DateTime.Now.Year)
                    {
                        MessageBox.Show("Chưa có báo cáo phù hợp!");
                        ThangDaChon = DateTime.Now.Date.AddMonths(-1).Month;
                        NamDaChon = DateTime.Now.Date.Year;
                    }
                    return true;
                }
                return false;
            }, (p) =>
            {
                try
                {
                    DSBaoCaoTonKhoDBIE = DSBaoCaoTonKhoDB.Where(x => x.Thang == ThangDaChon && x.Nam == NamDaChon);
                    LoadDS();
                    TinhTong();
                }
                catch (Exception)
                {
                }

            });

            #endregion
        }

        public void LayDSTuDatabase()
        {
            DSBaoCaoTonKhoDB = new ObservableCollection<BAOCAOTONKHO>(DataProvider.Ins.DB.BAOCAOTONKHOes.Where(x=>x.Thang < DateTime.Now.Month && x.Nam == DateTime.Now.Year || x.Nam < DateTime.Now.Year));
            DSBaoCaoTonKhoDBIE = DSBaoCaoTonKhoDB;
        }
        public void LoadDS()
        {
            DSBaoCaoTonKho = new ObservableCollection<BaoCaoTonKhoModel>();

            foreach (var item in DSBaoCaoTonKhoDBIE)
            {
                DSBaoCaoTonKho.Add(new BaoCaoTonKhoModel()
                {
                    MaBaoCaoTonKho = item.MaBaoCaoTonKho,
                    MaSanPham = item.MaSanPham.Value,
                    SANPHAM = item.SANPHAM,
                    SLBanRa = item.SLBanRa.Value,
                    SLMuaVao = item.SLMuaVao.Value,
                    TenDVT = item.DVT.TenDVT,
                    TonCuoi = item.TonCuoi.Value,
                    TonDau = item.TonDau.Value,
                    ThoiGianTonKho = new DateTime(item.Nam.Value, item.Thang.Value, 1)
                });
            }
        }

        public void TinhTong()
        {
            TongSP = DSBaoCaoTonKho.Count();
            TongSLMuaVao = DSBaoCaoTonKho.Sum(x => x.SLMuaVao);
            TongSLBanRa = DSBaoCaoTonKho.Sum(x => x.SLBanRa);
        }
    }
}
