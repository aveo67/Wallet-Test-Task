using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage
{
	public interface IStorage
	{
		Task SaveAsync(Dictionary<string, int> data);

		Task<int> RestoreAsync(string key);
	}
}
