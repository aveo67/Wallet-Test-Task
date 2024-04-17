using System;
using System.Collections.Generic;
using System.Linq;

namespace WalletOop
{
	internal class WalletProxyService : IWalletProxyService
	{
		private readonly Dictionary<Type, IWallet> _wallets;

		public WalletProxyService(IWallet[] wallets)
		{
			_wallets = wallets.ToDictionary(n => n.CurrencyType);
		}

		private IWallet GetWallet(Type type)
		{
			if (_wallets.TryGetValue(type, out IWallet wallet))
			{
				return wallet;
			}

			throw new NullReferenceException($"Wallet of currency type {type.Name} doesn't exist");
		}

		public void Increase<TCurrency>(int value)
			where TCurrency : struct, ICurrency
		{
			GetWallet(typeof(TCurrency)).Increase(value);
		}

		public void Reset<TCurrency>()
			where TCurrency : struct, ICurrency
		{
			GetWallet(typeof(TCurrency)).Reset();
		}

		public void ResetAll()
		{
			foreach (var wallet in _wallets.Values)
			{
				wallet.Reset();
			}
		}

		public bool TrySpend<TCurrency>(int value)
			where TCurrency : struct, ICurrency
		{
			return GetWallet(typeof(TCurrency)).TrySpend(value);
		}
	}
}
