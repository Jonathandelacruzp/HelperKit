using System.Web;
using System.Web.Optimization;

namespace $rootnamespace$
{
    public class BundleConfig
    { 
		public static void RegisterBundles(BundleCollection bundles)
        {
		    bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/shared").Include(
                        "~/Scripts/shared/global.min.js",
                        "~/Scripts/shared/rebind.js"));
			
            BundleTable.EnableOptimizations = true;

			//Global.asax.cs
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
    }
}
