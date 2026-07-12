using Microsoft.EntityFrameworkCore;
using RajvLearning.API.Data;
using RajvLearning.API.Entities;
using RajvLearning.API.Interfaces;

namespace RajvLearning.API.Repositories
{
    //public class LearningTopicRepository
    public class LearningTopicRepository(AppDbContext context) : ILearningTopicRepository
    {
       
        public async Task AddAsync(LearningTopic topic)
        {
            await context.LearningTopic.AddAsync(topic);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var topic = await context.LearningTopic
                .FirstOrDefaultAsync(x => x.Id == id);

            if (topic != null)
            {
                context.LearningTopic.Remove(topic);
            }
        }

        public async Task<LearningTopic?> GetByIdAsync(int id)
        {
            return await context.LearningTopic
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.LearningTopic
                .AnyAsync(x => x.Id == id);
        }


        public async Task<IEnumerable<LearningTopic>> GetAllAsync()
        {
            return await context.LearningTopic
                .OrderBy(x => x.Title)
                .ToListAsync();
        }

       
        public Task UpdateAsync(LearningTopic topic)
        {
            context.LearningTopic.Update(topic);
            return Task.CompletedTask;
        }
    }
}
