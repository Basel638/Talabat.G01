using Microsoft.EntityFrameworkCore;
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

			#endregion

			  
			var app = webApplicationBuilder.Build();


			using var scope = app.Services.CreateScope();


			var services = scope.ServiceProvider;
			
			var _dbContext = services.GetRequiredService<StoreContext>();
			// ASK CLR for Creating Object from DbContext Explicitly


			var loggerFactory = services.GetRequiredService<ILoggerFactory>();	

			try
			{
				await _dbContext.Database.MigrateAsync(); // Update-Database

			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an Error has been occured during apply the migration");

            }
			#region Configure Kestrel Middlewares
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}
