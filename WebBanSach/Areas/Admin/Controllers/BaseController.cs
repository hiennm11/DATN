using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Ajax.Utilities;
using WebBanSach.Models;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ses = (UserLogin)Session["TaiKhoan"];
            if (ses != null)
            {
                if (ses.MaChucVu == 0)
                {
                    filterContext.Result =
                        new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "" }));
                }
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "" }));
                base.OnActionExecuting(filterContext);
            }
            
        }
    }
}