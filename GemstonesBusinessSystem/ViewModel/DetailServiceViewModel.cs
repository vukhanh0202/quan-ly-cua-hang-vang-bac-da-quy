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


        private string _TenDichVu;
        public string TenDichVu { get => _TenDichVu; set { _TenDichVu = value; OnPropertyChanged(); } }

        private double _DonGia;
        public double DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }

        private int _MaDichVu;
        public int MaDichVu { get => _MaDichVu; set { _MaDichVu = value; OnPropertyChanged(); } }

        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }

        public DetailServiceViewModel()
        {

            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertUtils.convertString(TenDichVu) != ""
                    && Utils.ConvertUtils.convertString(DonGia.ToString()) != "")
                {
                    if (DonGia <= 0)
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
                    DICHVU DichVuDB = DataProvider.Ins.DB.DICHVUs.Where(x => x.MaDichVu == MaDichVu).SingleOrDefault();
                    DichVuDB.TenDichVu = TenDichVu;
                    DichVuDB.DonGiaDV = DonGia;

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
            TenDichVu = DichVu.TenDichVu;
            MaDichVu = DichVu.MaDichVu;
            DonGia = DichVu.DonGiaDV.Value;

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

