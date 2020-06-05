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
    class ListProductsViewModel : BaseViewModel
    {
        #region Chuỗi hằng
        public const string DSHoatDong = "Danh sách sản phẩm đang hoạt động";
        public const string DSNgungHoatDong = "Danh sách sản phẩm ngừng hoạt động";

        #endregion

        #region List binding
        //private IEnumerable<ProductTypeModel> _listProduct;
        //public IEnumerable<ProductTypeModel> listProduct { get => _listProduct; set { _listProduct = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _DSSanPhamDB;
        public ObservableCollection<SANPHAM> DSSanPhamDB { get => _DSSanPhamDB; set { _DSSanPhamDB = value; OnPropertyChanged(); } }

        private IEnumerable<SANPHAM> _DSSanPham;
        public IEnumerable<SANPHAM> DSSanPham { get => _DSSanPham; set { _DSSanPham = value; OnPropertyChanged(); } }

        //private ObservableCollection<LOAISANPHAM> _DSLoaiSanPham;
        //public ObservableCollection<LOAISANPHAM> DSLoaiSanPham { get => _DSLoaiSanPham; set { _DSLoaiSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _DSSPDaChon;
        public ObservableCollection<SANPHAM> DSSPDaChon { get => _DSSPDaChon; set { _DSSPDaChon = value; OnPropertyChanged(); } }

        #endregion

        #region Thuộc tính Binding
        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        private string _TongSLSanPham;
        public string TongSLSanPham { get => _TongSLSanPham; set { _TongSLSanPham = value; OnPropertyChanged(); } }

        private string _TieuDe;
        public string TieuDe { get => _TieuDe; set { _TieuDe = value; OnPropertyChanged(); } }

        private SANPHAM _CTSPDaChon;
        public SANPHAM CTSPDaChon { get => _CTSPDaChon; set { _CTSPDaChon = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        #region Command
        public ICommand TimKiemCommand { get; set; }
        public ICommand XoaSanPhamCommand { get; set; }
        public ICommand ThemSanPhamCommand { get; set; }
        public ICommand LoaiSanPhamCommand { get; set; }
        public ICommand ChiTietSPCommand { get; set; }
        public ICommand DSHoatDongCommand { get; set; }
        public ICommand DSNgungHoatDongCommand { get; set; }
        public ICommand DVTCommand { get; set; }
        public ICommand KhoiPhucSanPhamCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand XuatExcelCommand { get; set; }
        public RelayCommand<IList> SelectionChangedCommand { get; set; }

        #endregion
        public ListProductsViewModel()
        {
            //title = strActive;
            //getListFromDB();
            //loadListProductActive();
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
                        a.Workbook.Properties.Title = "Danh sách sản phẩm";

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
                                                "Đơn giá nhập",
                                                "Đơn giá bán",
                                                "Tổng số lượng tồn",
                                                "Trạng thái sản phẩm"
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
                        List<SANPHAM> userList = DSSanPham.ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.MaSanPham.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TenSanPham.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.DonGiaNhap.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.DonGiaBan.ToString();

                            ws.Cells[rowIndex, colIndex++].Value = item.TongSoLuongTon.ToString();

                            if (item.TrangThaiSanPham == 1)
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
                LayDSTuDatabase();
                LoadDSSPHoatDong();
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
                    DSSPDaChon.Clear();
                    foreach (var item in items)
                        DSSPDaChon.Add((SANPHAM)item);
                }
                catch (Exception)
                {
                }
            });


            // Load danh sách đang hoạt động
            DSHoatDongCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                LoadDSSPHoatDong();
            });

            // Load danh sách ngừng hoạt động
            DSNgungHoatDongCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                LoadDSSPNgungHoatDong();
            });

            // Khôi phục sản phẩm
            KhoiPhucSanPhamCommand = new RelayCommand<IList>((items) =>
            {
                if (TieuDe == DSHoatDong)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm ngưng hoạt động");
                    return false;
                }
                else if (TieuDe == DSNgungHoatDong)
                {
                    if (DSSPDaChon.Count > 0)
                    {
                        return true;
                    }
                }
                return false;
            }, (items) =>
            {
                try
                {
                    foreach (var SPDaChon in DSSPDaChon)
                    {
                        // Kiểm tra xem loại sản phẩm có còn hoạt động hay không => nếu không hoạt động không được khôi phục sản phẩm
                        var TrangThaiLSP = DataProvider.Ins.DB.LOAISANPHAMs.Where(x => x.MaLoaiSanPham == SPDaChon.MaLoaiSanPham).SingleOrDefault().TrangThaiLoaiSanPham;

                        if (TrangThaiLSP == 0)
                        {
                            MessageBox.Show("Một số sản phẩm không thể khôi phục, do không có loại sản phẩm phù hợp, vui lòng kiểm tra lại!");
                        }
                        else
                        {
                            DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaSanPham == SPDaChon.MaSanPham).SingleOrDefault().TrangThaiSanPham = Constant.ACTIVE_STATUS;
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        LayDSTuDatabase();
                        LoadDSSPNgungHoatDong();
                    }
                    CTSPDaChon = null;
                }
                catch (Exception)
                {
                }

            });

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TimKiem) != "" && DSSanPham != null)
                {
                    return true;
                }
                else
                {
                    if (TieuDe == DSHoatDong)
                    {
                        LoadDSSPHoatDong();
                    }
                    else if (TieuDe == DSNgungHoatDong)
                    {
                        LoadDSSPNgungHoatDong();
                    }
                    return false;
                }
            }, (p) =>
            {
                try
                {
                    DSSanPham = DSSanPham.Where(x => x.MaSanPham.ToString().Contains(TimKiem.ToLower())
                                                        || x.TenSanPham.ToLower().Contains(TimKiem.ToLower())
                                                        || x.LOAISANPHAM.TenLoaiSanPham.ToLower().Contains(TimKiem.ToLower())
                                                        || x.TongSoLuongTon.ToString().ToLower().Contains(TimKiem.ToLower()));

                    TongSLSanPham = DSSanPham.Count().ToString();
                }
                catch (Exception e) { }
            });

            // Xóa product
            XoaSanPhamCommand = new RelayCommand<Object>((p) =>
            {
                if (DSSPDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                if (TieuDe == DSHoatDong)
                {
                    MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn có chắc chắn ngưng hoạt động sản phẩm?", "Xác nhận", MessageBoxButton.OKCancel);
                    if (isExit == MessageBoxResult.OK)
                    {
                        try
                        {
                            foreach (var SPDaChon in DSSPDaChon)
                            {

                                DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaSanPham == SPDaChon.MaSanPham).SingleOrDefault().TrangThaiSanPham = Constant.INACTIVE_STATUS;
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            MessageBox.Show("Ngưng hoạt động thành công");
                            LayDSTuDatabase();
                            LoadDSSPHoatDong();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Ngưng hoạt động thất bại vui lòng thử lại");
                        }
                    }
                }
                else if (TieuDe == DSNgungHoatDong)
                {
                    MessageBoxResult issExit = System.Windows.MessageBox.Show("Bạn có chắc chắn thực hiện hành động này?", "Xác nhận", MessageBoxButton.OKCancel);
                    if (issExit == MessageBoxResult.OK)
                    {
                        try
                        {
                            foreach (var SPDaChon in DSSPDaChon)
                            {
                                var HDMuaHang = DataProvider.Ins.DB.CT_PMH.Where(x => x.MaSanPham == SPDaChon.MaSanPham && x.PHIEUMUAHANG.NHACUNGCAP != null).FirstOrDefault();
                                var HDBanHang = DataProvider.Ins.DB.CT_PBH.Where(x => x.MaSanPham == SPDaChon.MaSanPham).FirstOrDefault();
                                var BaoCaoTonKho = DataProvider.Ins.DB.BAOCAOTONKHOes.Where(x => x.Thang < DateTime.Now.Month && x.Nam < DateTime.Now.Year).FirstOrDefault();
                                if (HDMuaHang != null || HDBanHang != null || BaoCaoTonKho !=null)
                                {
                                    MessageBox.Show("Sản phẩm đang được sử dụng, vui lòng xóa các dữ liệu liên quan!");
                                }
                                else
                                {
                                    DataProvider.Ins.DB.SANPHAMs.Remove(SPDaChon);
                                    DataProvider.Ins.DB.SaveChanges();
                                    MessageBox.Show("Xóa thành công");
                                }

                            }
                            LayDSTuDatabase();
                            LoadDSSPNgungHoatDong();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Xóa thất bại vui lòng thử lại");
                        }
                    }
                }
                CTSPDaChon = null;
            });
            // Đơn vị tính
            DVTCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                UnitWindow unitWindow = new UnitWindow();
                unitWindow.ShowDialog();
                LayDSTuDatabase();
                LoadDSSPHoatDong();
            });

            // Thêm product
            ThemSanPhamCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
             {
                 NewProduct newProduct = new NewProduct();
                 p.Hide();
                 newProduct.ShowDialog();
                 p.Show();
                 LayDSTuDatabase();
                 LoadDSSPHoatDong();
             });

            // Thêm loại sản phẩm
            LoaiSanPhamCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                NewTypeProductWindow newTypeProductWindow = new NewTypeProductWindow();
                newTypeProductWindow.ShowDialog();
                LayDSTuDatabase();
                LoadDSSPHoatDong();
            });

            // Chi tiết sản phẩm
            ChiTietSPCommand = new RelayCommand<object>((p) =>
            {
                if (CTSPDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {

                DetailProduct detailProduct = new DetailProduct();
                (detailProduct.DataContext as DetailProductViewModel).LoadSanPham(CTSPDaChon);
                detailProduct.ShowDialog();
                CTSPDaChon = null;

                LayDSTuDatabase();
                if (TieuDe == DSHoatDong)
                {
                    LoadDSSPHoatDong();
                }
                else if (TieuDe == DSNgungHoatDong)
                {
                    LoadDSSPNgungHoatDong();
                }
            });
            #endregion
        }

        public void LayDSTuDatabase()
        {
            DSSPDaChon = new ObservableCollection<SANPHAM>();
            DSSanPhamDB = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);


            //DSLoaiSanPham = new ObservableCollection<LOAISANPHAM>(DataProvider.Ins.DB.LOAISANPHAMs.Where(x => x.TrangThaiLoaiSanPham == Constant.ACTIVE_STATUS));
        }
        public void LoadDSSPHoatDong()
        {
            TieuDe = DSHoatDong;
            DSSanPham = DSSanPhamDB.Where(x => x.TrangThaiSanPham == Constant.ACTIVE_STATUS);

            TongSLSanPham = DSSanPham.Count().ToString();
        }

        public void LoadDSSPNgungHoatDong()
        {
            TieuDe = DSNgungHoatDong;
            DSSanPham = DSSanPhamDB.Where(x => x.TrangThaiSanPham == Constant.INACTIVE_STATUS);

            TongSLSanPham = DSSanPham.Count().ToString();

        }

    }
}
