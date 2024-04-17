using Storage;
using Unity.Collections;
using Unity.Entities;
using WalletEcs;

namespace EcsExamples
{
	internal class WalletSaveAgent<TCurrencyTag> : ISaveable
		where TCurrencyTag : struct, IComponentData
	{
		private readonly EntityQuery _wallet;

		public string Key => typeof(TCurrencyTag).Name;

		public WalletSaveAgent()
		{
			_wallet = new EntityQueryBuilder(Allocator.Temp)
				.WithAll<TCurrencyTag>()
				.WithAllRW<Wallet>()
				.Build(World.DefaultGameObjectInjectionWorld.EntityManager);
		}


		public int GetData()
		{
			var wallet = _wallet.GetSingleton<Wallet>();

			return wallet.Value;
		}

		public void RestoreData(int data)
		{
			var e = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(TCurrencyTag), typeof(ResetWallet));

			World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(e, new ResetWallet() { Value = data });
		}
	}
}
