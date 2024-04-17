using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Storage
{
	internal class PlayerPrefStorage : IStorage
	{
		public async Task<int> RestoreAsync(string key)
		{
			if (PlayerPrefs.HasKey(key))
			{
				return await Task.FromResult(PlayerPrefs.GetInt(key));
			}

			Debug.LogError($"Key {key} doesn't exist");

			return await Task.FromResult(0);
		}

		public async Task SaveAsync(Dictionary<string, int> data)
		{
			foreach (var pair in data)
			{
				PlayerPrefs.SetInt(pair.Key, pair.Value);

				await Task.Yield();
			}
		}
	}
}
