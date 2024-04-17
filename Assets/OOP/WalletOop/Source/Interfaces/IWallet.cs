using System;

namespace WalletOop
{
	public interface IWallet
	{
		Type CurrencyType { get; }

		event Action<int> QuantityChanged;

		int Quantity { get; }

		void Increase(int value);

		bool TrySpend(int value);

		void Reset();
	}

	public interface IWallet<T> : IWallet
		where T : struct, ICurrency
	{
		//
	}
}
