using Region.Serializers;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Region.Repository {
	public interface IRegionFileRepository {
		Dictionary<string, string> GetSettings(string fileName);
	}

	public class RegionFileRepository : IRegionFileRepository {
		private string _path;

		public RegionFileRepository(string path) {
			_path = path;
		}

		public Dictionary<string, string> GetSettings(string fileName) {
			var pathAndFileName = Path.Combine(_path, fileName);
			var text = File.ReadAllText(pathAndFileName);
			var settings = Json.Deserialize<Dictionary<string, string>>(text);
			return settings;
		}
	}
}
