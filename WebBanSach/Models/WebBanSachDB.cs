using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebBanSach.Models 
{
    public class WebBanSachDB : DbContext
    {
        public WebBanSachDB() : base("WebBanSachDB")
        {

        }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<Sach> Saches { get; set; }
        public DbSet<TacGia> TacGias { get; set; }
        public DbSet<LoaiSach> LoaiSaches { get; set; }
        public DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public DbSet<BinhLuan> BinhLuans { get; set; }
        public DbSet<ThamGia> ThamGias { get; set; }
        public DbSet<DonHangBan> DonHangBans { get; set; }
        public DbSet<ChiTietDonBan> ChiTietDonBans { get; set; }
        public DbSet<DonHangNhap> DonHangNhaps { get; set; }
        public DbSet<ChiTietDonNhap> ChiTietDonNhaps { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public object Image { get; internal set; }

        public System.Data.Entity.DbSet<WebBanSach.Models.NhaCungCap> NhaCungCaps { get; set; }
        public object DonHang { get; internal set; }
    }
}