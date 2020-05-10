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
        public RelayCommand<IList> SelectionChangedCommand { get; set; }

        #endregion
        public ListProductsViewModel()
        {
            //title = strActive;
            //getListFromDB();
            //loadListProductActive();
            #region Command

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
                }
                catch (Exception)
                {
                }

            });

            // Lọc dữ liệu từ search
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertString.convertString(TimKiem) != "" && DSSanPham != null)
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
                    //listProduct = listProduct.Where(x => (x.code.ToString().Contains(TimKiem)
                    //                                    || x.name.ToLower().Contains(TimKiem)
                    //                                    || x.nameType.ToLower().Contains(TimKiem)
                    //                                    || x.quantity.ToString().ToLower().Contains(TimKiem)
                    //                                    || x.createdBy.ToLower().Contains(TimKiem)));
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
                    MessageBoxResult issExit = System.Windows.MessageBox.Show("Dữ liệu đã xóa không thể khôi phục, bạn có chắc chắn xóa?", "Xác nhận", MessageBoxButton.OKCancel);
                    if (issExit == MessageBoxResult.OK)
                    {
                        try
                        {
                            foreach (var SPDaChon in DSSPDaChon)
                            {

                                //var listBillProductSupplier = DataProvider.Ins.DB.Product_Supplier.Where(x => x.product_id == item.id);
                                var DSHDMuaHang = DataProvider.Ins.DB.CT_PMH.Where(x => x.MaSanPham == SPDaChon.MaSanPham);

                                foreach (var item in DSHDMuaHang)
                                {
                                    if (Utils.ConvertString.convertString(item.PHIEUMUAHANG.MaNhaCungCap.ToString()) == "")
                                    {
                                        var del = DataProvider.Ins.DB.PHIEUMUAHANGs.Where(x => x.MaPhieuMuaHang == item.MaPhieuMuaHang).SingleOrDefault();
                                        DataProvider.Ins.DB.PHIEUMUAHANGs.Remove(del);
                                    }
                                }

                                foreach (var i in DSHDMuaHang)
                                {
                                    DataProvider.Ins.DB.CT_PMH.Remove(i);
                                }
                                // var listBillProductCustomer = DataProvider.Ins.DB.Product_Customer.Where(x => x.product_id == item.id);
                                var DSHDBanHang = DataProvider.Ins.DB.CT_PBH.Where(x => x.MaSanPham == SPDaChon.MaSanPham);
                                foreach (var i in DSHDBanHang)
                                {
                                    DataProvider.Ins.DB.CT_PBH.Remove(i);
                                }
                                

                                DataProvider.Ins.DB.SANPHAMs.Remove(DSSanPham.Where(x => x.MaSanPham == SPDaChon.MaSanPham).SingleOrDefault());
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            MessageBox.Show("Xóa thành công");
                            LayDSTuDatabase();
                            LoadDSSPNgungHoatDong();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Xóa thất bại vui lòng thử lại");
                        }
                    }

                }
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
            //listProduct = (from product in DSSanPham.Where(x => x.status == Constant.ACTIVE_STATUS)
            //               join type in DSLoaiSanPham on product.product_type_id equals type.id
            //               select new ProductTypeModel()
            //               {
            //                   id = product.id,
            //                   idTypeProduct = (int)product.product_type_id,
            //                   code = product.code,
            //                   name = Utils.ConvertString.convertString(product.name),
            //                   nameType = Utils.ConvertString.convertString(type.name),
            //                   image = product.images,
            //                   imageSource = Utils.HandleImage.GetImage(product.images),
            //                   quantity = (int)(product.initial_amount),
            //                   purchasePrice = Utils.ConvertString.convertString(product.purchase_price.ToString()),
            //                   profitPercent = Utils.ConvertString.convertString(type.profit_percent.ToString()),
            //                   salePrice = Utils.ConvertString.convertString(product.sale_price.ToString()),
            //                   description = Utils.ConvertString.convertString(product.descriptions.ToString()),
            //                   createdBy = Utils.ConvertString.convertString(product.create_by),
            //                   createdDate = (DateTime)product.create_date
            //               });

            TongSLSanPham = DSSanPham.Count().ToString();
        }

        public void LoadDSSPNgungHoatDong()
        {
            TieuDe = DSNgungHoatDong;
            DSSanPham = DSSanPhamDB.Where(x => x.TrangThaiSanPham == Constant.INACTIVE_STATUS);
            //listProduct = (from product in DSSanPham.Where(x => x.status == Constant.INACTIVE_STATUS)
            //               join type in DSLoaiSanPham on product.product_type_id equals type.id
            //               select new ProductTypeModel()
            //               {
            //                   id = product.id,
            //                   idTypeProduct = (int)product.product_type_id,
            //                   code = product.code,
            //                   name = Utils.ConvertString.convertString(product.name),
            //                   nameType = Utils.ConvertString.convertString(type.name),
            //                   image = product.images,
            //                   imageSource = Utils.HandleImage.GetImage(product.images),
            //                   quantity = (int)product.initial_amount,
            //                   purchasePrice = Utils.ConvertString.convertString(product.purchase_price.ToString()),
            //                   profitPercent = Utils.ConvertString.convertString(type.profit_percent.ToString()),
            //                   salePrice = Utils.ConvertString.convertString(product.sale_price.ToString()),
            //                   description = Utils.ConvertString.convertString(product.descriptions.ToString()),
            //                   createdBy = Utils.ConvertString.convertString(product.create_by),
            //                   createdDate = (DateTime)product.create_date
            //               });

            TongSLSanPham = DSSanPham.Count().ToString();

        }

    }
}
