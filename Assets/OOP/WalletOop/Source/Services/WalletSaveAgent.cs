using Storage;

namespace WalletOop
{
	internal class WalletSaveAgent : ISaveable
	{
		private readonly Wallet _context;

		public string Key => _context.CurrencyType.Name;



		public WalletSaveAgent(Wallet context)
		{
			_context = context;
		}

		public int GetData() => _context.Quantity;

		public void RestoreData(int data)
		{
			_context.Set(data);
		}
	}
}
