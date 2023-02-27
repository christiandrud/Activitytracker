using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityTracker.Domain.Entities;

namespace ActivityTracker.Domain.Repositories
{
	public interface IUserRepository
	{
		Task SaveAsync(User user);
		Task<List<User>> GetAllAsync();
		Task<User?> GetByIdAsync(string userId);
	}
}
