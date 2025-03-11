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

        public async Task<bool> CreateObject(Object2D obj, int envId)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync("INSERT INTO Objects (EnvironmentId, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer) " +
                     "VALUES (@environmentId, @prefabId, @positionX, @positionY, @scaleX, @scaleY, @rotationZ, @sortingLayer)", obj) > 0;
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


        //public async Task<bool> UpdateObject(Object2D obj)
        //{
        //    using (var connection = new SqlConnection(sqlConnectionString))
        //    {
        //        connection.Open();
        //        return await connection.ExecuteAsync("UPDATE Objects SET PrefabId = @PrefabId, PositionX = @PositionX, PositionY = @PositionY, ScaleX = @ScaleX, ScaleY = @ScaleY, RotationZ = @RotationZ, SortingLayer = @SortingLayer WHERE ObjectId = @ObjectId", obj) > 0;
        //    }
        //}

        //public async Task<bool> DeleteObject(int id)
        //{
        //    using (var connection = new SqlConnection(sqlConnectionString))
        //    {
        //        connection.Open();
        //        return await connection.ExecuteAsync("DELETE FROM Objects WHERE ObjectId = @id", new { id }) > 0;
        //    }
        //}

    }
}
