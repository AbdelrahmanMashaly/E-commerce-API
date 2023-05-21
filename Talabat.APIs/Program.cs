using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Domain.IRepository;
using Talabat.Repository;
using Talabat.Repository.Data;
namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.SwaggerServices();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.AddServices();

            var app = builder.Build();

            #region Updating Database 
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<StoreContext>();

                await dbContext.Database.MigrateAsync();

                await StoreContextSeed.SeedData(dbContext);
            }
            catch (Exception ex)
            {
                var log = logger.CreateLogger<Program>();
                log.LogError(ex, ex.Message);
            } 
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.SwaggerMiddleware();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}