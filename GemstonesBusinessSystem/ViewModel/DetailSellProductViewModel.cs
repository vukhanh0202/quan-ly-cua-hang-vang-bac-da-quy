using GemstonesBusinessSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class DetailSellProductViewModel : BaseViewModel
    {
        #region list binding

        //private ObservableCollection<Product> _products;
        //public ObservableCollection<Product> products { get => _products; set { _products = value; OnPropertyChanged(); } }

        //private ObservableCollection<ProductType> _productTypes;
        //public ObservableCollection<ProductType> productTypes { get => _productTypes; set { _productTypes = value; OnPropertyChanged(); } }

        //private ObservableCollection<Product_Customer> _billSellProduct;
        //public ObservableCollection<Product_Customer> billSellProduct { get => _billSellProduct; set { _billSellProduct = value; OnPropertyChanged(); } }

        private IEnumerable<CT_PBH> _DSChiTietPBH;
        public IEnumerable<CT_PBH> DSChiTietPBH { get => _DSChiTietPBH; set { _DSChiTietPBH = value; OnPropertyChanged(); } }
        #endregion

        #region thuộc tính binding
        private PHIEUBANHANG _PhieuBanHang;
        public PHIEUBANHANG PhieuBanHang { get => _PhieuBanHang; set { _PhieuBanHang = value; OnPropertyChanged(); } }
        #endregion

        #region command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        #endregion
        public DetailSellProductViewModel()
        {

            // load
            //LoadedWindowCommand = new RelayCommand<Window>((p) =>
            //{
            //    return true;
            //}, (p) =>
            //{
            //    getListFromDB();
            //});

            // Hủy bỏ
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
        }

        public void LoadChiTietPhieuBanHang(PHIEUBANHANG ChiTietPhieuBanHang)
        {
            PhieuBanHang = ChiTietPhieuBanHang;
            DSChiTietPBH = DataProvider.Ins.DB.CT_PBH.Where(x => x.MaPhieuBanHang == PhieuBanHang.MaPhieuBanHang);
        }

        //public void loadListDetailBillById(int id)
        //{
            
        //    billSellProduct = new ObservableCollection<Product_Customer>(DataProvider.Ins.DB.Product_Customer.Where(x => x.bill_sell_product_id == id));
        //    BillSellProduct billCustomer = DataProvider.Ins.DB.BillSellProducts.Where(x => x.id == id).SingleOrDefault();
        //    getListFromDB();
        //    DSChiTietPBH = (from bill in billSellProduct
        //                      join product in products on bill.product_id equals product.id
        //                      join type in productTypes on product.product_type_id equals type.id
        //                      select new BillDetailProductModel()
        //                      {
        //                          nameProduct = Utils.ConvertString.convertString(product.name),
        //                          nameProductType = Utils.ConvertString.convertString(type.name),
        //                          quantity = (int)bill.quantity,
        //                          unit = Utils.ConvertString.convertString(type.unit),
        //                          price = (double)bill.price,
        //                          totalPrice = Utils.ConvertString.convertString(bill.total_price.ToString())
        //                      });
        //    if (PhieuBanHang == null)
        //    {
        //        PhieuBanHang = new BillSellProductModel();
        //        PhieuBanHang.id = billCustomer.id;
        //        PhieuBanHang.customerName = billCustomer.Customer.name;
        //        PhieuBanHang.createdDate = (DateTime)billCustomer.create_date;
        //        PhieuBanHang.createdBy = billCustomer.Staff.name;
        //        PhieuBanHang.totalPrice = (double)billCustomer.totalPrice;
        //        PhieuBanHang.totalQuantity = (int)billCustomer.totalQuantity;
        //    }
        //}
        //public void getListFromDB()
        //{
        //    products = new ObservableCollection<Product>(DataProvider.Ins.DB.Products);
        //    productTypes = new ObservableCollection<ProductType>(DataProvider.Ins.DB.ProductTypes);
        //}

       
    }
}
