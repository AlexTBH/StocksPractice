
using Microsoft.EntityFrameworkCore;
using stocksTradePractice.Models;
using System.Data.Common;

namespace stocksTradePractice
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddHttpClient();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var conString = builder.Configuration.GetConnectionString("StocksDb");

			builder.Services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(conString);
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
