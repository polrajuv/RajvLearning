using Microsoft.EntityFrameworkCore;

namespace RajvLearning.API.Data
{
    using RajvLearning.API.Models;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<LearningTopic> LearningTopic { get; set; }
    }
}