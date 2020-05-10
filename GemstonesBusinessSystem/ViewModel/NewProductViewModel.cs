using GemstonesBusinessSystem.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GemstonesBusinessSystem.ViewModel
{

    class NewProductViewModel : BaseViewModel
    {
        #region Thuộc tính binding

        private SANPHAM _SanPhamMoi;
        public SANPHAM SanPhamMoi { get => _SanPhamMoi; set { _SanPhamMoi = value; OnPropertyChanged(); } }

        //private string _name;
        //public string name { get => _name; set { _name = value; OnPropertyChanged(); } }

        //private string _description;
        //public string description { get => _description; set { _description = value; OnPropertyChanged(); } }

        //private string _initialAmount;
        //public string initialAmount { get => _initialAmount; set { _initialAmount = value; OnPropertyChanged(); } }

        //private string _purchasePrice;
        //public string purchasePrice { get => _purchasePrice; set { _purchasePrice = value; OnPropertyChanged(); } }

        //private string _profitPercent;
        //public string profitPercent { get => _profitPercent; set { _profitPercent = value; OnPropertyChanged(); } }

        //private string _salePrice;
        //public string salePrice { get => _salePrice; set { _salePrice = value; OnPropertyChanged(); } }

        //private string _codeTypeProduct;
        //public string codeTypeProduct { get => _codeTypeProduct; set { _codeTypeProduct = value; OnPropertyChanged(); } }

        //private string _unit;
        //public string unit { get => _unit; set { _unit = value; OnPropertyChanged(); } }

        private string _image;
        public string image { get => _image; set { _image = value; OnPropertyChanged(); } }

        private double _DonGiaBan;
        public double DonGiaBan { get => _DonGiaBan; set { _DonGiaBan = value; OnPropertyChanged(); } }

        private ImageSource _imageSource;
        public ImageSource imageSource { get => _imageSource; set { _imageSource = value; OnPropertyChanged(); } }

        private LOAISANPHAM _LSPDaChon;
        public LOAISANPHAM LSPDaChon { get => _LSPDaChon; set { _LSPDaChon = value; OnPropertyChanged(); } }


        #endregion

        #region List binding

        private ObservableCollection<LOAISANPHAM> _DSLoaiSanPham;
        public ObservableCollection<LOAISANPHAM> DSLoaiSanPham { get => _DSLoaiSanPham; set { _DSLoaiSanPham = value; OnPropertyChanged(); } }

        #endregion

        #region Command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand GiaThayDoiCommand { get; set; }
        public ICommand SelectionChangeCommand { get; set; }
        public ICommand ThemLSPCommand { get; set; }
        public ICommand ThayDoiAnhCommand { get; set; }
        #endregion
        public NewProductViewModel()
        {

            #region Command
            // Command khi load vào window
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                SanPhamMoi = new SANPHAM();
                SanPhamMoi.TongSoLuongTon = 0;
                SanPhamMoi.DonGiaNhap = 0;
               

                LSPDaChon = null;
                LayDSTuDatabase();
                image = Utils.HandleImage.ImageToString("../../Images/Empty.png");
                imageSource = Utils.HandleImage.GetImage(image);
            });
            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertString.convertString(SanPhamMoi.TenSanPham) != ""
                    && Utils.ConvertString.convertString(SanPhamMoi.TongSoLuongTon.ToString()) != ""
                    && Utils.ConvertString.convertString(SanPhamMoi.DonGiaNhap.ToString()) != ""
                    && Utils.ConvertString.convertString(SanPhamMoi.DonGiaBan.ToString()) != ""
                    && Utils.ConvertString.convertString(LSPDaChon.MaLoaiSanPham.ToString()) != "")
                {
                    if (SanPhamMoi.DonGiaNhap <= 0)
                    {
                        MessageBox.Show("Vui lòng nhập đơn giá sản phẩm lớn hơn 0");
                        return false;
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                    return false;
                }
            }, (p) =>
            {

                try
                {
                    if (Utils.ConvertString.convertString(SanPhamMoi.TongSoLuongTon.ToString()) == "")
                    {
                        SanPhamMoi.TongSoLuongTon = 0;
                    }
                    //Product product = new Product()
                    //{
                    //    name = name,
                    //    descriptions = Utils.ConvertString.convertString(description),
                    //    initial_amount = int.Parse(initialAmount),
                    //    sale_price = double.Parse(salePrice),
                    //    purchase_price = double.Parse(purchasePrice),
                    //    images = image,
                    //    status = 1,
                    //    create_date = DateTime.Now.Date,
                    //    product_type_id = LSPDaChon.id
                    //};
                    SanPhamMoi.HinhAnhSanPham = image;
                    SanPhamMoi.TrangThaiSanPham = Constant.ACTIVE_STATUS;
                    DataProvider.Ins.DB.SANPHAMs.Add(SanPhamMoi);
                    DataProvider.Ins.DB.SaveChanges();
                    //product.code = Constant.codeProduct + product.id.ToString();
                    CT_PMH HD = new CT_PMH()
                    {
                        SoLuongMua = SanPhamMoi.TongSoLuongTon,
                        DonGiaSPHienTai = SanPhamMoi.DonGiaBan,
                        MaSanPham = SanPhamMoi.MaSanPham,
                        SoLuongSPHienTai = SanPhamMoi.TongSoLuongTon,
                        PhuongThuc = Constant.methodInitProduct,
                        ThanhTien = SanPhamMoi.DonGiaBan * SanPhamMoi.TongSoLuongTon

                        //quantity = product.initial_amount,
                        //price = product.purchase_price,
                        //product_id = product.id,
                        //inventory_current = product.initial_amount,
                        //method = Constant.methodInitProduct,
                        //total_price = product.initial_amount * product.purchase_price,
                        //purchare_price_current = product.purchase_price
                    };
                    PHIEUMUAHANG PMH = new PHIEUMUAHANG()
                    {
                        NgayLapPhieuMua = DateTime.Now,
                        TongSoLuongMua = HD.SoLuongMua,
                        TongThanhTien = HD.ThanhTien,
                    };
                    HD.MaPhieuMuaHang = PMH.MaPhieuMuaHang;
                    DataProvider.Ins.DB.CT_PMH.Add(HD);
                    DataProvider.Ins.DB.PHIEUMUAHANGs.Add(PMH);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    //resetData();
                    p.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Thêm thất bại, vui lòng kiểm tra lại");
                }
            });

            // Hủy bỏ
            HuyBoCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });



            // Giá gốc thay đổi -> Giá bán thay đổi
            GiaThayDoiCommand = new RelayCommand<Window>((p) =>
            {
                try
                {
                    if (Utils.ConvertString.convertString(SanPhamMoi.DonGiaNhap.ToString()) == "")
                    {
                        //salePrice = "";
                        DonGiaBan = 0;
                        SanPhamMoi.DonGiaBan = DonGiaBan;
                        return false;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }, (p) =>
            {
                try
                {
                    DonGiaBan = Math.Round(((double)SanPhamMoi.DonGiaNhap + ((double)SanPhamMoi.DonGiaNhap * (double)SanPhamMoi.LOAISANPHAM.PhanTramLoiNhuan / 100)));
                    SanPhamMoi.DonGiaBan = DonGiaBan;
                    //salePrice = Math.Round((double.Parse(purchasePrice) + (double.Parse(purchasePrice) * double.Parse(profitPercent) / 100))).ToString();
                }
                catch (Exception) { }
            });

            // Chọn loại sản phẩm
            SelectionChangeCommand = new RelayCommand<Window>((p) =>
            {
                if (LSPDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    SanPhamMoi.MaLoaiSanPham = LSPDaChon.MaLoaiSanPham;
                    SanPhamMoi.LOAISANPHAM = LSPDaChon;
                    //codeTypeProduct = LSPDaChon.code;
                    //unit = LSPDaChon.unit;
                    //profitPercent = LSPDaChon.profit_percent.ToString();
                    //salePrice = (double.Parse(purchasePrice) + (double.Parse(purchasePrice) * double.Parse(profitPercent) / 100)).ToString();
                    DonGiaBan = Math.Round(((double)SanPhamMoi.DonGiaNhap + ((double)SanPhamMoi.DonGiaNhap * (double)SanPhamMoi.LOAISANPHAM.PhanTramLoiNhuan / 100)));
                    SanPhamMoi.DonGiaBan = DonGiaBan;
                }
                catch (Exception) { }
            });

            // Thêm loại sản phẩm
            ThemLSPCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                NewTypeProductWindow newTypeProductWindow = new NewTypeProductWindow();
                newTypeProductWindow.ShowDialog();
                LayDSTuDatabase();
            });

            // Thêm ảnh
            ThayDoiAnhCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = @"D:\",
                    Title = "Chọn ảnh",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "txt",
                    Filter = "Images (*.JPG;*.PNG)|*.JPG;*.PNG",
                    RestoreDirectory = true,
                    ReadOnlyChecked = true,
                    ShowReadOnly = true
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    image = Utils.HandleImage.ImageToString(openFileDialog.FileName);
                    imageSource = Utils.HandleImage.GetImage(image);
                }
            });
            #endregion
        }

        public void LayDSTuDatabase()
        {
            DSLoaiSanPham = new ObservableCollection<LOAISANPHAM>(DataProvider.Ins.DB.LOAISANPHAMs.Where(x => x.TrangThaiLoaiSanPham == Constant.ACTIVE_STATUS));
        }


        //public void resetData()
        //{
        //    //name = "";
        //    //description = "";
        //    //initialAmount = "0";
        //    //purchasePrice = "";
        //    //profitPercent = "";
        //    //salePrice = "";
        //    //codeTypeProduct = "";
        //    //unit = "";
        //    //image = "";
        //    //profitPercent = "";
        //    LSPDaChon = null;
        //}

    }
}
