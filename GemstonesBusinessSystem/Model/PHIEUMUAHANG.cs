//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GemstonesBusinessSystem.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHIEUMUAHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUMUAHANG()
        {
            this.CT_PMH = new HashSet<CT_PMH>();
        }
    
        public int MaPhieuMuaHang { get; set; }
        public Nullable<int> MaNhaCungCap { get; set; }
        public Nullable<System.DateTime> NgayLapPhieuMua { get; set; }
        public Nullable<int> TongSoLuongMua { get; set; }
        public Nullable<double> TongThanhTien { get; set; }
        public Nullable<int> MaNhanVien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PMH> CT_PMH { get; set; }
        public virtual NHACUNGCAP NHACUNGCAP { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
