using UnityEngine;

namespace Storage
{
	[CreateAssetMenu(fileName = "FileStorageConfig", menuName = "Storage/FileConfig", order = 1)]
	public class FileStorageConfig : ScriptableObject
	{
		[SerializeField]
		private string _path;

		[SerializeField]
		private string _fileName;

		public string Path => _path;

		public string FileName => _fileName;
	}
}