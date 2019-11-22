 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class TaiKhoan
    {
        [Key]
        [Required(ErrorMessage = "Bạn chưa nhập username")]
        [DisplayName("User name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập password")]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public bool TrangThai { get; set; }
        public bool IsAdmin { get; set; }
    }
}