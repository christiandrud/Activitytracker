using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityTracker.Domain.Entities;
using ActivityTracker.Domain.Model;
using ActivityTracker.Domain.Repositories;
using Newtonsoft.Json;

namespace ActivityTracker.Persistence.Repositories.InMemory
{
	public class InMemoryActivityRepository : IActivityRepository
	{
		public InMemoryActivityRepository()
		{
			Cache = new Dictionary<string, string>();
		}

		public Dictionary<string, string> Cache { get; }

		public Task SaveAsync(Activity activity)
		{
			if (!Cache.ContainsKey(activity.ActivityId.ToString()))
			{
				if (activity.Start.Kind != DateTimeKind.Utc)
				{
					throw new ArgumentException("Start date is not UTC");
				}
				if (activity.End != null && ((DateTime)activity.End).Kind != DateTimeKind.Utc)
				{
					throw new ArgumentException("End date is not UTC");
				}

				Cache.Add(activity.ActivityId.ToString(), JsonConvert.SerializeObject(activity));
			}
			else
			{
				Cache[activity.ActivityId.ToString()] = JsonConvert.SerializeObject(activity);
			}

			return Task.CompletedTask;
		}

		public Task<List<Activity>> GetByUserIdAsync(UserId userId)
		{
			if (string.IsNullOrEmpty(userId.ToString())) throw new ArgumentNullException(nameof(userId.ToString));

			var items = new List<Activity>();
			foreach (var item in Cache)
			{
				ActivityId activityId = new ActivityId(item.Key);
				if (activityId.UserId.Id == userId.Id)
				{
					Activity activity = JsonConvert.DeserializeObject<Activity>(item.Value)!;
					items.Add(activity);
				}
			}

			return Task.FromResult(items);
		}

		public Task<Activity?> GetByIdAsync(ActivityId activityId)
		{
			if (Cache.ContainsKey(activityId.ToString()))
			{
				Activity? activity = JsonConvert.DeserializeObject<Activity>(Cache[activityId.ToString()])!;
				return Task.FromResult(activity)!;
			}

			return Task.FromResult<Activity?>(null);
		}
	}
}
