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
    class NewServiceViewModel : BaseViewModel
    {
        #region Thuộc tính binding
        private DICHVU _DichVuMoi;
        public DICHVU DichVuMoi { get => _DichVuMoi; set { _DichVuMoi = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }
        #endregion


        public NewServiceViewModel()
        {
            #region Command

            // Command khi load vào window
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                DichVuMoi = new DICHVU();
            });

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertString.convertString(DichVuMoi.TenDichVu) != ""
                    && Utils.ConvertString.convertString(DichVuMoi.DonGiaDV.ToString()) != "")
                {
                    if (DichVuMoi.DonGiaDV <= 0)
                    {
                        MessageBox.Show("Vui lòng nhập đơn giá dịch vụ lớn hơn 0");
                        return false;
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                    return false;
                }
            }, (p) =>
            {

                try
                {
                    DichVuMoi.TrangThaiDV = Constant.ACTIVE_STATUS;
                    DataProvider.Ins.DB.DICHVUs.Add(DichVuMoi);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    p.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Thêm thất bại, vui lòng kiểm tra lại");
                }
            });

            HuyBoCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
            #endregion
        }


    }
}
