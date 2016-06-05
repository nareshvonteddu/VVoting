using System;
using System.Threading.Tasks;

namespace VVoting
{
	public interface ICache
	{
		Task<T> GetObject<T>(string key);
		Task InsertObject<T>(string key, T value);
		Task RemoveObject(string key);
	}
}

