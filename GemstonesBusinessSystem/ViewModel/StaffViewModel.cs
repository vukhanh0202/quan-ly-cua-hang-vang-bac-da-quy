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
    class StaffViewModel : BaseViewModel
    {

        #region command
        public ICommand ThemNVCommand { get; set; }

        public ICommand XoaNhanVien { get; set; }

        public ICommand ChiTietNVCommand { get; set; }

        public ICommand TimKiemCommand { get; set; }

        public ICommand XemLuongNVCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public RelayCommand<IList> SelectionChangedCommand { get; set; }
        #endregion

        #region list binding

        private IEnumerable<NHANVIEN> _DSNhanVien;
        public IEnumerable<NHANVIEN> DSNhanVien { get => _DSNhanVien; set { _DSNhanVien = value; OnPropertyChanged(); } }

        private ObservableCollection<NHANVIEN> _DSNVDaChon;
        public ObservableCollection<NHANVIEN> DSNVDaChon { get => _DSNVDaChon; set { _DSNVDaChon = value; OnPropertyChanged(); } }

        #endregion

        #region thuộc tính binding
        private NHANVIEN _NVDaChon;
        public NHANVIEN NVDaChon { get => _NVDaChon; set { _NVDaChon = value; OnPropertyChanged(); } }

        private int _SLNhanVien;
        public int SLNhanVien { get => _SLNhanVien; set { _SLNhanVien = value; OnPropertyChanged(); } }

        private string _TimKiem;
        public string TimKiem { get => _TimKiem; set { _TimKiem = value; OnPropertyChanged(); } }

        public static int status;
        #endregion

        public StaffViewModel()
        {


            #region command

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

            // Thêm nhân viên
            ThemNVCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                AddStaffWindow addStaffWindow = new AddStaffWindow();
                var addstaffvm = addStaffWindow.DataContext as AddStaffViewModel;
                addStaffWindow.ShowDialog();
                LayDSTuDatabase();
            });

            // Xem lương nhân viên
            XemLuongNVCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                SalaryWindow salaryWindow = new SalaryWindow();
                salaryWindow.ShowDialog();
            });

            // Lấy danh sách được chọn từ dataGrid
            SelectionChangedCommand = new RelayCommand<IList>((items) =>
            {
                return true;
            }, (items) =>
            {
                DSNVDaChon.Clear();
                foreach (var item in items)
                    DSNVDaChon.Add((NHANVIEN)item);
            });

            // Xem lại xóa account không xóa nhân viên
            //DeleteStaffCommand = new RelayCommand<Object>((p) =>
            //{
            //    if (selectedStaff.Count > 0)
            //    {
            //        return true;
            //    }
            //    return false;
            //}, (p) =>
            //{
            //    MessageBoxResult isExit = System.Windows.MessageBox.Show("Dữ liệu đã xóa không thể khôi phục, bạn có chắc chắn xóa?", "Xác nhận", MessageBoxButton.OKCancel);
            //    if (isExit == MessageBoxResult.OK)
            //    {
            //        try
            //        {
            //            foreach (var item in listStaff.ToList())
            //            {
            //                foreach (var selected in selectedStaff)
            //                {
            //                    if (item.id == selected.id)
            //                    {
            //                        DataProvider.Ins.DB.Staffs.Remove(listStaff.Where(x => x.id == item.id).SingleOrDefault());
            //                        DataProvider.Ins.DB.SaveChanges();
            //                    }
            //                }
            //            }
            //            MessageBox.Show("Xóa thành công");
            //        }
            //        catch (Exception e)
            //        {
            //            MessageBox.Show("Xóa thất bại vui lòng thử lại");
            //        }
            //    }
            //    LoadListStaff();
            //});

            // Chi tiết nhân viên
            ChiTietNVCommand = new RelayCommand<Object>((p) => {
                if (NVDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                InfoStaffWindow infoStaffWindow = new InfoStaffWindow();
                (infoStaffWindow.DataContext as InfoStaffViewModel).LoadNhanVien(NVDaChon);
                infoStaffWindow.ShowDialog();
                LayDSTuDatabase();
            });

            // Tìm kiếm
            TimKiemCommand = new RelayCommand<Object>((p) =>
            {
                if (Utils.ConvertString.convertString(TimKiem) != "" && DSNhanVien != null)
                {
                    return true;
                }
                else
                { 
                    LayDSTuDatabase();
                    return false;
                }
            }, (p) =>
            {
                DSNhanVien = DSNhanVien.Where(x => x.MaNhanVien.ToString().Contains(TimKiem.ToLower())
                                                    || x.TenNhanVien.ToLower().Contains(TimKiem.ToLower())
                                                    || x.EmailNV.ToLower().Contains(TimKiem.ToLower())
                                                    || x.SoDienThoaiNV.ToLower().Contains(TimKiem.ToLower())
                                                    || x.DiaChiNV.ToLower().Contains(TimKiem.ToLower()));
                SLNhanVien = DSNhanVien.Count();
            });

            #endregion
        }
        public void LayDSTuDatabase()
        {
            //var RoleAdmin = DataProvider.Ins.DB.AccountRoles.Where(x => x.code == Constant.codeRoleADMIN).SingleOrDefault();
            //var AccountADMIN = DataProvider.Ins.DB.Accounts.Where(x => x.account_roles_id == RoleAdmin.id).SingleOrDefault();
            //DSNhanVien = new ObservableCollection<Staff>(DataProvider.Ins.DB.Staffs.Where(x => x.account_id != AccountADMIN.id));
            DSNhanVien = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            DSNVDaChon = new ObservableCollection<NHANVIEN>();
            SLNhanVien = DSNhanVien.Count();
        }
    }
}
