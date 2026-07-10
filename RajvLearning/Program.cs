using Microsoft.EntityFrameworkCore;
using RajvLearning.API.Data;
using RajvLearning.API.Interfaces;
using RajvLearning.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Register services
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILearningTopicRepository, LearningTopicRepository>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// 2. Build the app
var app = builder.Build();

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