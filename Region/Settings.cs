using System;
using System.Collections.Generic;
using Region.Repository;
using Region.Synchronization;
using System.Threading.Tasks;

namespace Region {
	public class Settings {
		private static readonly object _sync = new object();
		public const string RegionRepository = "RegionRepository";

		public static Dictionary<string, List<string>> ValidSettings = new Dictionary<string, List<string>> {
			{
				RegionRepository,
				new List<string> { "Region.Repository.RegionCacheRepository", "Region.Repository.RegionRepository" }
			}
		};

		public event EventHandler ChangedSettingsEvent = (sender, args) => { return; };
		private Dictionary<string, string> _settings;

		public Settings(Dictionary<string, string> settings) {
			_settings = settings;
		}

		public Dictionary<string, string> All {
			get { return _settings; }
		}

		public string Get(string key) {
			if (!ValidSettings.ContainsKey(key)) throw new ApplicationException("Invalid key");

			string setting;

			lock (_sync) {
				setting = _settings[key];
            }

			return setting;
		}

		public bool TryGet(string key, out string setting) {
			var result = false;

			lock (_sync) {
				result = _settings.TryGetValue(key, out setting);
			}

			return result;
		}

		public void Set(string key, string setting) {
			if (!ValidSettings.ContainsKey(key)) throw new ApplicationException("Invalid key");
			if (!ValidSettings[key].Contains(setting)) throw new ApplicationException("Invalid value");
			var changed = false;

			lock (_sync) {
				var currentSetting = _settings[key];

				if (currentSetting != setting) {
					changed = true;
					_settings[key] = setting;
				}
			}

			if (changed) OnChangedSettingsEvent(key, setting);
        }

		private void OnChangedSettingsEvent(string key, string setting) {
			var settingsEventArgs = new SettingsEventArgs { Key = key, Setting = setting };
			ChangedSettingsEvent(this, settingsEventArgs);
        }
    }

	public class SettingsEventArgs : EventArgs {
		public string Key;
		public string Setting;
	}

	public sealed class Configuration {
		private static Dictionary<string, string> _settings = null;
		private IRegionFileRepository _regionFileRepository;

		public Configuration(IRegionFileRepository regionFileRepository) {
			_regionFileRepository = regionFileRepository;
		}

		public Dictionary<string, string> Settings {
			get {
				if (_settings == null) {
					_settings = _regionFileRepository.GetSettings("settings.json");
				}

				return _settings;
			}
		}
	}
}
