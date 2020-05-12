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

        private ObservableCollection<CT_PBH> _DSChiTietPBH;
        public ObservableCollection<CT_PBH> DSChiTietPBH { get => _DSChiTietPBH; set { _DSChiTietPBH = value; OnPropertyChanged(); } }
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
            DSChiTietPBH = new ObservableCollection<CT_PBH>(DataProvider.Ins.DB.CT_PBH.Where(x => x.MaPhieuBanHang == ChiTietPhieuBanHang.MaPhieuBanHang));
        }

       
    }
}
