using System;
using ActivityTracker.Domain.Model;

namespace ActivityTracker.Domain.Entities
{
	public class Activity
	{
		public Activity(ActivityId activityId, string name, DateTime start)
		{
			Name = name;
			Start = start;
			ActivityId = activityId;
		}

		public ActivityId ActivityId { get; set; }
		public string Name { get; set; }
		public DateTime Start { get; set; }
		public DateTime? End { get; set; }
	}
}
