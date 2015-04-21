using Region.Caching;
using Region.Synchronization;
using System.Reflection;
using System.Threading.Tasks;

namespace Region.Repository {
	public class RegionCacheRepository : IRegionRepository {
		private ICache _cache;
		private IRegionRepository _regionRepository;
        private MethodBase _methodBase = MethodBase.GetCurrentMethod();
		private static readonly AsyncLock _asyncLock = new AsyncLock();

		public RegionCacheRepository(IRegionRepository regionRepository, ICache cache) {
			_regionRepository = regionRepository;
			_cache = cache;
		}

		public async Task<Zipcode> GetByZipcode(string zip) {
			var cacheKey = GetCacheKey(_methodBase.Name, zip);
			var zipcode = _cache.Get<Zipcode>(cacheKey);
			var expiration = 5;

            if (zipcode == null) {
				using (var releaser = await _asyncLock.LockAsync()) {
					zipcode = _cache.Get<Zipcode>(cacheKey);

					if (zipcode == null) {
						zipcode = await _regionRepository.GetByZipcode(zip);
					}
				}

				if (zipcode != null) {
					_cache.Set<Zipcode>(cacheKey, zipcode, expiration);
				}
			}

			return zipcode;
		}

		private string GetCacheKey(string methodName, string zip) {
			return _methodBase.DeclaringType.FullName + "." + methodName + "." + zip;
		}
	}
}
