﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Storage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CatchRoute",
                url: "{.*}",
                defaults: new { controller = "View", action = "Index" }
            );

            routes.MapRoute(
                name: "View",
                url: "{controller}/{action}",
                defaults: new { controller = "View", action = "Index" }
            );

            routes.AppendTrailingSlash = true;
        }
    }
}
