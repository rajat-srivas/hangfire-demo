using Hangfire;
using hangfire_demo.Context;
using hangfire_demo.Jobs.Indexing;
using hangfire_demo.Jobs.Recurring;
using hangfire_demo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace hangfire_demo
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddScoped<RecurringJobProvider>();
			builder.Services.AddScoped<IndexingJobProvider>();
			builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			builder.Services.AddHangfire(x =>
			{
				x.UseSqlServerStorage(builder.Configuration["ConnectionStrings:HangfireStorage"]);
			});
			builder.Services.AddDbContext<CoreDbContext>(options => 
						options.UseSqlServer(builder.Configuration["ConnectionStrings:CoreStorage"]));
	
			builder.Services.AddHangfireServer();

			var app = builder.Build();


			

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();
			app.MapControllers();
			app.UseHangfireDashboard();
			app.Run();
		}
	}
}