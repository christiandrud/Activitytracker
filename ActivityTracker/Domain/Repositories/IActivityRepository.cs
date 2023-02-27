using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityTracker.Domain.Entities;
using ActivityTracker.Domain.Model;

namespace ActivityTracker.Domain.Repositories
{
	public interface IActivityRepository
	{
		Task SaveAsync(Activity activity);
		Task<List<Activity>> GetByUserIdAsync(UserId userId);
		Task<Activity?> GetByIdAsync(ActivityId activityId);
	}
}
