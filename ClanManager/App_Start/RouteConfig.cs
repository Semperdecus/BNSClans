using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClanManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "ClanLookup",
                 url: "Clan/{id}",
                 defaults: new { controller = "Clan", action = "ClanOverview", id = UrlParameter.Optional });

            routes.MapRoute(
                 name: "ProfileLookup",
                 url: "Profile/{id}",
                 defaults: new { controller = "Profile", action = "ProfileOverview", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
