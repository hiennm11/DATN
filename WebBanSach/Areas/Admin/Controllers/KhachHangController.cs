using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class KhachHangController : BaseController
    {
        WebBanSachDB db=new WebBanSachDB();
        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            return View();
        }
    }
}