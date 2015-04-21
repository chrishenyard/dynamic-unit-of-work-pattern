using Region.Repository;
using System.IO;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Region.Factories {
	public class SettingsFactory {
		private static readonly object _sync = new object();
		private static Settings _settings;

		public static Settings GetSettings(RequestContext requestContext) {
			if (_settings == null) {
				lock (_sync) {
					if (_settings == null) {
						var appDataPath = Path.Combine(requestContext.HttpContext.Request.PhysicalApplicationPath, "App_Data");
						var regionFileRepository = new RegionFileRepository(appDataPath);
						var configuration = new Configuration(regionFileRepository);
						var configurationSettings = configuration.Settings;
						_settings = new Settings(configurationSettings);
					}
				}
			}

			return _settings;
		}
	}
}