using GemstonesBusinessSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemstonesBusinessSystem.Model
{
    class LuongNhanVienModel : BaseViewModel
    {
        private int _MaNhanVien;
        public int MaNhanVien { get => _MaNhanVien; set { _MaNhanVien = value; OnPropertyChanged(); } }

        private string _TenNhanVien;
        public string TenNhanVien { get => _TenNhanVien; set { _TenNhanVien = value; OnPropertyChanged(); } }

        private double _LuongCoBan;
        public double LuongCoBan { get => _LuongCoBan; set { _LuongCoBan = value; OnPropertyChanged(); } }

        private double _PhanTramHoaHong;
        public double PhanTramHoaHong { get => _PhanTramHoaHong; set { _PhanTramHoaHong = value; OnPropertyChanged(); } }

        private double _TongLuong;
        public double TongLuong { get => _TongLuong; set { _TongLuong = value; OnPropertyChanged(); } }

        //private string _date;
        //public string date { get => _date; set { _date = value; OnPropertyChanged(); } }

        private int _TongSLBan;
        public int TongSLBan { get => _TongSLBan; set { _TongSLBan = value; OnPropertyChanged(); } }

        public LuongNhanVienModel() { }
    }
}
