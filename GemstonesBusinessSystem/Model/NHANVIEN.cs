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
    
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            this.PHIEUBANHANGs = new HashSet<PHIEUBANHANG>();
            this.PHIEUDICHVUs = new HashSet<PHIEUDICHVU>();
            this.PHIEUMUAHANGs = new HashSet<PHIEUMUAHANG>();
        }
    
        public int MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string DiaChiNV { get; set; }
        public string EmailNV { get; set; }
        public string SoDienThoaiNV { get; set; }
        public Nullable<double> LuongCoBan { get; set; }
        public Nullable<double> PhanTramHoaHong { get; set; }
        public string HinhAnhNV { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUBANHANG> PHIEUBANHANGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUDICHVU> PHIEUDICHVUs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUMUAHANG> PHIEUMUAHANGs { get; set; }
    }
}
