using System.Web.Optimization;

namespace WebAPI.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include(
                       "~/Assets/client/js/vendor/jquery-1.12.0.min.js",
                        "~/Assets/client/js/vendor/jquery-1.12.0.min.js",
                         "~/Assets/admin/libs/jqueri-ui/jquery-ui.min.js",
                            "~/Assets/admin/libs/jquery-validation/dist/additional-methods.min.js",
                           "~/Assets/admin/libs/jquery-validation/dist/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/js/plugins").Include(

                          "~/Assets/admin/libs/mustache/mustache.js",

                            "~/Assets/client/js/popper.js",

                                "~/Assets/client/js/ajax-mail.js",
                                  "~/Assets/client/js/main.js",
                                    "~/Assets/client/js/Common.js",
                                    "~/Assets/client/js/Controller/ShoppingCart.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                         "~/Assets/client/js/bootstrap.min.js"));
            BundleTable.EnableOptimizations = true;
        }
    }
}