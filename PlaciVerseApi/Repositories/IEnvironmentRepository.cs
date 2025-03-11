using PlaciVerseApi.Models;

namespace PlaciVerseApi.Repositories
{
    public interface IEnvironmentRepository
    {
        Task<Environment2D?> CreateEnvironment(Environment2D env, string userId);
        Task<Environment2D?> GetEnvironmentByUserIdAndId(string userId, int id);
        Task<IEnumerable<Environment2D?>> GetEnvironmentByUserId(string userId);
        //Task<bool> UpdateEnvironment(Environment2D env);
        Task<bool> DeleteEnvironment(int id, string userId);
    }
}
