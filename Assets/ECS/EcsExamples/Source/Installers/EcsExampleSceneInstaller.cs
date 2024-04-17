using Storage;
using UnityEngine;
using Zenject;

namespace EcsExamples
{
	public class EcsExampleSceneInstaller : MonoInstaller
	{
		[SerializeField]
		private FileStorageConfig _fileStorageConfig;

		public override void InstallBindings()
		{
			Container.Bind<FileStorageConfig>().FromInstance(_fileStorageConfig);

			Container
				.UsePlayerPrefsAsStorage()
				//.UseFileAsStorage() // <-- Switch to use File as a storage
				.UseStorageService();
		}
	}
}

