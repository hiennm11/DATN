using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public int MaChucVu { get; set; }
        public bool TrangThai { get; set; }

        public UserLogin(NhanVien nv)
        {
            Username = nv.MaNV;
            Password = nv.Password;
            HoTen = nv.HoTen;
            Email = nv.Email;
            DiaChi = nv.DiaChi;
            SDT = nv.SDT;
            MaChucVu = nv.MaChucVu;
            TrangThai = nv.TrangThai;
        }

        public UserLogin(KhachHang nv)
        {
            Username = nv.MaKhachHang;
            Password = nv.Password;
            HoTen = nv.HoTen;
            Email = nv.Email;
            DiaChi = nv.DiaChi;
            SDT = nv.SDT;
            MaChucVu = 0;
            TrangThai = nv.TrangThai;
        }
    }
}