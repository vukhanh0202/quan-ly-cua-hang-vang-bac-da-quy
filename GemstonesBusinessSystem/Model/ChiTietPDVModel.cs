using GemstonesBusinessSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemstonesBusinessSystem.Model
{
    class ChiTietPDVModel : BaseViewModel
    {
        private int _MaCT_PDV;
        public int MaCT_PDV { get => _MaCT_PDV; set { _MaCT_PDV = value; OnPropertyChanged(); } }

        private int _MaPhieuDichVu;
        public int MaPhieuDichVu { get => _MaPhieuDichVu; set { _MaPhieuDichVu = value; OnPropertyChanged(); } }

        private PHIEUDICHVU _PHIEUDICHVU;
        public PHIEUDICHVU PHIEUDICHVU { get => _PHIEUDICHVU; set { _PHIEUDICHVU = value; OnPropertyChanged(); } }

        private int _MaDichVu;
        public int MaDichVu { get => _MaDichVu; set { _MaDichVu = value; OnPropertyChanged(); } }

        private DICHVU _DICHVU;
        public DICHVU DICHVU { get => _DICHVU; set { _DICHVU = value; OnPropertyChanged(); } }

        private double _DonGiaDichVu;
        public double DonGiaDichVu { get => _DonGiaDichVu; set { _DonGiaDichVu = value; OnPropertyChanged(); } }

        private double _ChiPhiRieng;
        public double ChiPhiRieng { get => _ChiPhiRieng; set { _ChiPhiRieng = value; OnPropertyChanged(); } }

        private double _DonGiaDuocTinh;
        public double DonGiaDuocTinh { get => _DonGiaDuocTinh; set { _DonGiaDuocTinh = value; OnPropertyChanged(); } }

        private int _SoLuongDichVu;
        public int SoLuongDichVu { get => _SoLuongDichVu; set { _SoLuongDichVu = value; OnPropertyChanged(); } }

        private double _ThanhTien;
        public double ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }

        private double _TienTraTruoc;
        public double TienTraTruoc { get => _TienTraTruoc; set { _TienTraTruoc = value; OnPropertyChanged(); } }

        private double _TienConlai;
        public double TienConlai { get => _TienConlai; set { _TienConlai = value; OnPropertyChanged(); } }

        private DateTime _NgayGiao;
        public DateTime NgayGiao { get => _NgayGiao; set { _NgayGiao = value; OnPropertyChanged(); } }

        private int _TinhTrangCT_PDV;
        public int TinhTrangCT_PDV { get => _TinhTrangCT_PDV; set { _TinhTrangCT_PDV = value; OnPropertyChanged(); } }

        public ChiTietPDVModel()
        {
            DonGiaDichVu = 0;
            ChiPhiRieng = 0;
            DonGiaDuocTinh = 0;
            ThanhTien = 0;
            TienTraTruoc = 0;
            TienConlai = 0;
            NgayGiao = DateTime.Now.Date;
        }
    }
}
