using GemstonesBusinessSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemstonesBusinessSystem.Model
{
    class BaoCaoTonKhoModel : BaseViewModel
    {
        private int _MaBaoCaoTonKho;
        public int MaBaoCaoTonKho { get => _MaBaoCaoTonKho; set { _MaBaoCaoTonKho = value; OnPropertyChanged(); } }

        private DateTime _ThoiGianTonKho;
        public DateTime ThoiGianTonKho { get => _ThoiGianTonKho; set { _ThoiGianTonKho = value; OnPropertyChanged(); } }

        private int _MaSanPham;
        public int MaSanPham { get => _MaSanPham; set { _MaSanPham = value; OnPropertyChanged(); } }

        private SANPHAM _SANPHAM;
        public SANPHAM SANPHAM { get => _SANPHAM; set { _SANPHAM = value; OnPropertyChanged(); } }

        private int _TonDau;
        public int TonDau { get => _TonDau; set { _TonDau = value; OnPropertyChanged(); } }

        private int _SLMuaVao;
        public int SLMuaVao { get => _SLMuaVao; set { _SLMuaVao = value; OnPropertyChanged(); } }

        private int _SLBanRa;
        public int SLBanRa { get => _SLBanRa; set { _SLBanRa = value; OnPropertyChanged(); } }

        private int _TonCuoi;
        public int TonCuoi { get => _TonCuoi; set { _TonCuoi = value; OnPropertyChanged(); } }

        private string _TenDVT;
        public string TenDVT { get => _TenDVT; set { _TenDVT = value; OnPropertyChanged(); } }

        public BaoCaoTonKhoModel() { }
    }
}
