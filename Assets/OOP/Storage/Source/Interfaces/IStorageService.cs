using System.Threading.Tasks;

namespace Storage
{
	public interface IStorageService
	{
		void RegisterSaveable(ISaveable saveable);

		Task SaveAsync();

		Task RestoreAsync();
	}
}
