using GemstonesBusinessSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemstonesBusinessSystem.Model
{
    class HoaDonSanPhamModel : BaseViewModel
    {

        // sử dụng để list danh sách đã giao dịch của sản phẩm (detailProductWindow)

        private double _DonGiaSPHienTai;
        public double DonGiaSPHienTai { get => _DonGiaSPHienTai; set { _DonGiaSPHienTai = value; OnPropertyChanged(); } }

        private int _SoLuongSPHienTai;
        public int SoLuongSPHienTai { get => _SoLuongSPHienTai; set { _SoLuongSPHienTai = value; OnPropertyChanged(); } }

        private int _SoLuongGiaoDich;
        public int SoLuongGiaoDich { get => _SoLuongGiaoDich; set { _SoLuongGiaoDich = value; OnPropertyChanged(); } }

        private string _PhuongThucGD;
        public string PhuongThucGD { get => _PhuongThucGD; set { _PhuongThucGD = value; OnPropertyChanged(); } }

        private DateTime _ThoiGianGD;
        public DateTime ThoiGianGD { get => _ThoiGianGD; set { _ThoiGianGD = value; OnPropertyChanged(); } }

        public HoaDonSanPhamModel() { }
    }
}
