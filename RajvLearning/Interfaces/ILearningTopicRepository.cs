using RajvLearning.API.Entities;

namespace RajvLearning.API.Interfaces
{
    public interface ILearningTopicRepository
    {
        Task<IEnumerable<LearningTopic>> GetAllAsync();

        Task<LearningTopic?> GetByIdAsync(int id);

        Task AddAsync(LearningTopic topic);

        Task UpdateAsync(LearningTopic topic);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}