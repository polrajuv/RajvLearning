using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RajvLearning.API.Data;
using RajvLearning.API.Interfaces;
using RajvLearning.API.Middleware;
using RajvLearning.API.Repositories;
using RajvLearning.API.Services;
using Serilog;
using System.Text;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(
        "Logs/rajvlearning-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);


// Serilog
builder.Host.UseSerilog();


// Controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


// CORS
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


// ===========================
// JWT Authentication
// ===========================

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)

    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.SaveToken = true;


        options.TokenValidationParameters =
            new TokenValidationParameters
            {

                ValidateIssuer = true,

                ValidateAudience = true,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,


                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],


                ValidAudience =
                    builder.Configuration["Jwt:Audience"],


                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]!)
                    )
            };
    });



builder.Services.AddAuthorization();


// Database

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
        .GetConnectionString("DefaultConnection")));


// Dependency Injection

builder.Services.AddScoped
<ILearningTopicRepository, LearningTopicRepository>();


builder.Services.AddScoped
<ITokenService, TokenService>();


var app = builder.Build();



// Exception Middleware

app.UseMiddleware<ExceptionMiddleware>();



// Swagger

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}



app.UseHttpsRedirection();


// CORS

app.UseCors("ReactPolicy");


// JWT

app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();


app.Run();