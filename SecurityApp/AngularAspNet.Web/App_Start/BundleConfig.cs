using System.Web.Optimization;

namespace SecurityApp.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Set EnableOptimizations to false for debugging.
#if (!DEBUG)
                BundleTable.EnableOptimizations = true;
#endif
            // This is an intranet application, so do not use CDN.
            bundles.UseCdn = false;

            AddCss(bundles);
            AddJavaScript(bundles);
        }

        private static void AddCss(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/Kendo/2016.1.112/kendo.default.min.css",
                        "~/Content/Kendo/2016.1.112/kendo.bootstrap.min.css",
                        "~/Content/Kendo/2016.1.112/kendo.common.min.css",
                        "~/Content/Kendo/2016.1.112/kendo.default.mobile.min.css",
                        "~/Content/site.css",
                        "~/font-awesome/css/font-awesome.css",
                        "~/Content/loading-bar.css",
                        "~/Content/awesome-bootstrap-checkbox.css",
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-grid.css"
                ));
        }

        private static void AddJavaScript(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/App").Include(
                "~/App/App.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/common.js",
                "~/Scripts/angular.*",
                "~/Scripts/angular-*",
                "~/Scripts/Kendo/2016.1.112/kendo.all.min.js",
                "~/Scripts/loading-bar.js",
                "~/Scripts/ui-bootstrap.js",
                "~/Scripts/ui-bootstrap-tpls-2.5.0.js",
                "~/Scripts/bootstrap/tether.min.js",
                "~/Scripts/bootstrap/bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/utility").Include(
                 "~/App/Utility/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/users").Include(
                "~/App/Users/App.js",
                "~/App/Users/Components/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/applications").Include(
              "~/App/Applications/ApplicationService.js",
              "~/App/Applications/Components/*.js"));
        }
    }
}