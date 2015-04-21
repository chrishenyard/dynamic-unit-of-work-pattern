using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Region.Models {
	public class ChangeSettingViewModel : IValidatableObject {
		public string Key { get; set; }
		public string Setting { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
			var validSettings = Settings.ValidSettings;

			if (!validSettings.ContainsKey(Key)) {
				yield return new ValidationResult("Invalid key", new[] { "Key" });
			}
			else {
				if (!validSettings[Key].Contains(Setting))
					yield return new ValidationResult("Invalid setting", new[] { "Setting" });
			}
		}
	}
}
