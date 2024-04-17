using Zenject;

namespace WalletOop
{
	internal class WalletProxyServiceDiTag { }

	internal class WalletDiTag<TCurrency>
		where TCurrency : ICurrency
	{ }

	public static class WalletDiExtensions
	{
		public static DiContainer UseWalletProxyService(this DiContainer container)
		{
			if (!container.HasBinding<WalletProxyServiceDiTag>())
			{
				container.Bind<WalletProxyServiceDiTag>().AsSingle();

				container.Bind<IWalletProxyService>().To<WalletProxyService>().AsSingle();
			}

			return container;
		}

		public static DiContainer BindWallet<TCurrency>(this DiContainer container)
			where TCurrency : struct, ICurrency
		{
			if (!container.HasBinding<WalletDiTag<TCurrency>>())
			{
				container.Bind<WalletDiTag<TCurrency>>().AsSingle();

				container.Bind(typeof(IWallet), typeof(IWallet<TCurrency>)).To<Wallet<TCurrency>>().AsSingle();
			}

			return container;
		}
	}
}
