using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Storage
{
	internal class FileStorage : IStorage
	{
		private const string DefaultPath = ".\\Storage";

		private const string DefaultFileName = "save.txt";

		private readonly string _path;

		private readonly string _fileName;

		private readonly string _fullPath;

		private Dictionary<string, int> _restoredData;



		public FileStorage(FileStorageConfig config = null)
		{
			if (config == null)
			{
				_path = DefaultPath;
				_fileName = DefaultFileName;
			}

			else
			{
				_path = config.Path;
				_fileName = config.FileName;
			}

			_fullPath = Path.Combine(config.Path, _fileName);

			Directory.CreateDirectory(_path);
		}

		private async Task<Dictionary<string, int>> RestoreAsync()
		{
			var raw = await File.ReadAllLinesAsync(_fullPath);

			return raw.Select(n => JsonConvert.DeserializeObject<KeyValuePair<string, int>>(n)).ToDictionary(n => n.Key, n => n.Value);
		}

		public async Task SaveAsync(Dictionary<string, int> data)
		{
			string[] source = data.Select(x => JsonConvert.SerializeObject(x)).ToArray();

			await File.WriteAllLinesAsync(_fullPath, source);

			_restoredData = null;
		}

		public async Task<int> RestoreAsync(string key)
		{
			if (_restoredData == null)
				_restoredData = await RestoreAsync();

			if (!_restoredData.TryGetValue(key, out var value))
			{
				Debug.LogError($"Key {key} doesn't exist");
			}

			return value;
		}
	}
}
