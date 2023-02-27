using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Contracts;
using ActivityTracker.Domain.Entities;
using ActivityTracker.Domain.Repositories;

namespace ActivityTracker.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public Task<IEnumerable<UserDto>> GetAllAsync()
		{
			List<User> users = _userRepository.GetAllAsync().Result;
			IEnumerable<UserDto> userDtos = users.Select(user => new UserDto { Name = user.Name, UserName = user.Username });
			return Task.FromResult(userDtos);
		}

		public Task<UserDto> GetByIdAsync(string username)
		{
			var user = _userRepository.GetByIdAsync(username).Result;
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user), "User not found");
			}

			UserDto dto = new UserDto { Name = user.Name, UserName = user.Username };
			return Task.FromResult(dto);
		}

		public Task<UserDto> CreateAsync(UserCreateDto userCreateDto)
		{
			if (_userRepository.GetByIdAsync(userCreateDto.Username).Result != null)
			{
				throw new ArgumentException("Username exists", nameof(userCreateDto.Username));
			}

			User user = new User(userCreateDto.Username, userCreateDto.Password, userCreateDto.Name);
			_userRepository.SaveAsync(user);
			user = _userRepository.GetByIdAsync(userCreateDto.Username).Result!;

			UserDto dto = new UserDto { Name = user.Name, UserName = user.Username };
			return Task.FromResult(dto);
		}
	}
}
