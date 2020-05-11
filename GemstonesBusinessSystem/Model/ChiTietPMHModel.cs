using GemstonesBusinessSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemstonesBusinessSystem.Model
{
    class ChiTietPMHModel : BaseViewModel
    {
        private int _MaSanPham;
        public int MaSanPham { get => _MaSanPham; set { _MaSanPham = value; OnPropertyChanged(); } }

        private SANPHAM _SANPHAM;
        public SANPHAM SANPHAM { get => _SANPHAM; set { _SANPHAM = value; OnPropertyChanged(); } }

        private int _SoLuongBan;
        public int SoLuongBan { get => _SoLuongBan; set { _SoLuongBan = value; OnPropertyChanged(); } }

        private string _ThanhTien;
        public string ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }

        private int _SoLuongSPHienTai;
        public int SoLuongSPHienTai { get => _SoLuongSPHienTai; set { _SoLuongSPHienTai = value; OnPropertyChanged(); } }

        private string _PhuongThuc;
        public string PhuongThuc { get => _PhuongThuc; set { _PhuongThuc = value; OnPropertyChanged(); } }

        private double _DonGiaSPHienTai;
        public double DonGiaSPHienTai { get => _DonGiaSPHienTai; set { _DonGiaSPHienTai = value; OnPropertyChanged(); } }

        public ChiTietPMHModel() { }
    }
}
