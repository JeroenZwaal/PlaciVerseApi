using PlaciVerseApi.Classes;

namespace PlaciVerseApi.Repositories
{
    public interface IEnvironmentRepository
    {
        Task<Environment2D> CreateEnvironment(Environment2D env);
        Task<Environment2D?> GetEnvironment(int id);
        Task<bool> UpdateEnvironment(Environment2D env);
        Task<bool> DeleteEnvironment(int id);
    }
}
