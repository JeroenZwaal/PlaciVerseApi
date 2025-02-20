using PlaciVerseApi.Classes;

namespace PlaciVerseApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(User user);
        Task<UserDto> LoginUser(string username);
        Task<string> GetPasswordHash(string username);
    }
}