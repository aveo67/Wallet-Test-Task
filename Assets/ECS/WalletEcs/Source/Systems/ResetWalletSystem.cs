using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace WalletEcs
{
	public abstract partial class ResetWalletSystem<TCurrencyTag> : SystemBase
		where TCurrencyTag : unmanaged, IComponentData
	{
		EntityQuery _query;

		EntityQuery _wallet;

		protected override void OnCreate()
		{
			_query = new EntityQueryBuilder(Allocator.Temp)
				.WithAllRW<ResetWallet>()
				.WithAll<TCurrencyTag>()
				.Build(this);

			_wallet = new EntityQueryBuilder(Allocator.Temp).WithAll<TCurrencyTag>().WithAllRW<Wallet>().Build(this);
		}

		[BurstDiscard]
		protected override void OnUpdate()
		{
			var wallet = _wallet.GetSingletonRW<Wallet>();

			using (var array = _query.ToComponentDataArray<ResetWallet>(Allocator.Temp))
			{
				foreach (var update in array)
				{
					wallet.ValueRW = new Wallet() { Value = update.Value };

					break;
				}
			}

			EntityManager.DestroyEntity(_query);
		}
	}
}
