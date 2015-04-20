using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Storage.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/libs")
                .Include("~/Scripts/libs/jquery-1.10.2.min.js",
                    "~/Scripts/libs/angular.min.js",
                    "~/Scripts/libs/angular-route.min.js",
                    "~/Scripts/libs/bootstrap.min.js",
                    "~/Scripts/libs/underscore-min.js")
            );

            bundles.Add(new ScriptBundle("~/Scripts/app")
                .IncludeDirectory("~/Scripts/app", "*.js", true)
            );

            bundles.Add(new StyleBundle("~/Content")
                .Include("~/Content/bootstrap.min.css",
                    "~/Content/simple-sidebar.css",
                    "~/Content/elements.css")
                );
        }
    }
}