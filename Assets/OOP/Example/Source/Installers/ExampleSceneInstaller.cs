using Storage;
using UnityEngine;
using WalletOop;
using Zenject;

namespace OopExample
{
	public class ExampleSceneInstaller : MonoInstaller
	{
		[SerializeField]
		private FileStorageConfig _fileStorageConfig;

		public override void InstallBindings()
		{
			Container.Bind<FileStorageConfig>().FromInstance(_fileStorageConfig);

			Container
				.UseStorageService()
				.UsePlayerPrefsAsStorage()
				//.UseFileAsStorage() // <-- Switch to use File as a storage
				.UseWalletProxyService()
				.BindWallet<Coin>()
				.BindWallet<Crystal>();
		}
	}
}