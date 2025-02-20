using Dapper;
using Microsoft.Data.SqlClient;
using PlaciVerseApi.Classes;

namespace PlaciVerseApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _sqlConnectionString;
        public UserRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public async Task<User> RegisterUser(User user)
        {
            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Users (Username, Password) OUTPUT INSERTED.UserId VALUES (@Username, @Password)";
                var parameters = new { user.Username, user.Password };

                int userId = await connection.ExecuteScalarAsync<int>(query, parameters);
                
                user.UserId = userId;
                return user;
            } 
        }

        public async Task<UserDto> LoginUser(string username)
        {
            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT UserId, Username, Password FROM Users WHERE Username = @Username";
                var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });

                if (user == null)
                {
                    return null; 
                }

                return new UserDto
                {
                    UserId = user.UserId, 
                    Username = user.Username 
                };
            }
        }

        public async Task<string> GetPasswordHash(string username)
        {
            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Password FROM Users WHERE Username = @Username";
                var password = await connection.QueryFirstOrDefaultAsync<string>(query, new { Username = username });

                return password;
            }
        }
    }
}
