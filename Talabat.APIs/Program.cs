using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat_Core.Entities;
using Talabat_Core.Repositories.Contract;
using Talabat_Repository;
using Talabat_Repository.Data;

namespace Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{

			
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region Configure Services
			// Add services to the container.

			webApplicationBuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();

			webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			});


			webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			
			webApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));

			webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{

					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
													.SelectMany(P => P.Value.Errors)
													.Select(E => E.ErrorMessage)
													.ToList();

					var response = new ApiValidationErrorResponse()
					{
						Errors = errors
					};

					return new BadRequestObjectResult(response);
				};

			});

			#endregion

			  
			var app = webApplicationBuilder.Build();


			using var scope = app.Services.CreateScope();


			var services = scope.ServiceProvider;
			
			var _dbContext = services.GetRequiredService<StoreContext>();
			// ASK CLR for Creating Object from DbContext Explicitly


			var loggerFactory = services.GetRequiredService<ILoggerFactory>();	

				var logger = loggerFactory.CreateLogger<Program>();
			try
			{
				await _dbContext.Database.MigrateAsync(); // Update-Database
				await StoreContextSeed.SeedAsync(_dbContext); // Data-Seeding
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "an Error has been occured during apply the migration");

            }
			#region Configure Kestrel Middlewares




			//app.UseMiddleware<ExceptionMiddleware>();

			app.Use(async (httpContext, _next) =>
			{
				try
				{
					// Take an Action with the Request

					await _next.Invoke(httpContext); // go to next middleware

					//  Take an Action with The response
				}
				catch (Exception ex)
				{
					logger.LogError(ex.Message); //Development
												 // Log Exception in (Database | Files) // Production Env

					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					httpContext.Response.ContentType = "application/json";

					var response = app.Environment.IsDevelopment() ?
						new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
						:
						new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);


					var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

					var json = JsonSerializer.Serialize(response, options);

					await httpContext.Response.WriteAsync(json);

				}

			});

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			 
			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}
