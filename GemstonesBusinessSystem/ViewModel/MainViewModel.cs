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
    class MainViewModel : BaseViewModel
    {

        #region Thuộc tính ẩn hiện grid
        public enum EnumChucNang
        {
            TrangChu, DanhSachSP, DonHang, DanhSachDV, DonDichVu, DoiTac, NhapHang, TonKho, Nhanvien, DiemGiaoDich
        };
        private int _ChucNang;
        public int ChucNang { get => _ChucNang; set { _ChucNang = value; OnPropertyChanged(); } }

        #endregion

        #region Command ẩn hiện Grid
        public ICommand TrangChuCommand { get; set; }
        public ICommand DanhSachSPCommand { get; set; }
        public ICommand DonHangCommand { get; set; }
        public ICommand DanhSachDVCommand { get; set; }
        public ICommand DonDichVuCommand { get; set; }
        public ICommand DoiTacCommand { get; set; }
        public ICommand NhapHangCommand { get; set; }
        public ICommand TonKhoCommand { get; set; }
        public ICommand NhanvienCommand { get; set; }
        public ICommand DiemGiaoDichCommand { get; set; }


        #endregion

        public MainViewModel()
        {
            //supplierList = new ObservableCollection<Supplier>(DataProvider.Ins.DB.Suppliers);
            //list = new ObservableCollection<Supplier>(DataProvider.Ins.DB.Suppliers);


            #region Các giá trị khởi tạo
            #endregion
            #region Xử lí ẩn hiện Grid
            TrangChuCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.TrangChu;
                DashBoard.status = 1;
            });

            DanhSachSPCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DanhSachSP;
                ListProductsViewModel.status = 1;
            });

            DonHangCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DonHang;
                ListSellProductsViewModel.status = 1;
            });

            DanhSachDVCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DanhSachDV;
                ListServiceViewModel.status = 1;
            });
            DonDichVuCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DonDichVu;
                ListBillServiceViewModel.status = 1;
            });

            DoiTacCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DoiTac;
                CustomerViewModel.status = 1;
            });
            NhapHangCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.NhapHang;
                ListBuyProductViewModel.status = 1;

            });
            TonKhoCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.TonKho;

            });

            NhanvienCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.Nhanvien;
                StaffViewModel.status = 1;
            });

            DiemGiaoDichCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChucNang = (int)EnumChucNang.DiemGiaoDich;
                DealViewModel.status = 1;

            });

            #endregion


        }
    }


}
