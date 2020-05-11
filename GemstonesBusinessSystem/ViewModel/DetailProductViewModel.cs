using GemstonesBusinessSystem.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GemstonesBusinessSystem.ViewModel
{
    class DetailProductViewModel : BaseViewModel
    {

        #region List binding
        private ObservableCollection<SANPHAM> _DSSanPham;
        public ObservableCollection<SANPHAM> DSSanPham { get => _DSSanPham; set { _DSSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<CT_PBH> _DSCTPhieuBanHang;
        public ObservableCollection<CT_PBH> DSCTPhieuBanHang { get => _DSCTPhieuBanHang; set { _DSCTPhieuBanHang = value; OnPropertyChanged(); } }

        private ObservableCollection<CT_PMH> _DSCTPhieuMuaHang;
        public ObservableCollection<CT_PMH> DSCTPhieuMuaHang { get => _DSCTPhieuMuaHang; set { _DSCTPhieuMuaHang = value; OnPropertyChanged(); } }

        private ObservableCollection<LOAISANPHAM> _DSLoaiSanPham;
        public ObservableCollection<LOAISANPHAM> DSLoaiSanPham { get => _DSLoaiSanPham; set { _DSLoaiSanPham = value; OnPropertyChanged(); } }

        private IEnumerable<HoaDonSanPhamModel> _DSCTPhieuBanHangCuaSP;
        public IEnumerable<HoaDonSanPhamModel> DSCTPhieuBanHangCuaSP { get => _DSCTPhieuBanHangCuaSP; set { _DSCTPhieuBanHangCuaSP = value; OnPropertyChanged(); } }

        private IEnumerable<HoaDonSanPhamModel> _DSCTPhieuMuaHangCuaSP;
        public IEnumerable<HoaDonSanPhamModel> DSCTPhieuMuaHangCuaSP { get => _DSCTPhieuMuaHangCuaSP; set { _DSCTPhieuMuaHangCuaSP = value; OnPropertyChanged(); } }

        private IEnumerable<HoaDonSanPhamModel> _DSCTHDCuaSP;
        public IEnumerable<HoaDonSanPhamModel> DSCTHDCuaSP { get => _DSCTHDCuaSP; set { _DSCTHDCuaSP = value; OnPropertyChanged(); } }

        #endregion

        #region Thuộc tính Binding

        private SANPHAM _ChiTietSanPham;
        public SANPHAM ChiTietSanPham { get => _ChiTietSanPham; set { _ChiTietSanPham = value; OnPropertyChanged(); } }

        private LOAISANPHAM _LoaiSPDaChon;
        public LOAISANPHAM LoaiSPDaChon { get => _LoaiSPDaChon; set { _LoaiSPDaChon = value; OnPropertyChanged(); } }

        private ImageSource _imageSource;
        public ImageSource imageSource { get => _imageSource; set { _imageSource = value; OnPropertyChanged(); } }

        #endregion

        #region Command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XacNhanCommand { get; set; }
        public ICommand HuyBoCommand { get; set; }
        public ICommand GiaThayDoiCommand { get; set; }
        public ICommand SelectionChangeCommand { get; set; }
        public ICommand ThayDoiAnhCommand { get; set; }
        #endregion
        public DetailProductViewModel()
        {
            #region command
            // Load dữ liệu khởi tạo
            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                LayDSTuDatabase();
            });

            // Chọn loại sản phẩm
            SelectionChangeCommand = new RelayCommand<Window>((p) =>
            {
                if (LoaiSPDaChon == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                try
                {
                    ChiTietSanPham.MaLoaiSanPham = LoaiSPDaChon.MaLoaiSanPham;
                    ChiTietSanPham.DonGiaBan = Math.Round(((double)ChiTietSanPham.DonGiaNhap + ((double)ChiTietSanPham.DonGiaNhap * (double)ChiTietSanPham.LOAISANPHAM.PhanTramLoiNhuan / 100)));
                    //ChiTietSanPham.profitPercent = LoaiSPDaChon.profit_percent.ToString();
                    //ChiTietSanPham.salePrice = (double.Parse(ChiTietSanPham.purchasePrice) + (double.Parse(ChiTietSanPham.purchasePrice) * double.Parse(ChiTietSanPham.profitPercent) / 100)).ToString();
                }
                catch (Exception e) { }
            });


            // Xác nhận
            XacNhanCommand = new RelayCommand<Window>((p) =>
            {
                // Kiểm tra nhập đủ thông tin không
                if (Utils.ConvertUtils.convertString(ChiTietSanPham.DonGiaBan.ToString()) == ""
                || Utils.ConvertUtils.convertString(ChiTietSanPham.MaLoaiSanPham.ToString()) == ""
                || Utils.ConvertUtils.convertString(ChiTietSanPham.TenSanPham.ToString()) == "")
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                    return false;
                }
                else
                {
                    if (ChiTietSanPham.DonGiaNhap <= 0)
                    {
                        MessageBox.Show("Vui lòng nhập đơn giá sản phẩm lớn hơn 0");
                        return false;
                    }
                    if (Math.Round((double)(ChiTietSanPham.DonGiaBan)) != (Math.Round((double)(ChiTietSanPham.DonGiaNhap) + ((double)(ChiTietSanPham.DonGiaNhap) * (double)(ChiTietSanPham.LOAISANPHAM.PhanTramLoiNhuan) / 100))))
                    {
                        MessageBoxResult isExit = System.Windows.MessageBox.Show("Phần trăm lợi nhuận và giá bán không phù hợp, bạn có chắc chắc xác nhận", "Xác nhận", MessageBoxButton.OKCancel);
                        if (isExit == MessageBoxResult.OK)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }, (p) =>
            {
                try
                {
                    SANPHAM SanPham = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MaSanPham == ChiTietSanPham.MaSanPham).SingleOrDefault();
                    SanPham.TenSanPham = ChiTietSanPham.TenSanPham;
                    SanPham.MaLoaiSanPham = LoaiSPDaChon.MaLoaiSanPham;
                    SanPham.LOAISANPHAM = LoaiSPDaChon;
                    SanPham.DonGiaNhap = ChiTietSanPham.DonGiaNhap;
                    SanPham.DonGiaBan = ChiTietSanPham.DonGiaBan;
                    SanPham.TongSoLuongTon = ChiTietSanPham.TongSoLuongTon;
                    SanPham.HinhAnhSanPham = ChiTietSanPham.HinhAnhSanPham;

                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật thành công");
                    p.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cập nhật thất bại, vui lòng kiểm tra lại");
                }
            });

            // Hủy bỏ
            HuyBoCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });

            // Giá gốc thay đổi -> Giá bán thay đổi
            GiaThayDoiCommand = new RelayCommand<Object>((p) =>
            {
                try
                {
                    if (Utils.ConvertUtils.convertString(ChiTietSanPham.DonGiaNhap.ToString()) == "")
                    {
                        ChiTietSanPham.DonGiaBan = 0;
                        return false;
                    }
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }, (p) =>
            {
                try
                {
                    ChiTietSanPham.DonGiaBan = Math.Round(((double)ChiTietSanPham.DonGiaNhap + ((double)ChiTietSanPham.DonGiaNhap * (double)ChiTietSanPham.LOAISANPHAM.PhanTramLoiNhuan / 100)));
                }
                catch (Exception) { }
            });

            // Thay đổi ảnh
            ThayDoiAnhCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = @"D:\",
                    Title = "Chọn ảnh",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "txt",
                    Filter = "Images (*.JPG;*.PNG)|*.JPG;*.PNG",
                    RestoreDirectory = true,
                    ReadOnlyChecked = true,
                    ShowReadOnly = true
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    ChiTietSanPham.HinhAnhSanPham = Utils.HandleImage.ImageToString(openFileDialog.FileName);
                    imageSource = Utils.HandleImage.GetImage(ChiTietSanPham.HinhAnhSanPham);
                }
            });

            #endregion
        }
        public void LoadSanPham(SANPHAM SanPham)
        {
            ChiTietSanPham = SanPham;
            imageSource = Utils.HandleImage.GetImage(ChiTietSanPham.HinhAnhSanPham);
            LoaiSPDaChon = SanPham.LOAISANPHAM;
            //LoaiSPDaChon = (ProductType)DataProvider.Ins.DB.ProductTypes.Where(x => x.id == ChiTietSanPham.idTypeProduct).SingleOrDefault();

            DSCTPhieuBanHang = new ObservableCollection<CT_PBH>(DataProvider.Ins.DB.CT_PBH.Where(x => x.MaSanPham == ChiTietSanPham.MaSanPham));
            DSCTPhieuMuaHang = new ObservableCollection<CT_PMH>(DataProvider.Ins.DB.CT_PMH.Where(x => x.MaSanPham == ChiTietSanPham.MaSanPham));
            if (DSCTPhieuBanHang.Count() > 0)
            {
                DSCTPhieuBanHangCuaSP = (from CTHD in DSCTPhieuBanHang
                                         select new HoaDonSanPhamModel()
                                         {
                                             DonGiaSPHienTai = (double)CTHD.DonGiaSPHienTai,
                                             SoLuongSPHienTai = (int)CTHD.SoLuongSPHienTai,
                                             SoLuongGiaoDich = (int)CTHD.SoLuongBan,
                                             PhuongThucGD = CTHD.PhuongThuc,
                                             ThoiGianGD = (DateTime)CTHD.PHIEUBANHANG.NgayLapPhieuBan
                                         });
            }

            if (DSCTPhieuMuaHang.Count() > 0)
            {
                DSCTPhieuMuaHangCuaSP = (from CTHD in DSCTPhieuMuaHang
                                         select new HoaDonSanPhamModel()
                                         {
                                             DonGiaSPHienTai = (double)CTHD.DonGiaSPHienTai,
                                             SoLuongSPHienTai = (int)CTHD.SoLuongSPHienTai,
                                             SoLuongGiaoDich = (int)CTHD.SoLuongMua,
                                             PhuongThucGD = CTHD.PhuongThuc,
                                             ThoiGianGD = (DateTime)CTHD.PHIEUMUAHANG.NgayLapPhieuMua
                                         });
            }

            if (DSCTPhieuBanHang.Count == 0)
            {
                DSCTHDCuaSP = DSCTPhieuMuaHangCuaSP;
            }
            else
            {
                DSCTHDCuaSP = DSCTPhieuMuaHangCuaSP.Union(DSCTPhieuBanHangCuaSP);
            }
        }
        public void LayDSTuDatabase()
        {
            DSLoaiSanPham = new ObservableCollection<LOAISANPHAM>(DataProvider.Ins.DB.LOAISANPHAMs.Where(x => x.TrangThaiLoaiSanPham == Constant.ACTIVE_STATUS));
        }
    }
}
