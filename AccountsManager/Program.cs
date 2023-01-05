using AccountsManager.API.Filters;
using AccountsManager.Application.V1.Registery;
using AccountsManager.DataAccess.V1.Registery;
using AccountsManager.DataModels.V1.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
{
    config.Filters.Add(new ExceptionHandlingFilter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterDataRepositories();
builder.Services.RegisterBusinessServices();
builder.Services.RegisterHelpers();

builder.Services.AddDbContextPool<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
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