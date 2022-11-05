using AccountsManager.API.Filters;
using AccountsManager.Application.V1.Registery;
using AccountsManager.DataAccess.V1.Registery;
using AccountsManager.DataModels.V1.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace AccountsManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var SQLConnection = builder.Configuration.GetConnectionString("SQLServer");

            // Add services to the container.

            builder.Services.AddControllers(config =>
            {
                config.Filters.Add(new ExceptionHandlingFilter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.RegisterDataRepositories();
            builder.Services.RegisterBusinessServices();
            builder.Services.RegisterHelpers();
            
            builder.Services.AddDbContextPool<AppDBContext>(options =>
            {
                options.UseSqlServer(SQLConnection);
            });

            var logger = new LoggerConfiguration()
                                .ReadFrom
                                .Configuration(builder.Configuration)
                                .CreateLogger();

            builder.Host.UseSerilog(logger);

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