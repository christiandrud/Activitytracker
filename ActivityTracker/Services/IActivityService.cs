using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityTracker.Contracts;

namespace ActivityTracker.Services
{
	public interface IActivityService
	{
		Task<IEnumerable<ActivityDto>> GetActivitiesByUserId(string userName);

		Task<ActivityDto> CreateAsync(ActivityCreateDto activityCreateDto);

		Task UpdateAsync(string activityId);

		Task<ActivityDto> GetByIdAsync(string activityId);
	}
}
