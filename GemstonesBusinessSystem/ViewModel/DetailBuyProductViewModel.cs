using GemstonesBusinessSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{

    class DetailBuyProductViewModel : BaseViewModel
    {
        private IEnumerable<CT_PMH> _DSChiTietPMH;
        public IEnumerable<CT_PMH> DSChiTietPMH { get => _DSChiTietPMH; set { _DSChiTietPMH = value; OnPropertyChanged(); } }

        private PHIEUMUAHANG _PhieuMuaHang;
        public PHIEUMUAHANG PhieuMuaHang { get => _PhieuMuaHang; set { _PhieuMuaHang = value; OnPropertyChanged(); } }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }

        public DetailBuyProductViewModel()
        {
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
        }
        public void LoadChiTietPhieuMuaHang(PHIEUMUAHANG ChiTietPhieuMuaHang)
        {
            PhieuMuaHang = ChiTietPhieuMuaHang;
            DSChiTietPMH = DataProvider.Ins.DB.CT_PMH.Where(x => x.MaPhieuMuaHang == PhieuMuaHang.MaPhieuMuaHang);
        }
    }
}
