using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class ChiTietDonBansController : BaseController
    {
        private WebBanSachDB db = new WebBanSachDB();

        public List<ChiTietDonBan> GetCT(int? id)
        {
            List<ChiTietDonBan> lstGioHangs = db.ChiTietDonBans.Where(n => n.MaDonHang == id).ToList();

            Session["ChiTietHD"] = lstGioHangs;
            ViewBag.MaHD = id;
            return lstGioHangs;
        }

        public ActionResult Indexd(int? id)
        {
            List<ChiTietDonBan> hd = GetCT(id);
            ViewBag.MaHD = id;
            return View(hd);
        }


        public ActionResult ThemSach(int? donhang)
        {
            var saches = db.Saches.Include(s => s.Loai).Include(s => s.NhaXuatBan).Include(s => s.NhaCungCap).Where(s=>s.TrangThai == true);
            ViewBag.MaHD = donhang;
            return View(saches.ToList());
        }

        public ActionResult ThemSPVaoDonHang(int donhang,int maSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == maSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return Error("Sản phẩm không tồn tại !!");
            }

            List<ChiTietDonBan> hd = GetCT(donhang);
            ChiTietDonBan ct = hd.Find(n => n.MaSach == maSach);
            if (ct == null)
            {
                GioHang gh = new GioHang(maSach);
                ct = new ChiTietDonBan();
                ct.MaSach = gh.MaSP;
                ct.MaDonHang = donhang;
                ct.DonGia = gh.DonGia;
                ct.SoLuong = 1;
                db.ChiTietDonBans.Add(ct);
                db.SaveChanges();

                Sach s = db.Saches.Find(ct.MaSach);
                s.SoLuong -= ct.SoLuong;
                db.SaveChanges();

                DonHangBan dhb = db.DonHangBans.Find(donhang);
                dhb.TongTien = TongTien(donhang);
                db.SaveChanges();

                ViewBag.MaHD = donhang;
                Session["ChiTietHD"] = hd;

                return RedirectToAction("Index","ChiTietDonBans", new { @id = donhang });
            }
            else
            {
                ct.SoLuong++;

                Sach s = db.Saches.Find(ct.MaSach);
                s.SoLuong -= ct.SoLuong;
                db.SaveChanges();

                DonHangBan dhb = db.DonHangBans.Find(donhang);
                dhb.TongTien = TongTien(donhang);
                db.SaveChanges();

                ViewBag.MaHD = donhang;
                Session["ChiTietHD"] = hd;
                return RedirectToAction("Index", "ChiTietDonBans", new {@id = donhang });
            }
        }

        private ActionResult Error(string v)
        {
            throw new NotImplementedException();
        }

        // GET: Admin/ChiTietDonBans
        public ActionResult Index(int? id)
        {
            List<ChiTietDonBan> hd = GetCT(id);
            TempData["ChiTietID"] = id;
            return View(hd);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            string[] sl = form.GetValues("txtSoLuong");
            int maDH = Convert.ToInt32(TempData["ChiTietID"]);
            List<ChiTietDonBan> hd = GetCT(maDH);
            for (int i = 0; i < hd.Count; i++)
            {
                hd[i].SoLuong = Convert.ToInt32(sl[i]);
                db.SaveChanges();
            }

            DonHangBan dhb = db.DonHangBans.Find(maDH);
            dhb.TongTien = TongTien(maDH);
            db.SaveChanges();

            ViewBag.MaHD = maDH;
            Session["ChiTietHD"] = hd;
            return RedirectToAction("Index","DonHangBans");

        }

        public ActionResult XoaGioHang(int donhang,int maSP)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == maSP);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<ChiTietDonBan> hd = GetCT(donhang);
            ChiTietDonBan sp = hd.SingleOrDefault(n => n.MaSach == maSP);
            if (sp != null)
            {
                db.ChiTietDonBans.Remove(sp);
                
                Sach s = db.Saches.Find(sp.MaSach);
                s.SoLuong += sp.SoLuong;
                db.SaveChanges();
            }

            if (hd.Count == 0)
            {
                Session.Remove("ChiTietHD");
                return RedirectToAction("Index", "DonHangBans");
            }

            DonHangBan dhb = db.DonHangBans.Find(donhang);
            dhb.TongTien = TongTien(donhang);
            db.SaveChanges();

            return RedirectToAction("Index", "ChiTietDonBans", new { @id = donhang });
        }


        private double TongTien(int mhd)
        {
            double TongTien = 0;
            List<ChiTietDonBan> lstGioHangs = GetCT(mhd);
            if (lstGioHangs != null)
            {
                for (int i = 0; i < lstGioHangs.Count; i++)
                {
                    TongTien += lstGioHangs[i].SoLuong * lstGioHangs[i].DonGia;
                }

                return TongTien;
            }

            return TongTien;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
