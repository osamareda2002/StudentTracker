using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.EventLog;
using StudentTracker.APIs.Errors;
using StudentTracker.APIs.Extensions;
using StudentTracker.APIs.Helpers;
using StudentTracker.Core.Repositories.Contract;
using StudentTracker.Repository;
using StudentTracker.Repository.Data;
using System.Reflection;

namespace StudentTracker.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //static ip
            /*builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Listen(System.Net.IPAddress.Parse("192.168.1.200"), 5000);
            });*/

            //windows service
            /*builder.Host.UseWindowsService();

            //event log logging
            builder.Logging.AddEventLog(settings =>
            {
                settings.SourceName = "StudentTrackerService";
            
            });*/

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();
            builder.Services.AddDbContext<TrackerContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAplicationServices();


            // Add CORS configuration for static ip
            /*builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });*/

            // Add AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<TrackerContext>();
            //Ask CLR for creating obj from DbContext Explicitly 

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync(); //update DB
                await TrackerContextSeed.SeedAsync(_dbContext); //data seeding
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Has Been Occured During Apply Migrations");
                
            }
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseCors("AllowAll");
            app.UseHttpsRedirection(); //comment when we're using http on a port           
            app.UseCors("AllowNgrok");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}