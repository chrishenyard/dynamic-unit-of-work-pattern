using System.Web.Mvc;
using System.Web.Routing;

namespace Region {
	public class RouteConfig {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Root",
				url: "",
				defaults: new { controller = "Admin", action = "ChangeSetting" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Admin", action = "ChangeSetting", id = UrlParameter.Optional }
			);
		}
	}
}
