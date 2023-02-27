using System;

namespace ActivityTracker.Domain.Model
{
	public class ActivityId : AbstractId
	{
		public ActivityId(string id)
		: base(id)
		{
			string[] compositeKeys = id.Split('#', 2);
			UserId = new UserId(compositeKeys[0]);
		}

		public UserId UserId { get; set; }

		public static ActivityId CreateFromComposites(UserId userId, Guid id)
		{
			return new ActivityId($"{userId}#{id}");
		}

	}
}
