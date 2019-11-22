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
    public class DonHangNhapsController : BaseController
    {
        private WebBanSachDB db = new WebBanSachDB();

        // GET: Admin/DonHangNhaps
        public ActionResult Index()
        {
            

            return View(db.DonHangNhaps.ToList());
        }

        // GET: Admin/DonHangNhaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHangNhap donHangNhap = db.DonHangNhaps.Find(id);
            if (donHangNhap == null)
            {
                return HttpNotFound();
            }
            return View(donHangNhap);
        }

        // GET: Admin/DonHangNhaps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DonHangNhaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonHang,NgayNhap,TongTien")] DonHangNhap donHangNhap)
        {
            if (ModelState.IsValid)
            {
                db.DonHangNhaps.Add(donHangNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donHangNhap);
        }

        // GET: Admin/DonHangNhaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHangNhap donHangNhap = db.DonHangNhaps.Find(id);
            if (donHangNhap == null)
            {
                return HttpNotFound();
            }
            return View(donHangNhap);
        }
        public List<Sach> GetSaches()
        {
            List<Sach> lsSaches = Session["Sach"] as List<Sach>;
            if (lsSaches == null)
            {
                lsSaches = new List<Sach>();
                Session["Sach"] = lsSaches;
            }

            return lsSaches;
        }

        // POST: Admin/DonHangNhaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,NgayNhap,TongTien")] DonHangNhap donHangNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHangNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donHangNhap);
        }

        // GET: Admin/DonHangNhaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHangNhap donHangNhap = db.DonHangNhaps.Find(id);
            if (donHangNhap == null)
            {
                return HttpNotFound();
            }
            return View(donHangNhap);
        }

        // POST: Admin/DonHangNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHangNhap donHangNhap = db.DonHangNhaps.Find(id);
            db.DonHangNhaps.Remove(donHangNhap);
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
