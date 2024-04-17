using Unity.Entities;

namespace WalletEcs
{
	public struct Wallet : IComponentData
	{
		private int _value;

		public int Value
		{
			get => _value;

			internal set => _value = value;
		}
	}
}
