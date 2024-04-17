using UnityEngine;
using WalletOop;
using Zenject;

namespace OopExample
{
	public class WalletUIController : MonoBehaviour
	{
		private IWalletProxyService _walletProxy;

		[Inject]
		private void Construct(IWalletProxyService walletProxy)
		{
			_walletProxy = walletProxy;
		}

		public void AddOneCoin()
		{
			_walletProxy.Increase<Coin>(1);
		}

		public void AddOneCrystal()
		{
			_walletProxy.Increase<Crystal>(1);
		}

		public void SpendThreeCoins()
		{
			if (_walletProxy.TrySpend<Coin>(3))
			{
				_walletProxy.Increase<Crystal>(3);
			}

			else
			{
				Debug.Log("Not enough coins");
			}
		}

		public void SpendThreeCrystals()
		{
			if (_walletProxy.TrySpend<Crystal>(3))
			{
				_walletProxy.Increase<Coin>(3);
			}

			else
			{
				Debug.Log("Not enough crystals");
			}
		}

		public void ResetCoin()
		{
			_walletProxy.Reset<Coin>();
		}

		public void ResetCrystal()
		{
			_walletProxy.Reset<Crystal>();
		}
	}
}
