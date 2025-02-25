using Dapper;
using Microsoft.Data.SqlClient;
using PlaciVerseApi.Models;
using System.Security.Claims;

namespace PlaciVerseApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        //private readonly string _sqlConnectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(IHttpContextAccessor httpContextAccessor)
        {
            //_sqlConnectionString = sqlConnectionString;
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetCurrentUserId()
        {
            // Returns the aspnet_User.Id of the authenticated user
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}
