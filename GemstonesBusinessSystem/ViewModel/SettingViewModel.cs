using GemstonesBusinessSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class SettingViewModel : BaseViewModel
    {
        #region thuộc tính binding
        private double _PhanTramTraTruoc;
        public double PhanTramTraTruoc { get => _PhanTramTraTruoc; set { _PhanTramTraTruoc = value; OnPropertyChanged(); } }

        private double _GiaTriMoi;
        public double GiaTriMoi { get => _GiaTriMoi; set { _GiaTriMoi = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        #region command
        public ICommand ReloadCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        #endregion
        public SettingViewModel()
        {
            // Load trang sau mỗi lần click mở trang
            ReloadCommand = new RelayCommand<Object>((p) =>
            {
                if (status == 1)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                LayDSTuDatabase();
                status = 0;
            });

            XacNhanCommand = new RelayCommand<Object>((p) =>
            {
                if (GiaTriMoi > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo.Equals("PhanTramTraTruoc")).FirstOrDefault().GiaTri = GiaTriMoi;
                DataProvider.Ins.DB.SaveChanges();
                GiaTriMoi = new double();
                LayDSTuDatabase();
            });
        }
        public void LayDSTuDatabase()
        {
            PhanTramTraTruoc = DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo.Equals("PhanTramTraTruoc")).FirstOrDefault().GiaTri.Value;
        }
    }
}
