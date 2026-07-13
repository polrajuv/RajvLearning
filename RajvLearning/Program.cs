using Microsoft.EntityFrameworkCore;
using RajvLearning.API.Data;
using RajvLearning.API.Interfaces;
using RajvLearning.API.Middleware;
using RajvLearning.API.Repositories;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(
        "Logs/rajvlearning-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);


// Serilog
builder.Host.UseSerilog();


// 1. Register Services

builder.Services.AddControllers();


// JWT Authentication 
builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer();


builder.Services.AddAuthorization();


// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// Repository Dependency Injection
builder.Services.AddScoped<ILearningTopicRepository, LearningTopicRepository>();


var app = builder.Build();


// Global Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();


// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


// Authentication must come before Authorization for JWT
app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();


app.Run();