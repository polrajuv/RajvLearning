using RajvLearning.API.Entities;

namespace RajvLearning.API.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}