using System.Web;
using System.Web.Optimization;

namespace WebBanSach
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(                        
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                "~/Content/assets/vendor/bootstrap/css/bootstrap.min.css",
                "~/Content/assets/vendor/fonts/circular-std/style.css",
                "~/Content/assets/libs/css/style.css",
                "~/Content/assets/vendor/fonts/fontawesome/css/fontawesome-all.css",
                "~/Content/assets/vendor/charts/chartist-bundle/chartist.css",
                "~/Content/assets/vendor/charts/morris-bundle/morris.css",
                "~/Content/assets/vendor/fonts/material-design-iconic-font/css/materialdesignicons.min.css",
                "~/Content/assets/vendor/charts/c3charts/c3.css",
                "~/Content/assets/vendor/fonts/flag-icon-css/flag-icon.min.css"));

            bundles.Add(new StyleBundle("~/Content/indexcss").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/swiper.min.css",
                "~/Content/css/jquery.rateyo.min.css",
                "~/Content/css/bootstrap-datepicker.min.css",
                "~/Content/css/animate.min.css",
                "~/Content/css/style.css",
                "~/Content/css/fontawesome.css",
                "~/Content/PagedList.css"));

            bundles.Add(new ScriptBundle("~/bundles/indexjs").Include(
            "~/Scripts/jquery-{version}.js",
            "~/Scripts/js/jquery.min.js",
            "~/Scripts/js/bootstrap.min.js",
            "~/Scripts/js/select2-bootstrap.js",
            "~/Scripts/js/swiper.min.js",
            "~/Scripts/js/jquery.rateyo.min.js",
            "~/Scripts/js/jquery.validation.js",
            "~/Scripts/js/bootstrap-notify.min.js",
            "~/Scripts/js/moment.min.js",
            "~/Scripts/js/bootstrap-datepicker.min.js",
            "~/Scripts/ckeditor/ckeditor.js",
            "~/Scripts/js/common.js",
            "~/Scripts/js/menu.js",
            "~/Scripts/js/app.js",
            "~/Scripts/js/modal.js",
            "~/Scripts/jquery.unobtrusive-ajax.min.js"));
        }
    }
}
