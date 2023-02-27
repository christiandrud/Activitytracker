namespace ActivityTracker.Contracts
{
	public class ActivityDto
	{
		public ActivityDto(string activityId, string name, string start, string end, string duration)
		{
			ActivityId = activityId;
			Name = name;
			Start = start;
			End = end;
			Duration = duration;
		}

		public string ActivityId { get; set; }

		public string Name { get; set; }

		public string Start { get; set; }

		public string End { get; set; }

		public string Duration { get; set; }
	}
}
