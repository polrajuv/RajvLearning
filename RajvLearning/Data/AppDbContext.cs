using Microsoft.EntityFrameworkCore;
using RajvLearning.API.Entities;

namespace RajvLearning.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<LearningTopic> LearningTopic { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LearningTopic>()
            .ToTable("LearningTopic");
    }
}