﻿using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Compilify
{
    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterBundles(BundleTable.Bundles);
        }

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Compiler",
                url: "Compile",
                defaults: new { controller = "Home", action = "Compile" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        private static void RegisterBundles(BundleCollection bundles)
        {
            var css = new Bundle("~/css", new CssMinify());
            css.AddDirectory("~/assets/css", "*.css");
            bundles.Add(css);

            var js = new Bundle("~/js", new JsMinify());
            js.AddDirectory("~/assets/js", "*.js");
            bundles.Add(js);
        }
    }
}