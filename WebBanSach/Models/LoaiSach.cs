using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class LoaiSach
    {
        [Key]
        [DisplayName("Danh mục")]
        public int MaLoai { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên danh mục")]
        [DisplayName("Danh mục")]
        public string TenLoai { get; set; }
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

    }
}