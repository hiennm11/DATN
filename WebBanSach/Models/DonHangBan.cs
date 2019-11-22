using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class DonHangBan
    {
        
        public string MaKhachHang { get; set; }
        [Key]
        public int MaDonHang { get; set; }
        [DisplayName("Họ tên")]
        public string HoTenKH { get; set; }
        [DisplayName("Địa chỉ")]
        public string DiaChiKH { get; set; }
        [DisplayName("Email")]
        public string EmailKH { get; set; }
        [DisplayName("Email")]
        public string SdtKH { get; set; }
        [DisplayName("Ngày bán")]
        public DateTime NgayBan { get; set; }
        [DisplayName("Tổng tiền")]
        public double TongTien { get; set; }
        [DisplayName("Trạng thái")]
        public int TrangThai { get; set; }

        public DonHangBan()
        {
            TrangThai = 0;
        }

        public virtual KhachHang KhachHang { get; set; }
        public virtual ICollection<ChiTietDonBan> ChiTietDonBans { get; set; }
    }
}