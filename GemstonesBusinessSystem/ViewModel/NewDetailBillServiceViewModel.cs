using GemstonesBusinessSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GemstonesBusinessSystem.ViewModel
{
    class NewDetailBillServiceViewModel : BaseViewModel
    {
        #region Binding
        private ChiTietPDVModel _CTHDMoi;
        public ChiTietPDVModel CTHDMoi { get => _CTHDMoi; set { _CTHDMoi = value; OnPropertyChanged(); } }

        private string _ThanhTien;
        public string ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }
        private string _TienConLai;
        public string TienConLai { get => _TienConLai; set { _TienConLai = value; OnPropertyChanged(); } }

        private string _TienYeuCauTraTruoc;
        public string TienYeuCauTraTruoc { get => _TienYeuCauTraTruoc; set { _TienYeuCauTraTruoc = value; OnPropertyChanged(); } }

        private DICHVU _DVDaChon;
        public DICHVU DVDaChon { get => _DVDaChon; set { _DVDaChon = value; OnPropertyChanged(); } }
        #endregion

        #region List Binding
        private ObservableCollection<DICHVU> _DSDichVu;
        public ObservableCollection<DICHVU> DSDichVu { get => _DSDichVu; set { _DSDichVu = value; OnPropertyChanged(); } }

        #endregion


        #region Command
        public ICommand XacNhanCommand { get; set; }

        public ICommand HuyBoCommand { get; set; }

        public ICommand SelectionChangeCommand { get; set; }

        public ICommand TienTraTruocThayDoiCommand { get; set; }

        public ICommand SLThayDoiCommand { get; set; }

        public ICommand ChiPhiThemThayDoiCommand { get; set; }

        public ICommand LoadedWindowCommand { get; set; }

        #endregion

        private bool flagIsConfirm;

        public NewDetailBillServiceViewModel()
        {

            #region command

            // Command khi load
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                flagIsConfirm = false;
                LayDSTuDatabase();
                //KhoiTaoGiaTri();
            });

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {

                if (Utils.ConvertUtils.convertString(DVDaChon.TenDichVu) != ""
                    && Utils.ConvertUtils.convertString(CTHDMoi.NgayGiao.ToString()) != ""
                    && Utils.ConvertUtils.convertString(CTHDMoi.TienTraTruoc.ToString()) != ""
                    && Utils.ConvertUtils.convertString(CTHDMoi.TienConlai.ToString()) != ""
                    && CTHDMoi.ChiPhiRieng >= 0
                    && Utils.ConvertUtils.convertString(CTHDMoi.SoLuongDichVu.ToString()) != "")
                {
                    if (CTHDMoi.TienTraTruoc > CTHDMoi.ThanhTien)
                    {
                        MessageBox.Show("Vui lòng nhập tổng trả trước nhỏ hơn hoặc bằng thành tiền!");
                        return false;
                    }
                    if (CTHDMoi.NgayGiao.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show("Vui lòng nhập ngày giao trễ hơn ngày hiện tại!");
                        return false;
                    }
                    if (CTHDMoi.SoLuongDichVu < 1)
                    {
                        MessageBox.Show("Vui lòng kiểm tra lại số lượng dịch vụ");
                        return false;
                    }
                    double PhamTramTraTruoc = (double)DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "PhamTramTraTruoc").SingleOrDefault().GiaTri;
                    if (CTHDMoi.TienTraTruoc < (CTHDMoi.ThanhTien * PhamTramTraTruoc / 100))
                    {
                        MessageBox.Show("Vui lòng nhập tổng trả trước phù hợp!");
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
                CTHDMoi.MaDichVu = DVDaChon.MaDichVu;
                CTHDMoi.DICHVU = DVDaChon;
                CTHDMoi.DonGiaDichVu = CTHDMoi.DICHVU.DonGiaDV.Value;
                CTHDMoi.DonGiaDuocTinh = CTHDMoi.DonGiaDichVu + CTHDMoi.ChiPhiRieng;
                CTHDMoi.TinhTrangCT_PDV = Constant.INACTIVE_STATUS;
                flagIsConfirm = true;
                p.Close();
            });

            // Số lượng thay đổi
            SLThayDoiCommand = new RelayCommand<Window>((p) =>
            {
                try
                {
                    if (DVDaChon != null)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception) { return false; }
            }, (p) =>
            {
                try
                {
                    CTHDMoi.ThanhTien = ((DVDaChon.DonGiaDV.Value + CTHDMoi.ChiPhiRieng) * CTHDMoi.SoLuongDichVu);

                    if (CTHDMoi.TienTraTruoc.ToString() == "")
                    {
                        CTHDMoi.TienConlai = (double)((CTHDMoi.ThanhTien));
                    }
                    else
                    {
                        CTHDMoi.TienConlai = (double)((CTHDMoi.ThanhTien) - CTHDMoi.TienTraTruoc);
                    }
                    ThanhTien = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", CTHDMoi.ThanhTien);
                    TienConLai = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", CTHDMoi.TienConlai);

                    double PhamTramTraTruoc = (double)DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "PhamTramTraTruoc").SingleOrDefault().GiaTri;
                    TienYeuCauTraTruoc = "(≥ " + string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", (CTHDMoi.ThanhTien * PhamTramTraTruoc / 100)) + ")";

                }
                catch (Exception) { }
            });

            // Chi phí riêng thay đổi
            ChiPhiThemThayDoiCommand = new RelayCommand<Window>((p) =>
            {
                try
                {
                    if (DVDaChon != null)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }, (p) =>
            {
                try
                {
                    CTHDMoi.ThanhTien = ((DVDaChon.DonGiaDV.Value + CTHDMoi.ChiPhiRieng) * CTHDMoi.SoLuongDichVu);

                    if (CTHDMoi.TienTraTruoc.ToString() == "")
                    {
                        CTHDMoi.TienConlai = (double)((CTHDMoi.ThanhTien));
                    }
                    else
                    {
                        CTHDMoi.TienConlai = (double)((CTHDMoi.ThanhTien) - CTHDMoi.TienTraTruoc);
                    }
                    ThanhTien = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", CTHDMoi.ThanhTien);
                    TienConLai = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", CTHDMoi.TienConlai);

                    double PhamTramTraTruoc = (double)DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "PhamTramTraTruoc").SingleOrDefault().GiaTri;
                    TienYeuCauTraTruoc = "(≥ " + string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", (CTHDMoi.ThanhTien * PhamTramTraTruoc / 100)) + ")";

                }
                catch (Exception) { }

            });

            // Khi tổng tiền trả trước thay đổi
            TienTraTruocThayDoiCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    CTHDMoi.TienConlai = (double)((CTHDMoi.ThanhTien) - CTHDMoi.TienTraTruoc);

                    TienConLai = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", CTHDMoi.TienConlai);
                }
                catch (Exception)
                {
                }

            });

            // Chọn dịch vụ
            SelectionChangeCommand = new RelayCommand<Window>((p) =>
            {
                if (DVDaChon != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                try
                {
                    CTHDMoi.ThanhTien = ((DVDaChon.DonGiaDV.Value + CTHDMoi.ChiPhiRieng) * CTHDMoi.SoLuongDichVu);
                    if (CTHDMoi.TienTraTruoc.ToString() == "")
                    {
                        CTHDMoi.TienConlai = (double)((CTHDMoi.ThanhTien));
                    }
                    else
                    {
                        CTHDMoi.TienConlai = (double)((CTHDMoi.ThanhTien) - CTHDMoi.TienTraTruoc);
                    }
                    ThanhTien = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", CTHDMoi.ThanhTien);
                    TienConLai = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", CTHDMoi.TienConlai);

                    double PhamTramTraTruoc = (double)DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "PhamTramTraTruoc").SingleOrDefault().GiaTri;
                    TienYeuCauTraTruoc = "(≥ " + string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", (CTHDMoi.ThanhTien * PhamTramTraTruoc / 100)) + ")";

                }
                catch (Exception e) { }
            });

            // Hủy bỏ
            HuyBoCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
            #endregion
        }

        public void LayDSTuDatabase()
        {
            DSDichVu = new ObservableCollection<DICHVU>(DataProvider.Ins.DB.DICHVUs.Where(x => x.TrangThaiDV == Constant.ACTIVE_STATUS));
        }
        public void KhoiTaoGiaTri(ChiTietPDVModel ChiTiet = null)
        {
            if (ChiTiet != null)
            {

                //CTHDMoi.DICHVU = Chitiet.DICHVU;
                //CTHDMoi.MaCT_PDV = Chitiet.MaCT_PDV;
                //CTHDMoi.MaPhieuDichVu = Chitiet.MaPhieuDichVu;
                //CTHDMoi.MaDichVu = Chitiet.MaDichVu;
                //CTHDMoi.DonGiaDichVu = Chitiet.DonGiaDichVu;
                //CTHDMoi.ChiPhiRieng = Chitiet.ChiPhiRieng;
                //CTHDMoi.DonGiaDuocTinh = Chitiet.DonGiaDuocTinh;
                //CTHDMoi.SoLuongDichVu = Chitiet.SoLuongDichVu;
                //CTHDMoi.ThanhTien = Chitiet.ThanhTien;
                //CTHDMoi.TienTraTruoc = Chitiet.TienTraTruoc;
                //CTHDMoi.TienConlai = Chitiet.TienConlai;
                //CTHDMoi.NgayGiao = Chitiet.NgayGiao;
                //CTHDMoi.TinhTrangCT_PDV = Chitiet.TinhTrangCT_PDV;

                //DVDaChon = Chitiet.DICHVU;

                CTHDMoi = ChiTiet;
            }
            else
            {
                DVDaChon = null;
                CTHDMoi = new ChiTietPDVModel();
                CTHDMoi.ThanhTien = 0;
                CTHDMoi.TienConlai = 0;
            }

            ThanhTien = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", CTHDMoi.ThanhTien);
            TienConLai = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", CTHDMoi.TienConlai);

            double PhamTramTraTruoc = (double)DataProvider.Ins.DB.THAMSOes.Where(x => x.TenThamSo == "PhamTramTraTruoc").SingleOrDefault().GiaTri;
            TienYeuCauTraTruoc = "(≥ " + string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", (CTHDMoi.ThanhTien * PhamTramTraTruoc / 100)) + ")";
        }
        public ChiTietPDVModel LayCTHDMoi()
        {
            // Nếu ấn nút xác nhận mới trả về
            if (flagIsConfirm == true)
            {
                CTHDMoi.NgayGiao = CTHDMoi.NgayGiao.Date;
                return CTHDMoi;
            }
            return null;
        }
    }
}
