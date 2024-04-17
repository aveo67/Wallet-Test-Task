using Storage;
using UnityEngine;
using Zenject;

namespace OopExample
{
	public class StorageUIController : MonoBehaviour
	{
		private IStorageService _storageService;

		[Inject]
		private void Construct(IStorageService storageService)
		{
			_storageService = storageService;
		}

		public async void SaveAll()
		{
			await _storageService.SaveAsync();
		}

		public async void Restore()
		{
			await _storageService.RestoreAsync();
		}
	}
}
