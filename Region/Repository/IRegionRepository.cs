using System.Threading.Tasks;

namespace Region.Repository {
	public interface IRegionRepository {
		Task<Zipcode> GetByZipcode(string zipcode);
	}
}
