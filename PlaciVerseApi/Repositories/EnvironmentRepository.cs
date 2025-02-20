using Dapper;
using Microsoft.Data.SqlClient;
using PlaciVerseApi.Classes;

namespace PlaciVerseApi.Repositories
{
    public class EnvironmentRepository : IEnvironmentRepository
    {
        private readonly string sqlConnectionString;

        public EnvironmentRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Environment2D> CreateEnvironment(Environment2D env)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Environments (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
                return await connection.QuerySingleAsync<Environment2D>(query, env);
            }
        }

        public async Task<Environment2D?> GetEnvironment(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                var env = await connection.QueryFirstOrDefaultAsync<Environment2D>("SELECT * FROM Environments WHERE Id = @Id", new { Id = id });
                return env;
            }
        }

        public async Task<bool> UpdateEnvironment(Environment2D env)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Environments SET Name = @Name WHERE Id = @Id";
                return await connection.ExecuteAsync(query, env) > 0;
            }

        }

        public async Task<bool> DeleteEnvironment(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Environments WHERE Id = @Id";
                return await connection.ExecuteAsync(query, new { Id = id }) > 0;
            }

        }
    }
}
