using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Storage
{
	internal class StorageService : IStorageService
	{
		private readonly IStorage _storage;

		private readonly HashSet<ISaveable> _savebles = new HashSet<ISaveable>();



		public StorageService(IStorage storage)
		{
			_storage = storage;
		}

		public void RegisterSaveable(ISaveable saveable)
		{
			_savebles.Add(saveable);
		}

		public async Task RestoreAsync()
		{
			foreach (var saveble in _savebles)
			{
				try
				{
					var data = await _storage.RestoreAsync(saveble.Key);

					saveble.RestoreData(data);
				}

				catch (Exception ex)
				{
					Debug.LogError($"Attempt has faild. Key {saveble.Key}\n" + ex.Message);
				}
			}
		}

		public async Task SaveAsync()
		{
			Dictionary<string, int> source = _savebles.ToDictionary(n => n.Key, n => n.GetData());

			try
			{
				await _storage.SaveAsync(source);
			}

			catch (Exception ex)
			{
				Debug.LogError("Attempt has faild\n" + ex.Message);
			}
		}
	}
}
