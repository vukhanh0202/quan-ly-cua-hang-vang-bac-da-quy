using GemstonesBusinessSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GemstonesBusinessSystem.Model
{
    class SanPhamModel : BaseViewModel
    {
        private int _MaSanPham;
        public int MaSanPham { get => _MaSanPham; set { _MaSanPham = value; OnPropertyChanged(); } }

        private string _TenSanPham;
        public string TenSanPham { get => _TenSanPham; set { _TenSanPham = value; OnPropertyChanged(); } }

        private int _TongSoLuongTon;
        public int TongSoLuongTon { get => _TongSoLuongTon; set { _TongSoLuongTon = value; OnPropertyChanged(); } }

        private double _DonGiaNhap;
        public double DonGiaNhap { get => _DonGiaNhap; set { _DonGiaNhap = value; OnPropertyChanged(); } }

        private double _PhanTramLoiNhuan;
        public double PhanTramLoiNhuan { get => _PhanTramLoiNhuan; set { _PhanTramLoiNhuan = value; OnPropertyChanged(); } }

        private double _DonGiaBan;
        public double DonGiaBan { get => _DonGiaBan; set { _DonGiaBan = value; OnPropertyChanged(); } }

        private ImageSource _HinhAnhSP;
        public ImageSource HinhAnhSP { get => _HinhAnhSP; set { _HinhAnhSP = value; OnPropertyChanged(); } }

        private string _TenLoaiSanPham;
        public string TenLoaiSanPham { get => _TenLoaiSanPham; set { _TenLoaiSanPham = value; OnPropertyChanged(); } }

        private LOAISANPHAM _LOAISANPHAM;
        public LOAISANPHAM LOAISANPHAM { get => _LOAISANPHAM; set { _LOAISANPHAM = value; OnPropertyChanged(); } }

        private string _HinhAnhSanPham;
        public string HinhAnhSanPham { get => _HinhAnhSanPham; set { _HinhAnhSanPham = value; OnPropertyChanged(); } }

        public SanPhamModel() { }

    }
}
