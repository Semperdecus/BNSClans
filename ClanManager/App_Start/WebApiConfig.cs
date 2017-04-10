using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClanManager.App_Start
{
    public class WebApiConfig : ApiController
    {
        public static void Profile(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                "API Default",
                "{controller}/{action}/{name}",
                new { name = RouteParameter.Optional });
        }

        public static void Clan(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                "ClanLookup",
                "Clan/{id}",
                new { controller = "Clan", action = "Index", id = RouteParameter.Optional });
        }
    }
}