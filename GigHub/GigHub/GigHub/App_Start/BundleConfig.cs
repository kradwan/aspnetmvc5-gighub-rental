using System.Web;
using System.Web.Optimization;

namespace GigHub
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/services/AttandancesService.js",
                "~/Scripts/app/controllers/GigController.js",
                "~/Scripts/app/services/FollowingService.js",
                "~/Scripts/app/controllers/FollowingController.js",
                "~/Scripts/app/app.js"
                ));

            //bundle: lib - bundles third party libraries
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootbox.min.js", //lib for alerts
                        "~/Scripts/underscore-min.js",
                        "~/Scripts/moment.js"));

            //jqueryval is about 50-70kB and it's only needed for form validation so we keep it in a seperate file
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/animate.css" //css lib for animations
                      ));
        }
    }
}
