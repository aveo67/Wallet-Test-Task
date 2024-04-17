using System;
using WalletEcs;

namespace EcsExamples
{
	internal class PurchaseConfirmation : IConfirmablePurchase
	{
		private readonly Action _onComfirmed;


		private readonly Action _onRejected;

		public bool Processed { get; private set; }

		public PurchaseConfirmation(Action onComfirmed, Action onRejected)
		{
			_onComfirmed = onComfirmed;
			_onRejected = onRejected;
		}

		public void Confirm()
		{
			if (Processed)
				return;

			_onComfirmed?.Invoke();

			Processed = true;
		}

		public void Reject()
		{
			if (Processed)
				return;

			_onRejected?.Invoke();

			Processed = true;
		}
	}
}
