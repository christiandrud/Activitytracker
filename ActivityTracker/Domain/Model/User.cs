namespace ActivityTracker.Domain.Entities
{
	public class User
	{
		public User(string userName, string password, string name)
		{
			Username = userName;
			Password = password;
			Name = name;
		}

		public string Username { get; set; }
		public string Password { get; set; } //TODO not clear text
		public string Name { get; set; }
	}
}
