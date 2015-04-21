using Region.Models;
using Region.Repository;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Region.Controllers {
	public class RegionController : Controller {
		private IRegionRepository _regionRepository;

		public RegionController(IRegionRepository regionRepository) {
			_regionRepository = regionRepository;
		}

		public async Task<ActionResult> GetByZipcode(string id) {
			var region = await _regionRepository.GetByZipcode(id);
			var model = new RegionViewModel { Zipcode = region ?? new Zipcode(), RegionRepositoryType = _regionRepository.ToString() };

			if (region == null) {
				ModelState.AddModelError("Zipcode", "Invalid zipcode");
			}

			return View("Zipcode", model);
		}
    }
}