﻿ using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class KhachHang
    {
        [Key]
        [Required(ErrorMessage = "Bạn chưa nhập username")]
        [DisplayName("User name")]
        public string MaKhachHang { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập họ tên")]
        [DisplayName("Họ và tên")]
        public string HoTen { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        [DisplayName("Điện thoại")]
        public string SDT { get; set; }
        public bool TrangThai { get; set; }

        
    }
}