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

        
    }
}
