using System.Web.Optimization;
using Affecto.AngularJS.TypeScript.Base;
using Affecto.AngularJS.TypeScript.BusyIndication;
using Affecto.AngularJS.TypeScript.ExceptionHandling;
using Affecto.AngularJS.TypeScript.Login;

namespace OrganizationRegister.AngularApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-2.1.3.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate.js").Include("~/Scripts/jquery.validate.unobtrusive.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-cookies.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-resource.js"));

            bundles.Add(new ScriptBundle("~/bundles/angulartranslate").Include(
                "~/Scripts/angular-translate.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular-unsavedChanges").Include(
                "~/Scripts/angular-unsavedChanges/unsavedChanges.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/treecontrol").Include(
                "~/Scripts/angular-tree-control.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .IncludeDirectory("~/Scripts/OrganizationRegister/", "*.js")
                .IncludeDirectory("~/Scripts/affecto/", "*.js"));

            bundles.Add(BaseBundle.Create("~/bundles/base"));
            bundles.Add(BusyIndicationBundle.CreateScriptBundle("~/bundles/busyindication"));
            bundles.Add(ExceptionHandlingBundle.CreateScriptBundle("~/bundles/exceptionhandling"));
            bundles.Add(LoginBundle.CreateScriptBundle("~/bundles/login"));

            // order must be correct here, classes used, extended or implemented by other classes must be included first
            bundles.Add(new ScriptBundle("~/bundles/app")
                .IncludeDirectory("~/App/Constants/", "*.js")
                .IncludeDirectory("~/App/Exceptions/", "*.js")
                .Include("~/App/Models/OrganizationName.js")
                .IncludeDirectory("~/App/Models/", "*.js")
                .IncludeDirectory("~/App/Mapping/", "*.js")
                .IncludeDirectory("~/App/Services/", "*.js")
                .IncludeDirectory("~/App/Directives/IgnoreDirtyFormField/", "*.js")
                .IncludeDirectory("~/App/Directives/HelpPopup/", "*.js")
                .Include("~/App/Controllers/IController.js")
                .Include("~/App/Controllers/IViewScope.js")
                .IncludeDirectory("~/App/Controllers/", "*.js")
                .Include("~/App/App.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 BusyIndicationBundle.GetStyleBundleVirtualPath(),
                 "~/Content/screen.css",
                 "~/Content/temporary.css"));

#if(!DEBUG)
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
