namespace Storage
{
	public interface ISaveable
	{
		string Key { get; }

		int GetData();

		void RestoreData(int data);
	}
}
