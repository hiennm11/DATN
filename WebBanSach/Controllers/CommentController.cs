using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class CommentController : Controller
    {
        WebBanSachDB db = new WebBanSachDB();

        public List<BinhLuan> GetBinhLuans(int maSach)
        {
            var lsBinhLuans = db.BinhLuans.Where(n => n.MaSach == maSach).Include(n => n.KhachHang).ToList();           

            return lsBinhLuans;
        }

        public ActionResult LoadBinhLuan(int maSach)
        {                        
            return PartialView("_ListBinhLuan",GetBinhLuans(maSach));
        }

        
        
    }
}