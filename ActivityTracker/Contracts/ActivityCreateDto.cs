namespace ActivityTracker.Contracts
{
	public class ActivityCreateDto
	{
		public ActivityCreateDto(string userName, string name)
		{
			Username = userName;
			Name = name;
		}

		public string Username { get; set; }

		public string Name { get; set; }
	}
}
