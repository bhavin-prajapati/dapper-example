using System.Web;
using System.Web.Optimization;

namespace DapperExample
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            string cndHost = "cdn";

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.9.1.js"
                        ));
            
            bundles.Add(new ScriptBundle("~/Scripts/val").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/validation.js"
                        ));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                        "~/Scripts/globalize/globalize.js",
                        "~/Scripts/globalize/cultures/globalize.culture." + System.Globalization.CultureInfo.CurrentCulture.ToString() + ".js",
                        "~/Scripts/bootstrap*",
                        "~/Scripts/filebutton.js",
                        "~/Scripts/globalize-datepicker.js"
                        ));

            bundles.Add(new ScriptBundle("~/Scripts/md").Include(
                        "~/Scripts/MarkdownDeepLib.min.js",
                        "~/Scripts/markdown.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                        "~/Content/themes/bootstrap/bootstrap.css"));

            bundles.IgnoreList.Clear();
        }
    }
}
