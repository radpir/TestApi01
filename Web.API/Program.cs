using Application.WorkTasks.Create;
using Application.WorkTasks.Delete;
using Application.WorkTasks.Get;
using Application.WorkTasks.GetById;
using Application.WorkTasks.Update;
using Domain.Data;
using Domain.WorkTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Helpers;
using Persistence.Repositories;
using Web.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddScoped<IWorkTaskRepository, WorkTaskRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMediatR(cfg =>
    {
        //cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        cfg.RegisterServicesFromAssemblyContaining<CreateWorkTaskCommand>();
        //cfg.RegisterServicesFromAssembly(typeof(CreateWorkTaskCommand).Assembly);
        //cfg.RegisterServicesFromAssemblies(typeof(CreateWorkTaskCommand).Assembly);
        //cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

    }
    );

var app = builder.Build();

var scope = app.Services.CreateScope();
await DataHelper.ManageDataAsync(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (ApplicationDbContext dbContext) =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

var x = new WorkTaskEndpointDefinition();
x.RegisterEndpoints(app);

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
