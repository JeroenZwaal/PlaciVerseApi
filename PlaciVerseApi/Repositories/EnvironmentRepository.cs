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

        public async Task<bool> CreateEnvironment(Environment2D env, string userId)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Environments (Name, MaxLenght, MaxHeight, OwnerUserId) VALUES (@Name, @MaxLenght, @MaxHeight, @userId)";
                var result = await connection.ExecuteAsync(query, new
                {
                    env.Name,
                    env.MaxLenght,
                    env.MaxHeight,
                    userId
                });

                return result > 0; 
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
