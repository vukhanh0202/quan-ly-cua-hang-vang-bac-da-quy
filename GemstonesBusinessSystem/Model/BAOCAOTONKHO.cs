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
    
    public partial class BAOCAOTONKHO
    {
        public int MaBaoCaoTonKho { get; set; }
        public Nullable<int> Thang { get; set; }
        public Nullable<int> Nam { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public Nullable<int> TonDau { get; set; }
        public Nullable<int> SLMuaVao { get; set; }
        public Nullable<int> SLBanRa { get; set; }
        public Nullable<int> TonCuoi { get; set; }
        public Nullable<int> MaDVT { get; set; }
    
        public virtual DVT DVT { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}