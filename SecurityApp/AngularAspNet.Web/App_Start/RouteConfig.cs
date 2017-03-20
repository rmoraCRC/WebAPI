using System.Web.Mvc;
using System.Web.Routing;

namespace SecurityApp.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Templates",
                url: "{feature}/Template/{name}",
                defaults: new { controller = "Template", action = "Render" }
            );

            routes.MapRoute(
              name: "Modals",
              url: "{feature}/Modal/{name}",
              defaults: new { controller = "Modal", action = "Render" }
            );

            // Strongly type Angular Components.
            routes.MapRoute(
                name: "Components",
                url: "{feature}/components/{name}",
                defaults: new { controller = "Ng", action = "RenderComponents" });

            ConfigUserRoutes(routes);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        public static void ConfigUserRoutes(RouteCollection routes)
        {
            // Without this, Action helper in navigation menu creates current page's url instead of /users.
            routes.MapRoute(
                name: "UsersRoot",
                url: "users",
                defaults: new { controller = "Users", action = "Index" });

            routes.MapRoute(
                name: "Users",
                url: "users/{*catchall}",
                defaults: new { controller = "Users", action = "Index" });

            routes.MapRoute(
                name: "ApplicationsRoot",
                url: "applications",
                defaults: new { controller = "Applications", action = "Index" });

            routes.MapRoute(
                name: "Applications",
                url: "applications/{*catchall}",
                defaults: new { controller = "Applications", action = "Index" });
        }
    }
}
