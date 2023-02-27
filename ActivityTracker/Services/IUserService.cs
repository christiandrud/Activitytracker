using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityTracker.Contracts;

namespace ActivityTracker.Services
{
	public interface IUserService
	{
		Task<IEnumerable<UserDto>> GetAllAsync();

		Task<UserDto> GetByIdAsync(string userId);

		Task<UserDto> CreateAsync(UserCreateDto userCreateDto);
	}
}
