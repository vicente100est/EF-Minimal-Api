using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TaskContext>(options => options.UseInMemoryDatabase("TaskDB"));
builder.Services.AddSqlServer<TaskContext>(builder.Configuration.GetConnectionString("cnTask"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbConnection", async ([FromServices] TaskContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.Run();
