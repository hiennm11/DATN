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
    
    public partial class DonHangBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHangBan()
        {
            this.ChiTietDonBans = new HashSet<ChiTietDonBan>();
        }
    
        public int MaDonHang { get; set; }
        public string MaKhachHang { get; set; }
        public string HoTenKH { get; set; }
        public string DiaChiKH { get; set; }
        public string EmailKH { get; set; }
        public string SdtKH { get; set; }
        public System.DateTime NgayBan { get; set; }
        public double TongTien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonBan> ChiTietDonBans { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}