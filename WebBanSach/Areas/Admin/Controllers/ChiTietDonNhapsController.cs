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
    public class ChiTietDonNhapsController : BaseController
    {
        private WebBanSachDB db = new WebBanSachDB();

        // GET: Admin/ChiTietDonNhaps
        public ActionResult Index(int? id)
        {
            var chiTietDonNhaps = db.ChiTietDonNhaps.Where(d => d.MaDonHang == id);

            return View(chiTietDonNhaps.ToList());
            
        }

        // GET: Admin/ChiTietDonNhaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonNhap chiTietDonNhap = db.ChiTietDonNhaps.Find(id);
            if (chiTietDonNhap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonNhap);
        }

        // GET: Admin/ChiTietDonNhaps/Create
        public ActionResult Create()
        {
            ViewBag.MaDonHang = new SelectList(db.DonHangNhaps, "MaDonHang", "MaDonHang");
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach");
            return View();
        }

        // POST: Admin/ChiTietDonNhaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonHang,MaSach,SoLuong,DonGia")] ChiTietDonNhap chiTietDonNhap)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDonNhaps.Add(chiTietDonNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDonHang = new SelectList(db.DonHangNhaps, "MaDonHang", "MaDonHang", chiTietDonNhap.MaDonHang);
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", chiTietDonNhap.MaSach);
            return View(chiTietDonNhap);
        }

        // GET: Admin/ChiTietDonNhaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonNhap chiTietDonNhap = db.ChiTietDonNhaps.Find(id);
            if (chiTietDonNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDonHang = new SelectList(db.DonHangNhaps, "MaDonHang", "MaDonHang", chiTietDonNhap.MaDonHang);
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", chiTietDonNhap.MaSach);
            return View(chiTietDonNhap);
        }

        // POST: Admin/ChiTietDonNhaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,MaSach,SoLuong,DonGia")] ChiTietDonNhap chiTietDonNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDonNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDonHang = new SelectList(db.DonHangNhaps, "MaDonHang", "MaDonHang", chiTietDonNhap.MaDonHang);
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", chiTietDonNhap.MaSach);
            return View(chiTietDonNhap);
        }

        // GET: Admin/ChiTietDonNhaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonNhap chiTietDonNhap = db.ChiTietDonNhaps.Find(id);
            if (chiTietDonNhap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonNhap);
        }

        // POST: Admin/ChiTietDonNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDonNhap chiTietDonNhap = db.ChiTietDonNhaps.Find(id);
            db.ChiTietDonNhaps.Remove(chiTietDonNhap);
            db.SaveChanges();
            return RedirectToAction("Index");
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
