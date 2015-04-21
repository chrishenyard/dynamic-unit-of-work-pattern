using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Region.Repository {
	public class RegionRepository : IRegionRepository {
		public async Task<Zipcode> GetByZipcode(string zip) {
			Zipcode zipcode = null;

			using (var context = RegionContext.Create()) {
				zipcode = await (from z in context.Zipcodes
						   where z.Zip == zip
						   select z).FirstOrDefaultAsync();
			}

			return zipcode;
		}
	}
}