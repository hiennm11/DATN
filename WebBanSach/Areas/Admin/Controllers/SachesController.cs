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
    public class SachesController : BaseController
    {
        private WebBanSachDB db = new WebBanSachDB();

        // GET: Admin/Saches
        public ActionResult Index()
        {
            var saches = db.Saches.Include(s => s.Loai).Include(s => s.NhaXuatBan).Include(s=>s.NhaCungCap);
            return View(saches.ToList());
        }
        // GET: Admin/Saches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }
        // GET: Admin/Saches/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiSaches, "MaLoai", "TenLoai");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            return View();
        }
        [HttpPost]
        // POST: Admin/Saches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        public ActionResult Create([Bind(Include = "MaSach,TenSach,MaLoai,MaNXB,MaNCC,MoTa,Hinh,SoTrang,GiaBan,SoLuong,NgayTao,NgaySua,TrangThai")]
            Sach sach, HttpPostedFileBase file)
        {
            string pic = "";
            if (ModelState.IsValid)
            {
                sach.NgayTao = DateTime.Now;
                sach.NgaySua = DateTime.Now;
                db.Saches.Add(sach);
               db.SaveChanges();
                if (file != null)
                {
                    pic =  sach.MaSach.ToString() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("/Images"), pic);
                    sach.Hinh = "/Images/" + pic;
                    file.SaveAs(path);
                }
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSaches, "MaLoai", "TenLoai", sach.MaLoai);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "MaNXB", sach.MaNXB);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC",sach.MaNCC);
            return View(sach);
        }
        // GET: Admin/Saches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSaches, "MaLoai", "TenLoai", sach.MaLoai);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB", sach.MaNXB);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sach.MaNCC);
            return View(sach);
        }

        // POST: Admin/Saches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,MaLoai,MaNXB,MaNCC,MoTa,Hinh,SoTrang,GiaBan,NgayTao,NgaySua,SoLuong,TrangThai")] Sach sach,
            HttpPostedFileBase file)
        {
            string pic = "";
            if (ModelState.IsValid)
            {
                sach.NgayTao = DateTime.Now;
                sach.NgaySua = DateTime.Now;
                if (file != null)
                {
                    pic = sach.MaSach.ToString() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("/Images"), pic);
                    sach.Hinh = "/Images/" + pic;
                    file.SaveAs(path);
                }

                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSaches, "MaLoai", "TenLoai", sach.MaLoai);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB", sach.MaNXB);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sach.MaNCC);
            return View(sach);
        }

        // GET: Admin/Saches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: Admin/Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
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
