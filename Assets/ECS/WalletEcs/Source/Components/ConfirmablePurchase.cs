using System;
using Unity.Entities;

namespace WalletEcs
{
	public struct ConfirmablePurchase : ISharedComponentData, IEquatable<ConfirmablePurchase>
	{
		public int Cost;

		public IConfirmablePurchase Purchase;

		public readonly void Confirm() => Purchase?.Confirm();

		public readonly void Reject() => Purchase?.Reject();

		public readonly bool Equals(ConfirmablePurchase other)
		{
			var hash = GetHashCode();

			if (hash == -1)
				return false;

			var res = hash == other.GetHashCode();

			return res;
		}

		public readonly override int GetHashCode()
		{
			var x = Purchase?.GetHashCode() ?? -1;

			return x;
		}
	}
}
