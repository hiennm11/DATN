using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanSach.Models;
using System.Web.Mvc;

namespace WebBanSach.Models
{
    public class GioHang
    {
        WebBanSachDB db = new WebBanSachDB();
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public int TrangThai { get; set; }
        public double TongTien
        {
            get { return SoLuong * DonGia; }
        }

        public GioHang(int MaSach)
        {
            MaSP = MaSach;
            Sach sach = db.Saches.Single(n => n.MaSach == MaSach);
            TenSP = sach.TenSach;
            HinhAnh = sach.Hinh;
            SoLuong = 1;
            DonGia = sach.GiaBan;
       
        }
    }
}