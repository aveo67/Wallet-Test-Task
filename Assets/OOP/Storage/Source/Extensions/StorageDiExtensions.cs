using System;
using UnityEngine;
using Zenject;

namespace Storage
{
	internal class StorageDiTag { }

	public static class StorageDiExtensions
	{
		public static DiContainer UseStorageService(this DiContainer container)
		{
			if (!container.HasBinding<StorageDiTag>())
			{
				container.Bind<StorageDiTag>().AsSingle();

				container.Bind<IStorageService>().To<StorageService>().AsSingle();
			}

			else
				Debug.Log("IStorageService has already bound");

			return container;
		}

		public static DiContainer BindStorage<TStorage>(this DiContainer container)
			where TStorage : IStorage
		{
			if (container.HasBinding<IStorage>())
				throw new InvalidOperationException($"Attemp to setup {typeof(TStorage).Name} as IStorage but some storage has been already bound");

			container.Bind<IStorage>().To<TStorage>().AsSingle();

			return container;
		}

		public static DiContainer UsePlayerPrefsAsStorage(this DiContainer container)
		{
			return container.BindStorage<PlayerPrefStorage>();
		}

		public static DiContainer UseFileAsStorage(this DiContainer container) 
		{
			return container.BindStorage<FileStorage>();
		}
	}
}
