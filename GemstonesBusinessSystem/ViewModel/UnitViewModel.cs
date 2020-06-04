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
    class UnitViewModel : BaseViewModel
    {
        private DVT _DVTMoi;
        public DVT DVTMoi { get => _DVTMoi; set { _DVTMoi = value; OnPropertyChanged(); } }

        private ObservableCollection<DVT> _DSDVT;
        public ObservableCollection<DVT> DSDVT { get => _DSDVT; set { _DSDVT = value; OnPropertyChanged(); } }

        private ObservableCollection<DVT> _DSDVTDaChon;
        public ObservableCollection<DVT> DSDVTDaChon { get => _DSDVTDaChon; set { _DSDVTDaChon = value; OnPropertyChanged(); } }


        private int _TongSL;
        public int TongSL { get => _TongSL; set { _TongSL = value; OnPropertyChanged(); } }

        private DVT _DVTDaChon;
        public DVT DVTDaChon { get => _DVTDaChon; set { _DVTDaChon = value; OnPropertyChanged(); } }

        public bool isChanged = false;
        #region Command

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand ThemMoiCommand { get; set; }
        public ICommand XoaDVTCommand { get; set; }
        public ICommand SuaDVTCommand { get; set; }
        public ICommand XacNhanSuaDVTCommand { get; set; }
        public ICommand KhoiPhucCommand { get; set; }
        public RelayCommand<IList> SelectionChangedCommand { get; set; }

        #endregion
        public UnitViewModel()
        {
            // Command khi load vào window
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                DSDVTDaChon = new ObservableCollection<DVT>();
                DVTDaChon = null;
                DVTMoi = new DVT();
                LayDSTuDatabase();
            });

            // Lấy danh sách được chọn từ dataGrids
            SelectionChangedCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                try
                {
                    DSDVTDaChon.Clear();
                    foreach (var item in items)
                        DSDVTDaChon.Add((DVT)item);
                }
                catch (Exception)
                {
                }
            });

            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });

            // Thêm mới đơn vị tính
            ThemMoiCommand = new RelayCommand<Window>((p) =>
            {
                if (Utils.ConvertUtils.convertString(DVTMoi.TenDVT) != "")
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                if (DSDVT.Where(x => x.TenDVT.ToLower().Equals(DVTMoi.TenDVT.ToLower())).SingleOrDefault() != null)
                {
                    MessageBox.Show("Đơn vị tính đã tồn tại");
                }
                else
                {
                    DVTMoi.TrangThaiDVT = Constant.ACTIVE_STATUS;
                    DataProvider.Ins.DB.DVTs.Add(DVTMoi);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    DVTMoi = null;
                    LayDSTuDatabase();
                }
            });

            // Khôi phục
            KhoiPhucCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                try
                {
                    foreach (var DVT in DSDVTDaChon)
                    {

                        if (DVT.TrangThaiDVT == Constant.INACTIVE_STATUS)
                        {
                            DataProvider.Ins.DB.DVTs.Where(x => x.MaDVT == DVT.MaDVT).SingleOrDefault().TrangThaiDVT = Constant.ACTIVE_STATUS;
                            MessageBox.Show("Thành Công");
                        }
                        DataProvider.Ins.DB.SaveChanges();

                    }
                    LayDSTuDatabase();
                    DVTDaChon = null;
                }
                catch (Exception)
                {
                    MessageBox.Show("Thất bại");
                }
            });

            // Xóa đơn vị tính
            XoaDVTCommand = new RelayCommand<Object>((p) =>
            {
                if (DSDVTDaChon.Count > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {

                MessageBoxResult isExit = System.Windows.MessageBox.Show("Bạn có chắc chắn thực hiện hành động này?", "Xác nhận", MessageBoxButton.OKCancel);
                if (isExit == MessageBoxResult.OK)
                {
                    try
                    {
                        foreach (var DVT in DSDVTDaChon)
                        {

                            if (DVT.TrangThaiDVT == Constant.ACTIVE_STATUS)
                            {
                                DataProvider.Ins.DB.DVTs.Where(x => x.MaDVT == DVT.MaDVT).SingleOrDefault().TrangThaiDVT = Constant.INACTIVE_STATUS;
                                MessageBox.Show("Thành Công");
                            }
                            else if (DVT.TrangThaiDVT == Constant.INACTIVE_STATUS)
                            {
                                // Xóa các hóa đơn liên quan
                                var LSP = DataProvider.Ins.DB.LOAISANPHAMs.Where(x => x.MaDVT == DVT.MaDVT).FirstOrDefault();
                                var BaoCao = DataProvider.Ins.DB.BAOCAOTONKHOes.Where(x => x.MaDVT == DVT.MaDVT).FirstOrDefault();
                                if (LSP != null && BaoCao != null)
                                {
                                    MessageBox.Show("Đơn vị tính đang được sử dụng, vui lòng xóa các dữ liệu liên quan!");
                                }
                                else
                                {
                                    DataProvider.Ins.DB.DVTs.Remove(DVT);
                                    MessageBox.Show("Thành Công");
                                }
                            }
                        }
                        DataProvider.Ins.DB.SaveChanges();

                        LayDSTuDatabase();
                        DVTDaChon = null;

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Thất bại");
                    }
                }

            });

            // Chỉnh sửa đơn vị tính
            SuaDVTCommand = new RelayCommand<object>((p) =>
            {
                if (DVTDaChon != null)
                {
                    return true; ;
                }
                return false;
            }, (p) =>
            {
                isChanged = true;
            });

            // thực hiện điều chỉnh sau khi chỉnh sửa
            XacNhanSuaDVTCommand = new RelayCommand<object>((p) =>
            {
                if (isChanged == true)
                {
                    return true; ;
                }
                return false;
            }, (p) =>
            {
                try
                {
                    DataProvider.Ins.DB.DVTs.Where(x => x.MaDVT == DVTDaChon.MaDVT).SingleOrDefault().TenDVT = DVTDaChon.TenDVT;
                    DataProvider.Ins.DB.SaveChanges();

                    LayDSTuDatabase();
                    DVTDaChon = null;
                    isChanged = false;
                }
                catch (Exception)
                {
                }

            });

        }
        void LayDSTuDatabase()
        {
            DSDVT = new ObservableCollection<DVT>(DataProvider.Ins.DB.DVTs);
            TongSL = DSDVT.Count();
        }
    }
}
