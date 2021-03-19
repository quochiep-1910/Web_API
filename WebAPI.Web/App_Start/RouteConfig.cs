using System.Web.Mvc;
using System.Web.Routing;

namespace WebAPI.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            routes.MapRoute(
                name: "Search",
                url: "tim-kiem.html",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional }
    );
            routes.MapRoute(
                name: "TagList",
                url: "tag/{TagId}.html",
                defaults: new { controller = "Product", action = "ListByTag", TagId = UrlParameter.Optional }
);
            routes.MapRoute(
                name: "Contact",
                url: "lien-he.html",
                defaults: new { controller = "Contact", action = "Index", TagId = UrlParameter.Optional }
);
            routes.MapRoute(
                name: "Page",
                url: "trang/{alias}.html",
                defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional }
   );
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap.html",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
      );
            routes.MapRoute(
               name: "Register",
               url: "dang-ki.html",
               defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
     );
            routes.MapRoute(
              name: "Cart",
              url: "gio-hang.html",
              defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional }
    );
            routes.MapRoute(
              name: "Checkout",
              url: "thanh-toan.html",
              defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional }
    );
            routes.MapRoute(
                name: "Product Category",
                url: "{alias}.pc-{id}.html",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "Product",
              url: "{alias}-{id}-p.html",
              defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "home",
             url: "trang-chu",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
       );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}