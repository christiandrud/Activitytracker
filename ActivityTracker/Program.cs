using System.Collections.Generic;
using ActivityTracker.Domain.Repositories;
using ActivityTracker.Persistence.Repositories.InMemory;
using ActivityTracker.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityTracker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.Add(new ServiceDescriptor(typeof(IUserRepository), typeof(InMemoryUserRepository),
				ServiceLifetime.Singleton));
			builder.Services.Add(new ServiceDescriptor(typeof(IActivityRepository), typeof(InMemoryActivityRepository),
				ServiceLifetime.Singleton));
			builder.Services.Add(new ServiceDescriptor(typeof(IActivityService), typeof(ActivityService),
				ServiceLifetime.Singleton));
			builder.Services.Add(new ServiceDescriptor(typeof(IUserService), typeof(UserService),
				ServiceLifetime.Singleton));

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddCors(options =>
				// default policy is applied to ALL controller endpoints
				options.AddDefaultPolicy(builder =>
				{
					var origins = new List<string> { "http://localhost:3000" };
					builder.WithMethods("GET", "POST", "OPTIONS", "PUT", "DELETE");
					builder.WithHeaders("content-type");

					builder.WithOrigins(origins.ToArray())
						.SetIsOriginAllowedToAllowWildcardSubdomains()
						.AllowCredentials();
				})
			);

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.MapControllers();
			app.UseCors();
			app.Run();
		}
	}
}