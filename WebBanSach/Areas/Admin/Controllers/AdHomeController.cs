using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebBanSach.Models;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class AdHomeController : BaseController
    {
        private WebBanSachDB db = new WebBanSachDB();

        
        public ActionResult nvPartial()
        {
            var nhanViens = db.NhanViens.Include(n => n.ChucVuNhanVien);
            return PartialView("_NhanVien",nhanViens.ToList());
        }
       
        public ActionResult Index()
        {
            List<double> LoiNhuan = new List<double>();
            var thang = db.DonHangBans.Select(n => n.NgayBan.Month).Distinct();
            var temp = db.DonHangBans.Where(n=>n.TrangThai == 2).GroupBy(n => n.NgayBan.Month)
                .Select(o => new
                {
                    Thang = o.Key,
                    tien = o.Sum(c => c.TongTien)
                });
            foreach (var item in temp)
            {                
                LoiNhuan.Add((item.tien));
            }

            double max = LoiNhuan.Max();

            ViewBag.MaxY = max;
            ViewBag.Thang = thang;
            ViewBag.TongNhap = LoiNhuan.ToList();
            return View();
        }

        private double LoiNhuanThang(int thang)
        {
            return TienXuatTheoThang(thang) - TienNhapTheoThang(thang);
        }

        private double TienNhapTheoThang(int thang)
        {
            double tong = 0;
            var Lst = db.DonHangNhaps.Where(n => n.NgayNhap.Month == thang);
            tong = Lst.Sum(n => n.TongTien);
            return tong;
        }

        private double TienXuatTheoThang(int thang)
        {
            double tong = 0;
            var Lst = db.DonHangBans.Where(n => n.NgayBan.Month == thang && n.TrangThai == 2);
            foreach (var item in Lst)
            {
                tong += item.TongTien;
            }
            return tong;
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home", new {area = ""});
        }

        public ActionResult DoiMatKhau(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien khachHang = db.NhanViens.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMatKhau(string id, FormCollection form)
        {
            string mkCu = form["txtMatKhauCu"];
            string mkMoi = form["txtMatKhauMoi"];
            string Nhaplai = form["txtNhapLaiMK"];
            mkCu = EncodeMD5(mkCu);


            NhanVien kh = db.NhanViens.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }

            if (kh.Password == mkCu)
            {
                if (mkMoi == Nhaplai)
                {
                    kh.Password = EncodeMD5(mkMoi);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Alert = "Nhập lại mật khẩu không đúng.";
                    return View(kh);
                }
            }
            ViewBag.Alert = "Mật khẩu cũ không chính xác.";
            return View(kh);

        }

        private string EncodeMD5(string pass)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);

            bs = md5.ComputeHash(bs);

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)

            {

                s.Append(b.ToString("x1").ToLower());

            }

            pass = s.ToString();

            return pass;
        }
    }
}