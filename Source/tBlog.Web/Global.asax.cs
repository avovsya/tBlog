using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CKFinder;

namespace tBlog.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Entries",
                "page{page}",
                new { Controller = "Blog", action = "Entries" }, new { page = @"\d+" });

            routes.MapRoute(
                "Tags",
                "tags/{page}",
                new { Controller = "Blog", action = "Tags", page = 1 }, new { page = @"\d+" });

            routes.MapRoute(
                "Archive",
                "archive/",
                new { Controller = "Blog", action = "Archive" });

            routes.MapRoute(
                "Categories",
                "categories/{page}",
                new { Controller = "Blog", action = "Categories", page = 1 }, new { page = @"\d+" });

            //routes.MapRoute(
            //    "Search",
            //    "search/{term}/{page}",
            //    new { Controller = "Blog", action = "Search", page = 1 }, new { page = @"\d+" });

            routes.MapRoute(
                "Entry", // Route name
                "entry/{slug}", // URL with parameters
                new { controller = "Blog", action = "Show" } // Parameter defaults
            );

            routes.MapRoute(
                "Tag", // Route name
                "tag/{tag}/{page}", // URL with parameters
                new { controller = "Blog", action = "Tag", page = 1 }, new { page = @"\d+" } // Parameter defaults
            );

            routes.MapRoute(
                "Category", // Route name
                "category/{category}/{page}", // URL with parameters
                new { controller = "Blog", action = "Category", page = 1 }, new { page = @"\d+" } // Parameter defaults
            );

            routes.MapRoute(
                "Admin", // Route name
                "Admin/{action}", // URL with parameters
                new { controller = "Admin", action = "Administration" } // Parameter defaults
            );

            routes.MapRoute(
                "Error",
                "Error/{action}",
                new { controller = "Error", action = "Index" }
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Blog", action = "Entries", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            var fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/Content/Uploads";

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
            Application[HttpContext.Current.Request.UserHostAddress] = ex;
        }
    }
}