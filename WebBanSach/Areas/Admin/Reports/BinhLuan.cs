//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanSach.Areas.Admin.Reports
{
    using System;
    using System.Collections.Generic;
    
    public partial class BinhLuan
    {
        public int MaBinhLuan { get; set; }
        public string MaKhachHang { get; set; }
        public int MaSach { get; set; }
        public string NoiDung { get; set; }
        public int SoSao { get; set; }
        public System.DateTime NgayBinhLuan { get; set; }
        public bool TrangThai { get; set; }
    
        public virtual KhachHang KhachHang { get; set; }
        public virtual Sach Sach { get; set; }
    }
}
