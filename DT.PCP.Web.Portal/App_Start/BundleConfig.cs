using System.Web;
using System.Web.Optimization;

namespace DT.PCP.Web.Portal
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryplugin")
                .Include("~/Scripts/jquery.placeholder.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqGrid").Include(
                       "~/Scripts/jqGrid/jquery.jqGrid-{version}.js",
                       "~/Scripts/jqGrid/jquery.jqGrid.locale-*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/global")
                .Include("~/Scripts/Pcp/Helpers.js", "~/Scripts/Pcp/global.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/core")
                //.Include("~/Content/bootstrap.css")
                //.Include("~/Content/bootstrap-responsive.css")
                .Include("~/Content/core.css"));

            bundles.Add(new StyleBundle("~/Content/themes/smoothness/css").Include(
                        "~/Content/themes/smoothness/jquery-ui-1.10.0.css"));

            bundles.Add(new StyleBundle("~/Content/themes/bootstrap/css").Include(
                       "~/Content/themes/bootstrap/jquery-ui-1.10.0.css"));
        }
    }
}