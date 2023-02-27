using System.ComponentModel.DataAnnotations;

namespace ActivityTracker.Contracts
{
	public class UserCreateDto
	{
		[Required(ErrorMessage = "Username is required")]
		public string Username { get; set; } = null!;

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; } = null!;

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; } = null!;
	}
}
