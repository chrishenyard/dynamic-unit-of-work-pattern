using Region.Models;
using System.Linq;
using System.Web.Mvc;

namespace Region.Controllers {
	public class AdminController : Controller
    {
		private Settings _settings;

		public AdminController(Settings settings) {
			_settings = settings;
		}

		[HttpGet]
		public ActionResult ChangeSetting(string id = Settings.RegionRepository)
        {
			var model = new ChangeSettingViewModel { Key = id };
			string setting;

			if (_settings.TryGet(id, out setting)) {
				model.Setting = setting;
            }

			if (TryValidateModel(model)) {
				SetSettingsViewBag(model.Key, model.Setting);
			}
			else {
				SetSettingsViewBag(null, null);
			}

            return View(model);
        }

		[HttpPost]
		public ActionResult ChangeSetting(ChangeSettingViewModel model) {
			if (!ModelState.IsValid) {
				return View(model);
			}

			_settings.Set(model.Key, model.Setting);
			SetSettingsViewBag(model.Key, model.Setting);
			ViewBag.Status = "Saved";

            return View(model);
		}

		private void SetSettingsViewBag(string key, string value) {
			var keys = Settings.ValidSettings.Keys.ToList();
			var values = Settings.ValidSettings.Values.SelectMany(v => v).ToList();

			ViewBag.Key = new SelectList(keys);
			ViewBag.Setting = new SelectList(values, value);
		}
	}
}