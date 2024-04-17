namespace WalletEcs
{
	public interface IConfirmablePurchase
	{
		bool Processed { get; }

		void Confirm();

		void Reject();
	}
}
