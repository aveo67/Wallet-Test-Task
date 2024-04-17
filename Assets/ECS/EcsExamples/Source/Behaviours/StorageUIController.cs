using Storage;
using UnityEngine;
using Zenject;

namespace EcsExamples
{
	public class StorageUIController : MonoBehaviour
	{
		private IStorageService _storageService;

		[Inject]
		private void Construct(IStorageService storageService)
		{
			_storageService = storageService;

			_storageService.RegisterSaveable(new WalletSaveAgent<Coin>());
			_storageService.RegisterSaveable(new WalletSaveAgent<Crystal>());
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
