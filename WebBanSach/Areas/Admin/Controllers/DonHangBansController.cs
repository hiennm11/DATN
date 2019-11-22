using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class DonHangBansController : BaseController
    {
        private WebBanSachDB db = new WebBanSachDB();
        
        // GET: Admin/DonHangBans
        public ActionResult Index()
        { 
            return View(db.DonHangBans.ToList());
        }

        public ActionResult GetGiaoHangThatBai(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHangBan donHangBan = db.DonHangBans.Find(id);
            if (donHangBan.TrangThai == 0)
            {
                donHangBan.TrangThai = 3;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "DonHangBans");
        }
        public ActionResult GetDuyetTrangThaiDonHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHangBan donHangBan = db.DonHangBans.Find(id);
            if (donHangBan.TrangThai == 0)
            {
                donHangBan.TrangThai = 1;
            }
            else if (donHangBan.TrangThai == 1)
            {
                donHangBan.TrangThai = 2;
            } 
            db.SaveChanges();
            return RedirectToAction("Index", "DonHangBans");
        }
        // GET: Admin/DonHangBans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHangBan donHangBan = db.DonHangBans.Find(id);
            if (donHangBan == null)
            {
                return HttpNotFound();
            }
           
            return View(donHangBan);
        }
        // GET: Admin/DonHangBans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHangBan donHangBan = db.DonHangBans.Find(id);
            if (donHangBan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "Password", donHangBan.MaKhachHang);
            return View(donHangBan);
        }

        // POST: Admin/DonHangBans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,MaKhachHang,HoTenKH,DiaChiKH,EmailKH,SdtKH,NgayBan,TongTien,TrangThai")] DonHangBan donHangBan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHangBan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "Password", donHangBan.MaKhachHang);
            return View(donHangBan);
        }

        // GET: Admin/DonHangBans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHangBan donHangBan = db.DonHangBans.Find(id);
            if (donHangBan == null)
            {
                return HttpNotFound();
            }
            return View(donHangBan);
        }

        // POST: Admin/DonHangBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHangBan donHangBan = db.DonHangBans.Find(id);
            db.DonHangBans.Remove(donHangBan);
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
