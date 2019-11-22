using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Security;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class UserController : DefaultController
    {
        private WebBanSachDB db = new WebBanSachDB();
        
        public ActionResult ThongTin(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        public ActionResult Update(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(string id,[Bind(Exclude = "MaKhachHang,Password")]KhachHang khachHang)
        {
           
            ModelState.Remove("MaKhachHang");
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                KhachHang kh = db.KhachHangs.FirstOrDefault(n => n.MaKhachHang == id);
                if (kh != null)
                {
                    kh.HoTen = khachHang.HoTen;
                    kh.DiaChi = khachHang.DiaChi;
                    kh.SDT = khachHang.SDT;
                    kh.Email = khachHang.Email;
                    db.SaveChanges();                    
                    UserLogin user = new UserLogin(kh);
                    Session["TaiKhoan"] = user;
                    return RedirectToAction("ThongTin", new {id});
                }
                
            }
            return View(khachHang);
        }

        public ActionResult DoiMatKhau(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
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
            

            KhachHang kh = db.KhachHangs.Find(id);
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
                    return RedirectToAction("ThongTin", new { id });
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

        public ActionResult ThongTinDonHang(string id)
        {
            var hd = db.DonHangBans.Where(n => n.MaKhachHang == id).ToList();       
            return View(hd);
        }

        public ActionResult ChiTietDonHang(int id)
        {
            var hd = db.ChiTietDonBans.Where(n => n.MaDonHang == id).ToList();
            @ViewBag.TongSL = hd.Sum(n => n.SoLuong);
            @ViewBag.TongTien = hd.Sum(n => n.DonGia * n.SoLuong);

            return View(hd);
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