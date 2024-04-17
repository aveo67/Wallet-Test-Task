using TMPro;
using UnityEngine;
using WalletOop;
using Zenject;

namespace OopExample
{
	public abstract class WalletView<TCurrency> : MonoBehaviour
		where TCurrency : struct, ICurrency
	{
		private IWallet<TCurrency> _wallet;

		[SerializeField]
		private TMP_Text _valueLabel;

		[Inject]
		private void Construct(IWallet<TCurrency> wallet)
		{
			_wallet = wallet;
		}

		private void Start()
		{
			_valueLabel.text = _wallet.Quantity.ToString();

			_wallet.QuantityChanged += OnQuantityChanged;
		}

		private void OnQuantityChanged(int value)
		{
			_valueLabel.text = value.ToString();
		}

		private void OnDestroy()
		{
			_wallet.QuantityChanged -= OnQuantityChanged;
		}
	}
}
