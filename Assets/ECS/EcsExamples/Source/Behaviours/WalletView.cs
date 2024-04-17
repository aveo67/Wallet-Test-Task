using TMPro;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using WalletEcs;

namespace EcsExamples
{
	internal abstract class WalletView<TCurrency> : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _label;

		private EntityQuery _wallet;

		private void Awake()
		{
			_wallet = new EntityQueryBuilder(Allocator.Temp)
				.WithAll<TCurrency>()
				.WithAllRW<Wallet>()
				.Build(World.DefaultGameObjectInjectionWorld.EntityManager);
		}

		private void Update()
		{
			var wallet = _wallet.GetSingleton<Wallet>();

			_label.text = wallet.Value.ToString();
		}
	}
}
