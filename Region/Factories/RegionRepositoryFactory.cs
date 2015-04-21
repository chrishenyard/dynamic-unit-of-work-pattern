using System.Collections.Generic;
using Region.Caching;
using Region.Repository;

namespace Region.Factories {
	public class RegionRepositoryFactory {
		private static Dictionary<string, IRegionRepository> _factory = null;
		private static object _sync = new object();

		public static IRegionRepository GetRegionRepository(Settings settings) {
			if (_factory == null) {
				lock (_sync) {
					if (_factory == null) {
						var regionRepository = new RegionRepository();
						var cache = new Cache();
						_factory = new Dictionary<string, IRegionRepository> {
							{ "Region.Repository.RegionCacheRepository", new RegionCacheRepository(regionRepository, cache) },
							{ "Region.Repository.RegionRepository", regionRepository }
						};
					}
				}
			}

			var regionRepositoryValue = settings.Get(Settings.RegionRepository);
			return _factory[regionRepositoryValue];
		}
	}
}