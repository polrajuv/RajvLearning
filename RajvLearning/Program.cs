using Microsoft.EntityFrameworkCore;
using RajvLearning.API.Data;
using RajvLearning.API.Interfaces;
using RajvLearning.API.Middleware;
using RajvLearning.API.Repositories;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    //.MinimumLevel.Error() 
    .MinimumLevel.Information()
    .WriteTo.File(
        "Logs/rajvlearning-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// 1. Register services
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection999")));

builder.Services.AddScoped<ILearningTopicRepository, LearningTopicRepository>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// 2. Build the app
var app = builder.Build();

// Global Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();


// 3. Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();