using Dapper;
using Microsoft.Data.SqlClient;
using PlaciVerseApi.Models;

namespace PlaciVerseApi.Repositories
{
    public class EnvironmentRepository : IEnvironmentRepository
    {
        private readonly string sqlConnectionString;

        public EnvironmentRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Environment2D?> CreateEnvironment(Environment2D env, string userId)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Environments (Name, MaxLenght, MaxHeight, OwnerUserId) OUTPUT INSERTED.EnvironmentId VALUES (@Name, @MaxLenght, @MaxHeight, @userId)";
                var newId = await connection.ExecuteScalarAsync<int>(query, new
                {
                    env.Name,
                    env.MaxLenght,
                    env.MaxHeight,
                    UserId = userId
                });

                if (newId > 0)
                {
                    return new Environment2D
                    {
                        EnvironmentId = newId,
                        Name = env.Name,
                        MaxLenght = env.MaxLenght,
                        MaxHeight = env.MaxHeight,
                    };
                }

                return null; // Geeft aan dat de insert is mislukt
            }
        }

        public async Task<Environment2D?> GetEnvironmentByUserIdAndId(string userId, int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Environment2D>("SELECT * FROM Environments WHERE EnvironmentId = @id AND OwnerUserId = @userId", new { id, userId });
            }
        }

        public async Task<IEnumerable<Environment2D?>> GetEnvironmentByUserId(string userId)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                string query = "SELECT * FROM Environments WHERE userId = @userId";
                return await connection.QueryAsync<Environment2D>("SELECT * FROM Environments WHERE OwnerUserId = @Id", new { Id = userId });
            }
        }

        public async Task<bool> DeleteEnvironment(int id, string userId)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.OpenAsync();

                //await connection.ExecuteAsync("DELETE FROM Objects WHERE EnvironmentId = @Id", new { Id = id });
                return await connection.ExecuteAsync("DELETE FROM Environments WHERE EnvironmentId = @id AND OwnerUserId = @userId", new { id, userId }) > 0;
            }

        }
    }
}
