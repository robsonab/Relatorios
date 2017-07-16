using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RelatoriosCongregação
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("GetPublicadoresByGrupo",
                          "test/getpublicadoresbygrupo/",
                          new { controller = "Test", action = "GetPublicadoresByGrupo" },
                          new[] { "RelatoriosCongregação.Controllers" });

            routes.MapRoute("GetTipoPublicador",
                          "test/gettipopublicador/",
                          new { controller = "Test", action = "GetTipoPublicador" },
                          new[] { "RelatoriosCongregação.Controllers" });


            routes.MapRoute("GetRelatorioPublicador",
                          "test/getrelatoriopublicador/",
                          new { controller = "Test", action = "GetRelatorioPublicador" },
                          new[] { "RelatoriosCongregação.Controllers" });
        }
    }
}
