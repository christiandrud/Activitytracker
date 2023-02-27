using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityTracker.Contracts;
using ActivityTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Controllers
{
	[Route("/api")]
	[ApiController]
	public class ActivityController : ControllerBase
	{
		private readonly IActivityService _activityService;
		public ActivityController(IActivityService activityService)
		{
			_activityService = activityService;
		}

		[HttpGet]
		[Route("activities/{username}")]
		public async Task<IEnumerable<ActivityDto>> OnGetActivitiesByUsername(string username)
		{
			return await _activityService.GetActivitiesByUserId(username);
		}

		[HttpGet]
		[Route("activity/{activityId}")]
		public async Task<ActivityDto> OnGet(string activityId)
		{
			return await _activityService.GetByIdAsync(activityId);
		}

		[HttpPost]
		[Route("activity/create")]
		public async Task<ActivityDto> OnCreate(ActivityCreateDto dto)
		{
			return await _activityService.CreateAsync(dto);
		}

		[HttpPost]
		[Route("activity/update/{activityId}")]
		public async Task<IActionResult> OnUpdate(string activityId)
		{
			await _activityService.UpdateAsync(activityId);
			return Ok();
		}
	}
}
