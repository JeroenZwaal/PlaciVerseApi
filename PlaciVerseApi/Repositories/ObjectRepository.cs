using Dapper;
using Microsoft.Data.SqlClient;
using PlaciVerseApi.Models;

namespace PlaciVerseApi.Repositories
{
    public class ObjectRepository : IObjectRepository
    {
        private readonly string sqlConnectionString;
        public ObjectRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Object2D?> CreateObject(Object2D obj, int envId)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Objects (EnvironmentId, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer) " +
                               "OUTPUT INSERTED.ObjectId VALUES (@EnvironmentId, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer)";

                var newId = await connection.ExecuteScalarAsync<int>(query, new
                {
                    EnvironmentId = envId,
                    obj.PrefabId,
                    obj.PositionX,
                    obj.PositionY,
                    obj.ScaleX,
                    obj.ScaleY,
                    obj.RotationZ,
                    obj.SortingLayer
                });

                if (newId > 0)
                {
                    return new Object2D
                    {
                        ObjectId = newId,
                        EnvironmentId = envId,
                        PrefabId = obj.PrefabId,
                        PositionX = obj.PositionX,
                        PositionY = obj.PositionY,
                        ScaleX = obj.ScaleX,
                        ScaleY = obj.ScaleY,
                        RotationZ = obj.RotationZ,
                        SortingLayer = obj.SortingLayer
                    };
                }

                return null; // Geeft aan dat de insert is mislukt
            }
        }

        public async Task<List<Object2D?>> GetObjectsByEnvironmentId(int envid)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.OpenAsync();
                var objects = await connection.QueryAsync<Object2D>("SELECT * FROM Objects WHERE EnvironmentId = @id", new { id = envid });

                return objects.ToList();
            }
        }


        public async Task<Object2D?> UpdateObject(Object2D obj)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Objects SET " +
                               "PrefabId = @PrefabId, PositionX = @PositionX, PositionY = @PositionY, " +
                               "ScaleX = @ScaleX, ScaleY = @ScaleY, RotationZ = @RotationZ, SortingLayer = @SortingLayer " +
                               "OUTPUT INSERTED.* WHERE ObjectId = @ObjectId";

                var updatedObject = await connection.QuerySingleOrDefaultAsync<Object2D>(query, new
                {
                    ObjectId = obj.ObjectId, // Gebruik ObjectId als de database dit verwacht
                    obj.PrefabId,
                    obj.PositionX,
                    obj.PositionY,
                    obj.ScaleX,
                    obj.ScaleY,
                    obj.RotationZ,
                    obj.SortingLayer
                });

                return updatedObject; // Geeft het bijgewerkte object terug of null als de update niet is gelukt
            }
        }

        public async Task<bool> DeleteObject(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync("DELETE FROM Objects WHERE ObjectId = @id", new { id }) > 0;
            }
        }

    }
}
