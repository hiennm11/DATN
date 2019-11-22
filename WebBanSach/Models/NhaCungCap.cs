using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class NhaCungCap
    {
        [Key]
        [DisplayName("Nhà cung cấp")]
        public int MaNCC { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên nhà cung cấp")]
        [DisplayName("Nhà cung cấp")]
        public string TenNCC { get; set; }
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }
    }
}