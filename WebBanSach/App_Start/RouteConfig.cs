using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanSach
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "Home", action = "Login" }
            );

            routes.MapRoute(
                name: "Reg",
                url: "dang-ky",
                defaults: new { controller = "Home", action = "Register" }
            );

            routes.MapRoute(
                name: "Details",
                url: "chi-tiet/{id}",
                defaults: new { controller = "Home", action = "Details"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
