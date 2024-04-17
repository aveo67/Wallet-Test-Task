using Storage;
using System;

namespace WalletOop
{
	internal abstract class Wallet : IWallet
	{
		public event Action<int> QuantityChanged;

		private int _quantity;

		public abstract Type CurrencyType { get; }

		public int Quantity
		{
			get => _quantity;

			internal set
			{
				_quantity = value;
				QuantityChanged?.Invoke(Quantity);
			}
		}

		public Wallet(IStorageService storageService)
		{
			storageService.RegisterSaveable(new WalletSaveAgent(this));
		}

		internal void Set(int value) => Quantity = value;

		public void Increase(int value)
		{
			if (value <= 0)
				throw new InvalidOperationException("Value must be greater than 0");

			Quantity += value;
		}

		public bool TrySpend(int value)
		{
			if (value <= Quantity)
			{
				Quantity -= value;

				return true;
			}

			return false;
		}

		public void Reset()
		{
			Quantity = 0;
		}
	}

	internal class Wallet<TCurrency> : Wallet, IWallet<TCurrency>
		where TCurrency : struct, ICurrency
	{
		public override Type CurrencyType => typeof(TCurrency);

		public Wallet(IStorageService storageService) : base(storageService)
		{
			//
		}
	}
}
