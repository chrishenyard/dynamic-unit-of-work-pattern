using Region.Repository;
using System.ComponentModel.DataAnnotations;

namespace Region.Models {
	public class RegionViewModel {

		public Zipcode Zipcode { get; set; }

		public string Id { get; set; }

		public string RegionRepositoryType { get; set; }
	}
}
