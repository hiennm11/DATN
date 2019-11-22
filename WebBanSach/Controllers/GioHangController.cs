using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class GioHangController : Controller
    {
        WebBanSachDB db = new WebBanSachDB();

        public List<GioHang> GetGioHang()
        {
            List<GioHang> lstGioHangs = Session["GioHang"] as List<GioHang>;
            if (lstGioHangs == null)
            {
                lstGioHangs = new List<GioHang>();
                Session["GioHang"] = lstGioHangs;
            }

            return lstGioHangs;
        }

        public ActionResult ThemSPVaoGioHang(int maSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == maSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return Error("Sản phẩm không tồn tại !!");
            }

            List<GioHang> lsGioHangs = GetGioHang();

            GioHang sp = lsGioHangs.Find(n => n.MaSP == maSach);
            if (sp == null)
            {
                sp = new GioHang(maSach);
                lsGioHangs.Add(sp);
                Session["GioHang"] = lsGioHangs;
                ViewBag.TongSL = TongSL();                
                return Success();
            }
            else
            {
                sp.SoLuong++;
                Session["GioHang"] = lsGioHangs;
                ViewBag.TongSL = TongSL();                
                return Success();
            }
        }

        public JsonResult Success() { return Json(new { Success = true }); }
        public JsonResult Error(string message) { return Json(new { Success = false, Message = message }); }

        public ActionResult CapNhatGioHang(FormCollection form)
        {
            string[] sl = form.GetValues("txtSoLuong");
            List<GioHang> lsGioHangs = GetGioHang();

            for (int i = 0; i < lsGioHangs.Count; i++)
            {
                lsGioHangs[i].SoLuong = Convert.ToInt32(sl[i]);
            }

            Session["GioHang"] = lsGioHangs;
            return RedirectToAction("GioHang");
        }
       

        public ActionResult XoaGioHang(int maSP)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == maSP);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<GioHang> lsGioHangs = GetGioHang();
            GioHang sp = lsGioHangs.SingleOrDefault(n => n.MaSP == maSP);
            if (sp != null)
            {
                lsGioHangs.RemoveAll(n => n.MaSP == maSP);
            }

            if (lsGioHangs.Count == 0)
            {
                Session.Remove("GioHang");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<GioHang> lsGioHangs = GetGioHang();
            ViewBag.TongSL = TongSL();
            ViewBag.TongTien = TongTien();
            foreach (var item in lsGioHangs)
            {             
                if (!KiemTraSLHang(item))
                {
                    ViewBag.SPError = "Sản phẩm " + item.TenSP + " hiện không đủ " + item.SoLuong + " sản phẩm.";                   
                }
            }
            return View(lsGioHangs);
        }

        private int TongSL()
        {
            int TongSL = 0;
            List<GioHang> lstGioHangs = Session["GioHang"] as List<GioHang>;
            if (lstGioHangs != null)
            {
                TongSL = lstGioHangs.Sum(n => n.SoLuong);
            }

            return TongSL;
        }

        private double TongTien()
        {
            double TongTien = 0;
            List<GioHang> lstGioHangs = Session["GioHang"] as List<GioHang>;
            if (lstGioHangs != null)
            {
                TongTien = lstGioHangs.Sum(n => n.TongTien);
            }

            return TongTien;
        }

        public ActionResult GHPartial()
        {
            if (TongSL() == 0)
            {
                return PartialView("_GioHang");
            }

            ViewBag.TongSL = TongSL();
            ViewBag.TongTien = TongTien();
            return PartialView("_GioHang");
        }


        public ActionResult XacNhanDonHang()
        {
            return View();
        }


        public void TangSL(ChiTietDonBan ct)
        {
            Sach sach = db.Saches.Find(ct.MaSach);
            if (sach != null)
            {
                sach.LuotMua += ct.SoLuong;
                sach.SoLuong -= ct.SoLuong;
            }

            db.SaveChanges();
        }

        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
               return RedirectToAction("Login", "Home");
            }
            else
            {
                if (Session["GioHang"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                
                UserLogin tk = (UserLogin)Session["TaiKhoan"];
                List<GioHang> gh = GetGioHang();
                bool er = false;
                foreach (var item in gh)
                {
                    if(er == true) break;
                    
                    if (KiemTraSLHang(item) != true)
                    {
                        er = true;
                        ViewBag.SPError = "Sản phẩm" + item.TenSP + " hiện không đủ số lượng.";
                        return RedirectToAction("GioHang");
                    }                    
                }

                if (er == false)
                {
                    DonHangBan dhb = new DonHangBan();
                    dhb.MaKhachHang = tk.Username;
                    dhb.HoTenKH = tk.HoTen;
                    dhb.DiaChiKH = tk.DiaChi;
                    dhb.EmailKH = tk.Email;
                    dhb.SdtKH = tk.SDT;
                    dhb.NgayBan = DateTime.Now;
                    dhb.TongTien = TongTien();
                    dhb.TrangThai = 0;
                    db.DonHangBans.Add(dhb);

                    db.SaveChanges();

                    //Chi tiết đơn hàng
                    foreach (var item in gh)
                    {
                        ChiTietDonBan ct = new ChiTietDonBan();
                        ct.MaDonHang = dhb.MaDonHang;
                        ct.MaSach = item.MaSP;
                        ct.SoLuong = item.SoLuong;
                        ct.DonGia = item.DonGia;
                        db.ChiTietDonBans.Add(ct);
                        TangSL(ct);
                    }

                    db.SaveChanges();

                    Session.Remove("GioHang");

                    TempData["message"] = "Đặt hàng thành công !!!";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("GioHang");
                }
            }
                            
        }

        private bool KiemTraSLHang(GioHang gh)
        {
            Sach sach = db.Saches.Find(gh.MaSP);
            if (sach != null)
            {
                if (sach.SoLuong >= gh.SoLuong)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}