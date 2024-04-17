using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace WalletEcs
{
	public abstract partial class UpdateWalletSystem<TCurrencyTag> : SystemBase
		where TCurrencyTag : unmanaged, ICurrency
	{
		private EntityQuery _query;

		private EntityQuery _wallet;

		protected override void OnCreate()
		{
			EntityManager.CreateEntity(typeof(Wallet), typeof(TCurrencyTag));

			_query = new EntityQueryBuilder(Allocator.Temp)
				.WithAllRW<WalletUpdate>()
				.WithAll<TCurrencyTag>()
				.Build(this);

			_wallet = new EntityQueryBuilder(Allocator.Temp).WithAll<TCurrencyTag>().WithAllRW<Wallet>().Build(this);
		}

		[BurstDiscard]
		protected override void OnUpdate()
		{
			var wallet = _wallet.GetSingletonRW<Wallet>();

			// Работает
			using (var array = _query.ToComponentDataArray<WalletUpdate>(Allocator.Temp))
			{
				foreach (var modifier in array)
				{
					wallet.ValueRW = new Wallet() { Value = wallet.ValueRW.Value + modifier.Value };
				}
			}

			EntityManager.DestroyEntity(_query);

			// Не работает
			//Entities.ForEach((ref IncreaseData a, in TCurrencyTag b) =>
			//{

			//}).Run();

			// Не работает
			//Entities.WithAll<TCurrencyTag>().ForEach(ref IncreaseData a) =>
			//{

			//}).Run();

			// Не работает
			//foreach (var wallet in SystemAPI.Query<RefRW<IncreaseData>, TCurrencyTag>())
			//{

			//}

			// Не работает
			//foreach (var wallet in SystemAPI.Query<RefRW<IncreaseData>>().WithAll<TCurrencyTag>())
			//{

			//}
		}
	}
}
