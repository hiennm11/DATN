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
    public class ThamGiasController : BaseController
    {
        private WebBanSachDB db = new WebBanSachDB();

        // GET: Admin/ThamGias
        public ActionResult Index()
        {
            var thamGias = db.ThamGias.Include(t => t.Sach).Include(t => t.TacGia);
            return View(thamGias.ToList());
        }

        // GET: Admin/ThamGias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThamGia thamGia = db.ThamGias.Find(id);
            if (thamGia == null)
            {
                return HttpNotFound();
            }
            return View(thamGia);
        }

        // GET: Admin/ThamGias/Create
        public ActionResult Create()
        {
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach");
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia");
            return View();
        }

        // POST: Admin/ThamGias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSach,MaTacGia,VaiTro")] ThamGia thamGia)
        {
            if (ModelState.IsValid)
            {
                db.ThamGias.Add(thamGia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", thamGia.MaSach);
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia", thamGia.MaTacGia);
            return View(thamGia);
        }

        // GET: Admin/ThamGias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThamGia thamGia = db.ThamGias.Find(id);
            if (thamGia == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", thamGia.MaSach);
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia", thamGia.MaTacGia);
            return View(thamGia);
        }

        // POST: Admin/ThamGias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,MaTacGia,VaiTro")] ThamGia thamGia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thamGia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", thamGia.MaSach);
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia", thamGia.MaTacGia);
            return View(thamGia);
        }

        // GET: Admin/ThamGias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThamGia thamGia = db.ThamGias.Find(id);
            if (thamGia == null)
            {
                return HttpNotFound();
            }
            return View(thamGia);
        }

        // POST: Admin/ThamGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThamGia thamGia = db.ThamGias.Find(id);
            db.ThamGias.Remove(thamGia);
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
