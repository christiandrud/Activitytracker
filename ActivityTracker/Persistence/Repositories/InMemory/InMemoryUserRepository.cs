using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityTracker.Domain.Entities;
using ActivityTracker.Domain.Repositories;
using Newtonsoft.Json;

namespace ActivityTracker.Persistence.Repositories.InMemory
{
	public class InMemoryUserRepository : IUserRepository
	{
		public InMemoryUserRepository()
		{
			Cache = new Dictionary<string, string>();
		}

		public Dictionary<string, string> Cache { get; }

		public Task SaveAsync(User user)
		{
			if (!Cache.ContainsKey(user.Username))
			{
				Cache.Add(user.Username, JsonConvert.SerializeObject(user));
			}
			else
			{
				Cache[user.Username] = JsonConvert.SerializeObject(user);
			}

			return Task.CompletedTask;
		}

		public Task<List<User>> GetAllAsync()
		{
			List<User> users = new List<User>();
			foreach (var user in Cache)
			{
				users.Add(JsonConvert.DeserializeObject<User>(user.Value)!);
			}

			return Task.FromResult(users);
		}

		public Task<User?> GetByIdAsync(string userId)
		{
			if (Cache.ContainsKey(userId) && !string.IsNullOrEmpty(Cache[userId]))
			{
				User user = JsonConvert.DeserializeObject<User>(Cache[userId])!;
				return Task.FromResult(user)!;
			}

			return Task.FromResult<User?>(null);
		}
	}
}
