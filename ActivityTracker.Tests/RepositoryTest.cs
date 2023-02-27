using System;
using ActivityTracker.Domain.Entities;
using ActivityTracker.Domain.Model;
using ActivityTracker.Persistence.Repositories.InMemory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActivityTracker.Tests
{
	[TestClass]
	public class RepositoryTest
	{
		[TestMethod]
		public void InMemoryActivityRepositoryTest()
		{
			InMemoryActivityRepository repository = new InMemoryActivityRepository();
			ActivityId activityId = ActivityId.CreateFromComposites(new UserId("MisterBlack"), Guid.NewGuid());
			Activity activity = new Activity(activityId, "Customer A", DateTime.UtcNow);
			repository.SaveAsync(activity);
			Assert.IsNotNull(repository.GetByIdAsync(activityId).Result);
			var activities = repository.GetByUserIdAsync(activityId.UserId).Result;
			Assert.IsTrue(activities.Count == 1);
		}
	}
}
