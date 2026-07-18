using Microsoft.EntityFrameworkCore;
using RajvLearning.API.Data;
using RajvLearning.API.Interfaces;
using RajvLearning.API.Middleware;
using RajvLearning.API.Repositories;
using RajvLearning.API.Services;
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


// ================================
// 1. Register Services
// ================================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


// ================================
// CORS Configuration
// ================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});



// ================================
// JWT Authentication
// ================================

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        // JWT validation will be configured here
        // after appsettings.json JWT setup
    });


builder.Services.AddAuthorization();


// ================================
// Database
// ================================

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
        .GetConnectionString("DefaultConnection")));


// ================================
// Dependency Injection
// ================================

// Repository
builder.Services.AddScoped
    <ILearningTopicRepository, LearningTopicRepository>();


// JWT Token Service
builder.Services.AddScoped
    <ITokenService, TokenService>();



// ================================
// 2. Build Application
// ================================

var app = builder.Build();



// ================================
// Global Exception Middleware
// ================================

app.UseMiddleware<ExceptionMiddleware>();



// ================================
// Swagger
// ================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}



// ================================
// 3. Configure Middleware
// ================================

app.UseHttpsRedirection();


// CORS must be before Authentication
app.UseCors("ReactPolicy");


// JWT Authentication
app.UseAuthentication();


// Authorization
app.UseAuthorization();



// Map Controllers
app.MapControllers();



app.Run();