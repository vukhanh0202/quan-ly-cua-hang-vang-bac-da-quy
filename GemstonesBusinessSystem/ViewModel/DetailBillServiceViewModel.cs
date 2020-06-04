using GemstonesBusinessSystem.Model;
using System;
using System.Collections;
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

        private ObservableCollection<CT_PDV> _DSCTDaChon;
        public ObservableCollection<CT_PDV> DSCTDaChon { get => _DSCTDaChon; set { _DSCTDaChon = value; OnPropertyChanged(); } }
        #endregion

        #region thuộc tính binding
        private PHIEUDICHVU _PhieuDichVu;
        public PHIEUDICHVU PhieuDichVu { get => _PhieuDichVu; set { _PhieuDichVu = value; OnPropertyChanged(); } }
        #endregion

        #region command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand HoanThanhCommand { get; set; }
        public RelayCommand<IList> SelectionChangedCommand { get; set; }
        #endregion
        public DetailBillServiceViewModel()
        {

            // load
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                DSCTDaChon = new ObservableCollection<CT_PDV>();
            });

            // Lấy danh sách được chọn từ dataGrids
            SelectionChangedCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                try
                {
                    DSCTDaChon.Clear();
                    foreach (var item in items)
                        DSCTDaChon.Add((CT_PDV)item);
                }
                catch (Exception)
                {
                }
            });
            // Xóa product
            HoanThanhCommand = new RelayCommand<Object>((p) =>
            {
                if (DSCTDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                try
                {
                    foreach (var CT in DSCTDaChon)
                    {
                        DataProvider.Ins.DB.CT_PDV.Where(x => x.MaCT_PDV == CT.MaCT_PDV).FirstOrDefault().TinhTrangCT_PDV = Constant.ACTIVE_STATUS;
                    }

                    bool flagHoanThanh = true;
                    foreach (var item in DSChiTietPDV)
                    {
                        if (item.TinhTrangCT_PDV == Constant.INACTIVE_STATUS)
                        {
                            flagHoanThanh = false;
                        }
                    }
                    if (flagHoanThanh == true)
                    {
                        var PDVDB = DataProvider.Ins.DB.PHIEUDICHVUs.Where(x => x.MaPhieuDichVu == PhieuDichVu.MaPhieuDichVu).FirstOrDefault();
                        PDVDB.TinhTrangPDV = Constant.ACTIVE_STATUS;
                        PhieuDichVu = PDVDB;
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    DSChiTietPDV = new ObservableCollection<CT_PDV>(DataProvider.Ins.DB.CT_PDV.Where(x => x.MaPhieuDichVu == PhieuDichVu.MaPhieuDichVu));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Thất bại");
                }

            });
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
