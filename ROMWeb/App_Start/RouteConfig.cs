using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ROM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{company}/{controller}/{action}/{id}",
                defaults: new { company = "COM", controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
