using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using WebBanSach.Models;
using System.Net;
using PagedList;
using System.Web.Security;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;

namespace WebBanSach.Controllers
{
    public class HomeController : Controller
    {
        private WebBanSachDB db = new WebBanSachDB();

        [HandleError]
        public ActionResult Index(int? danhmuc, int? tacgia,int? nxb,int? ncc,string search_key, int page = 1, int pagesize = 18)
        {
            if (danhmuc != null)
            {
                LoaiSach temp = db.LoaiSaches.Find(danhmuc);
                ViewBag.PageTitle = "Danh mục: " + temp.TenLoai;
                var saches = db.Saches.OrderBy(s => s.TenSach)
                    .Include(s => s.Loai).Include(s => s.NhaXuatBan)
                    .Where(s => danhmuc == null || s.MaLoai == danhmuc).ToPagedList(page, pagesize);
                ViewBag.SLTim = saches.TotalItemCount;
                ViewBag.DanhMuc = danhmuc;
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Sach", saches);
                }
                return View("Index", saches);
            }
            else if(tacgia != null)
            {
                TacGia temp = db.TacGias.Find(tacgia);
                ViewBag.PageTitle = "Tác giả: " + temp.TenTacGia;
                var saches = db.ThamGias.OrderBy(s => s.MaSach).Where(n => n.MaTacGia == tacgia).Select(n=>n.Sach).ToPagedList(page, pagesize);
                ViewBag.SLTim = saches.TotalItemCount;
                ViewBag.TacGia = tacgia;
                return View("Index", saches);
            }
            else if (nxb != null)
            {
                NhaXuatBan temp = db.NhaXuatBans.Find(nxb);
                ViewBag.PageTitle = "Nhà xuất bản: " + temp.TenNXB;
                var saches = db.Saches.OrderBy(s => s.MaSach).Where(n => n.MaNXB == nxb).ToPagedList(page, pagesize);
                ViewBag.SLTim = saches.TotalItemCount;
                ViewBag.NhaXB = nxb;
                return View("Index", saches);
            }
            else if (ncc != null)
            {
                NhaCungCap temp = db.NhaCungCaps.Find(ncc);
                ViewBag.PageTitle = "Nhà cung cấp: " + temp.TenNCC;
                var saches = db.Saches.OrderBy(s => s.MaSach).Where(n => n.MaNCC == ncc).ToPagedList(page, pagesize);
                ViewBag.SLTim = saches.TotalItemCount;
                ViewBag.NhaCC = ncc;
                return View("Index", saches);
            }
            else if (search_key != null)
            {
                ViewBag.PageTitle = "Tìm kiếm: " + search_key;


                string searchStr = ConvertToUnSign(search_key).ToLower();
                var saches = db.Saches.ToList();
                var result = saches.FindAll(
                    delegate (Sach sach)
                    {
                        if (ConvertToUnSign(sach.TenSach.ToLower()).Contains(searchStr))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }).OrderBy(n => n.TenSach).ToPagedList(page, pagesize);
                //var saches = db.Saches.OrderBy(s => s.TenSach)
                //    .Include(s => s.Loai).Include(s => s.NhaXuatBan)
                //    .Where(s => search_key == null || s.TenSach.ToLower().Contains(search_key.ToLower()))
                //    .ToPagedList(page, pagesize);
                ViewBag.SearchKey = search_key;
                ViewBag.SLTim = result.TotalItemCount;
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Sach", result);
                }
                return View("Index", result);
            }
            else
            {
                var saches = db.Saches.OrderBy(n=>n.TenSach).ToPagedList(page, pagesize);
                
                @ViewBag.PageTitle = "Tất cả sách";
                @ViewBag.SLTim = saches.TotalItemCount;
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Sach", saches);
                }
                return View("Index", saches);
            }

        }

        public ActionResult SachMoi()
        {
            var saches = db.Saches.OrderByDescending(n => n.NgaySua).ToList().Take(18);                       
            return View("_SachMoi", saches);
        }

        public ActionResult SachBanChay()
        {
            var saches = db.Saches.Where(n=>n.LuotMua > 0).OrderByDescending(n => n.LuotMua).ToList().Take(18);            
            return View("_SachBanChay", saches);
        }

        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

       
        private string ConvertToUnSign111(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D').Replace(" ", "-").Replace(" - ", "");
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

        public ActionResult SachNgauNhien()
        {
            List<Sach> saches = new List<Sach>();
            Random vl = new Random();
            for (int i = 0; i < 5; i++)
            {                
                int so = vl.Next(1, (db.Saches.Count() - 1));
                Sach sach = db.Saches.Find(so);
                if (sach != null)
                {
                    saches.Add(sach);
                }               
            }

            return PartialView("_SachNgauNhien", saches);
        }

        public ActionResult SachCungChuDe(int? danhmuc)
        {
            var saches = db.Saches.OrderBy(s => s.TenSach)
                .Include(s => s.Loai).Include(s => s.NhaXuatBan)
                .Where(s => danhmuc == null || s.MaLoai == danhmuc).ToList();
            return PartialView(saches);
        }

        [HandleError]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Page Not Found");
            }
            Sach sach = db.Saches.FirstOrDefault(n=>n.TenSachKD.Contains(id));
            if (sach == null)
            {
                throw new HttpException(404, "Page Not Found");
            }

            ViewBag.LuotDG = LuotDanhGia(sach.MaSach);
            ViewBag.SoSao = Sao(sach.MaSach) * 20;
            return View(sach);
        }

        

        public ActionResult LoaiSachP()
        {
            var danhMuc = db.LoaiSaches.Where(c => db.Saches.Select(b => b.MaLoai).Contains(c.MaLoai));
            return PartialView("_DanhMuc",danhMuc.ToList());
        }

        public ActionResult DanhmucFooter()
        {
            var danhMuc = db.LoaiSaches.Take(9);
            return PartialView("_DanhmucFooter", danhMuc.ToList());
        }

        public ActionResult TacGiaFooter()
        {            
            var tacgia = db.ThamGias.Include(n => n.TacGia).Include(n => n.Sach).OrderByDescending(n=>n.Sach.LuotMua).Take(9);
            return PartialView("_TacGiaFooter", tacgia.ToList());
        }

        public ActionResult NXBFooter()
        {
            var nxb = db.NhaXuatBans.Take(9);
            return PartialView("_NXBFooter", nxb.ToList());
        }

        // Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "MaKhachHang,HoTen,Password,Email,DiaChi,SDT,TrangThai")] KhachHang taiKhoan)
        {
            KhachHang kh = db.KhachHangs.Find(taiKhoan.MaKhachHang);
            if (kh != null)
            {
                ViewBag.Alert = "Tài khoản đã tồn tại";
                return View(taiKhoan);
            }
            if (ModelState.IsValid)
            {
                taiKhoan.TrangThai = true;
                taiKhoan.Password = EncodeMD5(taiKhoan.Password);
                db.KhachHangs.Add(taiKhoan);
                db.SaveChanges();

                UserLogin user = new UserLogin(taiKhoan);
                Session["TaiKhoan"] = user;
                return RedirectToAction("Index", "Home");
            }

            return View(taiKhoan);
        }

        // Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["TaiKhoan"] != null)
            {
                Session.RemoveAll();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string userName = form["txtUserName"].ToString();
            string password = form["txtPassword"].ToString();
            password = EncodeMD5(password);

            KhachHang tk = db.KhachHangs.FirstOrDefault(n => n.MaKhachHang == userName && n.Password == password && n.TrangThai==true);
            if (tk == null)
            {
                NhanVien nv = db.NhanViens.FirstOrDefault(n => n.MaNV == userName && n.Password == password && n.TrangThai == true);
                if (nv != null)
                {
                    UserLogin user = new UserLogin(nv);
                    Session["TaiKhoan"] = user;
                    return RedirectToAction("Index", "AdHome", new { area = "Admin" });
                }
                else
                {
                    ViewBag.Alert = "Tài khoản hoặc mật khảu không đúng.";
                    return View();
                }
            }
            else
            {
                UserLogin user = new UserLogin(tk);
                Session["TaiKhoan"] = user;
                return RedirectToAction("Index", "Home");
            }

        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult TacGia(int maSach)
        {
            var tacgia = db.ThamGias.Where(n => n.MaSach == maSach).Include(n => n.TacGia);
            return PartialView(tacgia);
        }

        [HttpPost]
        public ActionResult Details(int id, [Bind(Include = "NoiDung,SoSao")] BinhLuan bl)
        {
            bl.MaSach = id;
            UserLogin user = (UserLogin)Session["TaiKhoan"];
            if (user != null || user.ToString() != "")
            {
                var kt = db.BinhLuans.Where(n => n.MaSach == id && n.MaKhachHang == user.Username && n.SoSao > 0).ToList();
                Sach sach = db.Saches.Find(id);

                if (ModelState.IsValid)
                {
                    if (bl.NoiDung != null || bl.NoiDung != "")
                    {
                        bl.NgayBinhLuan = DateTime.Now;
                        bl.MaKhachHang = user.Username;
                        bl.TrangThai = true;
                        if (kt.Count > 0)
                        {
                            bl.SoSao = 0;
                        }
                        db.BinhLuans.Add(bl);
                        db.SaveChanges();
                        sach.SoSao = Sao(id);
                        db.SaveChanges();

                    }

                    return RedirectToAction("Details", "Home",new {@id = sach.TenSachKD});
                }

            }

            return RedirectToAction("Details", id);
        }

        public ActionResult TGNgauNhien()
        {
            Random vl = new Random();
            int so = vl.Next(1, db.TacGias.Count());           
            TacGia tg = db.TacGias.Find(so);
            if (tg == null)
            {
                so = 1;
                tg = db.TacGias.Find(so);
            }
            var saches = db.ThamGias.OrderBy(s => s.Sach.SoSao).Where(n => n.MaTacGia == so).Select(n => n.Sach).ToList();

            ViewBag.SoSachTG = saches.Count;
            return PartialView("_TGNgauNhien", tg);
        }

        public ActionResult SachTGNgauNhien(int tg)
        {
            var saches = db.ThamGias.OrderBy(s => s.Sach.SoSao).Where(n => n.MaTacGia == tg).Select(n => n.Sach).Take(4).ToList();
            return PartialView("_SachTGNgauNhien", saches);
        }

        private double Sao(int? maSach)
        {
            double avg = 0;
            var temp = db.BinhLuans.Where(b => b.MaSach == maSach && b.SoSao > 0).ToList();
            if (temp.Count > 0)
            {
                avg = temp.Average(b => b.SoSao);
            }            
            return avg;
        }

        private int LuotDanhGia(int? maSach)
        {           
            var temp = db.BinhLuans.Where(b => b.MaSach == maSach && b.SoSao > 0).ToList();            
            return temp.Count;
        }

        private string EncodeMD5(string pass)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);

            bs = md5.ComputeHash(bs);

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)

            {

                s.Append(b.ToString("x1").ToLower());

            }

            pass = s.ToString();

            return pass;
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