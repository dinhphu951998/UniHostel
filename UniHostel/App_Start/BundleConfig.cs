using System.Web;
using System.Web.Optimization;

namespace UniHostel
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bill.js",
                      "~/Scripts/DatePickerJQuery.js",
                      "~/Scripts/userpage.js",
                      "~/Scripts/alert/sweetalert2.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bill.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-datepicker3.min.css",
                      "~/Content/bootstrap-datepicker.standalone.min.css",
                      "~/Content/bootstrap-datepicker3.standalone.min.css",
                      "~/Content/bootstrap-datepicker.min.css",
                      "~/Scripts/alert/sweetalert2.css",
                      "~/url/https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css",
                      "~/Content/site.css"));
        }
    }
}
