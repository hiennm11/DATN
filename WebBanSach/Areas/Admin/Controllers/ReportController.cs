using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Web.Mvc;

using WebBanSach.Models;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        WebBanSachDB db = new WebBanSachDB();

        // GET: Admin/Report
        public ActionResult Index()
        {
            return View();
        }

    

        public ActionResult ReportHangTon(string ReportType)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Areas/Admin/Reports"), "TonKho.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            List<Sach> saches = new List<Sach>();

            saches = db.Saches.ToList();

            ReportDataSource rd = new ReportDataSource("DataSetSach", saches);
            lr.DataSources.Add(rd);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

                "<DeviceInfo>" +
                "  <OutputFormat>" + ReportType + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            Response.AddHeader("content-disposition", "attachment:filename = sach_report." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);
        }

        public ActionResult ReportHD(string ReportType , int? id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Areas/Admin/Reports"), "HoaDon.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            using (WebBanSachDB db = new WebBanSachDB())
            {
                var v = from a in db.Saches
                    join b in db.ChiTietDonBans on a.MaSach equals b.MaSach
                    join c in db.DonHangBans on b.MaDonHang equals c.MaDonHang
                    where b.MaDonHang == id
                    select new{a.MaSach,a.TenSach,b.MaDonHang,b.DonGia,b.SoLuong,c.MaKhachHang,c.HoTenKH,c.SdtKH,c.DiaChiKH,c.NgayBan,c.TongTien} ;
                ReportDataSource rd = new ReportDataSource("DataSetHD", v.ToList());
                lr.DataSources.Add(rd);
            }
      
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;

            
            string deviceInfo =

                "<DeviceInfo>" +
                "  <OutputFormat>" + ReportType + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            Response.AddHeader("content-disposition", "attachment:filename = hoadon_report." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);
        }
    }
}


