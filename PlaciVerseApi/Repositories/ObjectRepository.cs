using PlaciVerseApi.Classes;

namespace PlaciVerseApi.Repositories
{
    public class ObjectRepository : IObjectRepository
    {
        private readonly string sqlConnectionString;
        public ObjectRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public Task<Object2D> CreateObject(Object2D obj)
        {
            throw new NotImplementedException();
        }

        public Task<Object2D?> GetObject(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Object2D>> GetAllObjects()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateObject(Object2D obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteObject(int id)
        {
            throw new NotImplementedException();
        }

    }
}
