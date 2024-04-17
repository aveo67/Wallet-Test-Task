namespace WalletOop
{
	public interface IWalletProxyService
	{
		bool TrySpend<TCurrency>(int value)
			where TCurrency : struct, ICurrency;

		void Increase<TCurrency>(int value)
			where TCurrency : struct, ICurrency;

		void Reset<TCurrency>()
			where TCurrency : struct, ICurrency;

		void ResetAll();
	}
}
