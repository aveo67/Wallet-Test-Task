using Unity.Entities;
using UnityEngine;
using WalletEcs;

namespace EcsExamples
{
	public class WalletUIController : MonoBehaviour
	{
		public void AddOneCoin()
		{
			var e = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(Coin), typeof(WalletUpdate));

			World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(e, new WalletUpdate() { Value = 1 });
		}

		public void AddOneCrystal()
		{
			var e = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(Crystal), typeof(WalletUpdate));

			World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(e, new WalletUpdate() { Value = 1 });
		}

		public void SpendThreeCoins()
		{
			var e = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(ConfirmablePurchase), typeof(Coin));

			World
				.DefaultGameObjectInjectionWorld
				.EntityManager
				.SetSharedComponentManaged(e, new ConfirmablePurchase() { Cost = 3, Purchase = new PurchaseConfirmation(
					() =>
					{
						var e = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(Crystal), typeof(WalletUpdate));

						World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(e, new WalletUpdate() { Value = 3 });
					},
					() =>
					{
						Debug.Log("Not enough coins");
					}
					) }); 
		}

		public void SpendThreeCrystals()
		{
			var e = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(ConfirmablePurchase), typeof(Crystal));

			World
				.DefaultGameObjectInjectionWorld
				.EntityManager
				.SetSharedComponentManaged(e, new ConfirmablePurchase()
				{
					Cost = 3,
					Purchase = new PurchaseConfirmation(
					() =>
					{
						var e = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(Coin), typeof(WalletUpdate));

						World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(e, new WalletUpdate() { Value = 3 });
					},
					() =>
					{
						Debug.Log("Not enough crystals");
					}
					)
				});
		}

		public void ResetCoin()
		{
			World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(Coin), typeof(ResetWallet));
		}

		public void ResetCrystal()
		{
			World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(Crystal), typeof(ResetWallet));
		}
	}
}
