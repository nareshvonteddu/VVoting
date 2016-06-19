using System;
using Akavache;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Collections.Generic;

namespace VVoting
{
	public class Cache : ICache
	{
		public Cache()
		{
			BlobCache.ApplicationName = "vvoting";
		}

		public async Task RemoveObject(string key)
		{
			await BlobCache.LocalMachine.Invalidate(key);
		}

		public async Task<T> GetObject<T>(string key)
		{
			try
			{
				return await BlobCache.LocalMachine.GetObject<T>(key);
			}
			catch (KeyNotFoundException)
			{
				return default(T);
			}
			catch (Exception)
			{
				return default(T);
			}
		}

		public async Task InsertObject<T>(string key, T value)
		{
			await BlobCache.LocalMachine.InsertObject(key, value);
		}
	}
}

