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
    
    public partial class NHACUNGCAP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHACUNGCAP()
        {
            this.PHIEUMUAHANGs = new HashSet<PHIEUMUAHANG>();
        }
    
        public int MaNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; }
        public string DiaChiNCC { get; set; }
        public string EmailNCC { get; set; }
        public string SoDienThoaiNCC { get; set; }
        public Nullable<double> TongTienBanNCC { get; set; }
        public Nullable<int> TrangThaiNCC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUMUAHANG> PHIEUMUAHANGs { get; set; }
    }
}
