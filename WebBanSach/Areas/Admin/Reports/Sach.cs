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
    
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            this.BinhLuans = new HashSet<BinhLuan>();
            this.ChiTietDonBans = new HashSet<ChiTietDonBan>();
            this.ChiTietDonNhaps = new HashSet<ChiTietDonNhap>();
            this.ThamGias = new HashSet<ThamGia>();
        }
    
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public int MaLoai { get; set; }
        public int MaNXB { get; set; }
        public int MaNCC { get; set; }
        public string MoTa { get; set; }
        public string Hinh { get; set; }
        public string SoTrang { get; set; }
        public double GiaBan { get; set; }
        public int SoLuong { get; set; }
        public System.DateTime NgayTao { get; set; }
        public System.DateTime NgaySua { get; set; }
        public int LuotMua { get; set; }
        public string File { get; set; }
        public double SoSao { get; set; }
        public bool TrangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonBan> ChiTietDonBans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonNhap> ChiTietDonNhaps { get; set; }
        public virtual LoaiSach LoaiSach { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
        public virtual NhaXuatBan NhaXuatBan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThamGia> ThamGias { get; set; }
    }
}
