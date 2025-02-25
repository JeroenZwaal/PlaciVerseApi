using PlaciVerseApi.Models;

namespace PlaciVerseApi.Repositories
{
    public interface IUserRepository
    {
        string? GetCurrentUserId();
    }
}