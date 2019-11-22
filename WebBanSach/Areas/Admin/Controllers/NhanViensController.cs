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
    public class NhanViensController : BaseController
    {
        private WebBanSachDB db = new WebBanSachDB();

        // GET: Admin/NhanViens
        public ActionResult Index()
        {
            UserLogin user = (UserLogin) Session["TaiKhoan"];
            NhanVien nv = db.NhanViens.Find(user.Username);
            var nhanViens = db.NhanViens.Include(n => n.ChucVuNhanVien).ToList();
            nhanViens.Remove(nv);
            return View(nhanViens.ToList());
        }

        // GET: Admin/NhanViens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Create
        public ActionResult Create()
        {
            ViewBag.MaChucVu = new SelectList(db.ChucVus, "MaChucVu", "TenChucVu");
            return View();
        }

        // POST: Admin/NhanViens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,Password,HoTen,Email,DiaChi,SDT,MaChucVu,TrangThai")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaChucVu = new SelectList(db.ChucVus, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChucVu = new SelectList(db.ChucVus, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,Password,HoTen,Email,DiaChi,SDT,MaChucVu,TrangThai")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChucVu = new SelectList(db.ChucVus, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
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
