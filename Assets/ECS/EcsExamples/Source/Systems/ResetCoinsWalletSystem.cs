using Unity.Entities;
using WalletEcs;

namespace EcsExamples
{
	[UpdateAfter(typeof(UpdateCoinsWalletSystem))]
	internal partial class ResetCoinsWalletSystem : ResetWalletSystem<Coin>
	{
	}
}
