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
    class DetailBillServiceViewModel : BaseViewModel
    {
        #region list binding

        private ObservableCollection<CT_PDV> _DSChiTietPDV;
        public ObservableCollection<CT_PDV> DSChiTietPDV { get => _DSChiTietPDV; set { _DSChiTietPDV = value; OnPropertyChanged(); } }
        #endregion

        #region thuộc tính binding
        private PHIEUDICHVU _PhieuDichVu;
        public PHIEUDICHVU PhieuDichVu { get => _PhieuDichVu; set { _PhieuDichVu = value; OnPropertyChanged(); } }
        #endregion

        #region command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        #endregion
        public DetailBillServiceViewModel()
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
        public void LoadChiTietPhieuDichVu(PHIEUDICHVU ChiTietPhieuDichVu)
        {
            PhieuDichVu = ChiTietPhieuDichVu;
            DSChiTietPDV = new ObservableCollection<CT_PDV>(DataProvider.Ins.DB.CT_PDV.Where(x => x.MaPhieuDichVu == ChiTietPhieuDichVu.MaPhieuDichVu));
        }
    }
}
