using Unity.Collections;
using Unity.Entities;

namespace WalletEcs
{
	public abstract partial class PurchaseConfirmationSystem<TCurrencyTag> : SystemBase
		where TCurrencyTag : struct, ICurrency
	{
		private EntityQuery _query;

		private EntityQuery _wallet;

		private EndFixedStepSimulationEntityCommandBufferSystem _ecbSystem;

		protected override void OnCreate()
		{
			_query = new EntityQueryBuilder(Allocator.Temp).WithAll<TCurrencyTag>().WithAll<ConfirmablePurchase>().Build(this);
			_ecbSystem = World.GetExistingSystemManaged<EndFixedStepSimulationEntityCommandBufferSystem>();

			_wallet = new EntityQueryBuilder(Allocator.Temp).WithAll<TCurrencyTag>().WithAllRW<Wallet>().Build(this);
		}

		protected override void OnUpdate()
		{
			var wallet = _wallet.GetSingletonRW<Wallet>();

			using (var array = _query.ToEntityArray(Allocator.Temp))
			{
				foreach (var e in array)
				{
					var purchase = EntityManager.GetSharedComponentManaged<ConfirmablePurchase>(e);

					if (!purchase.Purchase.Processed && wallet.ValueRO.Value >= purchase.Cost)
					{
						wallet.ValueRW = new Wallet() { Value = wallet.ValueRO.Value - purchase.Cost };

						purchase.Confirm();
					}

					else
					{
						purchase.Reject();
					}
				}
			}

			_ecbSystem.CreateCommandBuffer().DestroyEntity(_query, EntityQueryCaptureMode.AtRecord);
		}
	}
}
