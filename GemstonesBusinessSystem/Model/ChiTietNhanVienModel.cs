using GemstonesBusinessSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemstonesBusinessSystem.Model
{
    class ChiTietNhanVienModel : BaseViewModel
    {

        private int _MaNhanVien;
        public int MaNhanVien { get => _MaNhanVien; set { _MaNhanVien = value; OnPropertyChanged(); } }

        private string _TenNhanVien;
        public string TenNhanVien { get => _TenNhanVien; set { _TenNhanVien = value; OnPropertyChanged(); } }

        private string _DiaChiNV;
        public string DiaChiNV { get => _DiaChiNV; set { _DiaChiNV = value; OnPropertyChanged(); } }

        private string _EmailNV;
        public string EmailNV { get => _EmailNV; set { _EmailNV = value; OnPropertyChanged(); } }

        private string _SoDienThoaiNV;
        public string SoDienThoaiNV { get => _SoDienThoaiNV; set { _SoDienThoaiNV = value; OnPropertyChanged(); } }

        private double _LuongCoBan;
        public double LuongCoBan { get => _LuongCoBan; set { _LuongCoBan = value; OnPropertyChanged(); } }

        private double _PhanTramHoaHong;
        public double PhanTramHoaHong { get => _PhanTramHoaHong; set { _PhanTramHoaHong = value; OnPropertyChanged(); } }

        private string _HinhAnhNV;
        public string HinhAnhNV { get => _HinhAnhNV; set { _HinhAnhNV = value; OnPropertyChanged(); } }

        public ChiTietNhanVienModel() { }

        public ChiTietNhanVienModel convert(NHANVIEN NhanVien)
        {
            ChiTietNhanVienModel model = new ChiTietNhanVienModel();
            try
            {
                model.MaNhanVien = NhanVien.MaNhanVien;
                model.TenNhanVien = NhanVien.TenNhanVien;
                model.DiaChiNV = NhanVien.DiaChiNV;
                model.EmailNV = NhanVien.EmailNV;
                model.SoDienThoaiNV = NhanVien.SoDienThoaiNV;
                model.LuongCoBan = NhanVien.LuongCoBan.Value;
                model.HinhAnhNV = NhanVien.HinhAnhNV;
                
            }
            catch (Exception)
            {
            }
            return model;
        }
    }
}
