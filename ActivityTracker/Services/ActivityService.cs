using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Contracts;
using ActivityTracker.Domain.Entities;
using ActivityTracker.Domain.Model;
using ActivityTracker.Domain.Repositories;

namespace ActivityTracker.Services
{
	public class ActivityService : IActivityService
	{
		private readonly IActivityRepository _activityRepository;

		public ActivityService(IActivityRepository activityRepository)
		{
			_activityRepository = activityRepository;
		}

		public async Task<IEnumerable<ActivityDto>> GetActivitiesByUserId(string username)
		{
			IEnumerable<Activity> activities = await _activityRepository.GetByUserIdAsync(new UserId(username));
			IEnumerable<ActivityDto> dtos = activities.Select(activity => new ActivityDto(activity.ActivityId.Id, activity.Name,
				activity.Start.ToString("u"), HandleEndDate(activity.End), PrettyFormatTimeSpan(activity.Start, activity.End)));
			return dtos;
		}

		public async Task<ActivityDto> CreateAsync(ActivityCreateDto activityCreateDto)
		{
			ActivityId activityId = ActivityId.CreateFromComposites(new UserId(activityCreateDto.Username), Guid.NewGuid());
			Activity activity = new Activity(activityId, activityCreateDto.Name, DateTime.UtcNow);
			await _activityRepository.SaveAsync(activity);
			activity = await _activityRepository.GetByIdAsync(activityId) ?? throw new InvalidOperationException();
			ActivityDto dto = new ActivityDto(activityId.ToString(), activity.Name, activity.Start.ToString("u"), HandleEndDate(activity.End), PrettyFormatTimeSpan(activity.Start, activity.End));
			return dto;
		}

		public async Task UpdateAsync(string activityId)
		{
			var activity = await _activityRepository.GetByIdAsync(new ActivityId(activityId));
			if (activity == null)
			{
				throw new ArgumentNullException(nameof(activity), "Activity not found");
			}

			activity.End = DateTime.UtcNow;
			await _activityRepository.SaveAsync(activity);
		}

		public async Task<ActivityDto> GetByIdAsync(string activityId)
		{
			var activity = await _activityRepository.GetByIdAsync(new ActivityId(activityId));
			if (activity == null)
			{
				throw new ArgumentNullException(nameof(activity), "Activity not found");
			}

			ActivityDto dto = new ActivityDto(activity.ActivityId.ToString(), activity.Name, activity.Start.ToString("u"),
				HandleEndDate(activity.End), PrettyFormatTimeSpan(activity.Start, activity.End));
			return dto;
		}

		private static string HandleEndDate(DateTime? end)
		{
			if (end.HasValue)
			{
				return end.Value.ToString("u");
			}

			return string.Empty;
		}

		private static string PrettyFormatTimeSpan(DateTime start, DateTime? end)
		{
			if (end.HasValue)
			{
				var span = end.Value - start;
				if (span.Days > 0)
					return $"{span.Days} days, {span.Hours} hours, {span.Minutes} minutes, {span.Seconds} seconds";
				if (span.Hours > 0)
					return $"{span.Hours} hours, {span.Minutes} minutes, {span.Seconds} seconds";
				if (span.Minutes > 0)
					return $"{span.Minutes} minutes, {span.Seconds} seconds";

				return $"{span.Seconds} seconds";
			}

			return string.Empty;
		}
	}
}
