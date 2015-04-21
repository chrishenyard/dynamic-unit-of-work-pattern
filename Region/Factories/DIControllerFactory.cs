using Region.Controllers;
using Region.Factories;
using System.Web.Mvc;
using System.Web.Routing;

namespace Region {
	public class DIControllerFactory : DefaultControllerFactory {
		public override IController CreateController(RequestContext requestContext, string controllerName) {
			var settings = SettingsFactory.GetSettings(requestContext);

			if (controllerName == "Admin") {
				return new AdminController(settings);
			}

			if (controllerName == "Region") {
				var regionRepository = RegionRepositoryFactory.GetRegionRepository(settings);
				return new RegionController(regionRepository);
			}

			return base.CreateController(requestContext, controllerName);
		}
	}
}
