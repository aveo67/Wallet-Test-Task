using Unity.Entities;
using WalletEcs;

namespace EcsExamples
{
	[UpdateAfter(typeof(UpdateCrystalWalletSystem))]
	internal partial class ResetCrystalsWalletSystem : ResetWalletSystem<Crystal>
	{
	}
}
