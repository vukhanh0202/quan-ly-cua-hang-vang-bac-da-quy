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
    class DetailServiceViewModel : BaseViewModel
    {
        private string _TrangThaiDV;
        public string TrangThaiDV { get => _TrangThaiDV; set { _TrangThaiDV = value; OnPropertyChanged(); } }

        private DICHVU _DichVu;
        public DICHVU DichVu { get => _DichVu; set { _DichVu = value; OnPropertyChanged(); } }

        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }

        public DetailServiceViewModel()
        {

            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertString.convertString(DichVu.TenDichVu) != ""
                    && Utils.ConvertString.convertString(DichVu.DonGiaDV.ToString()) != "")
                {
                    if (DichVu.DonGiaDV <= 0)
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
                    DICHVU DichVuDB = DataProvider.Ins.DB.DICHVUs.Where(x => x.MaDichVu == DichVu.MaDichVu).SingleOrDefault();
                    DichVuDB = DichVu;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật thành công");
                    p.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cập nhật thất bại, vui lòng kiểm tra lại");
                }
            });

            HuyBoCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
        }


        public void LoadChiTietDichVu(DICHVU DichVu)
        {
            this.DichVu = DichVu;
            if (DichVu.TrangThaiDV == Constant.ACTIVE_STATUS)
            {
                TrangThaiDV = "Đang hoạt động";
            }
            else if (DichVu.TrangThaiDV == Constant.INACTIVE_STATUS)
            {
                TrangThaiDV = "Ngừng hoạt động";
            }
        }
    }
}

