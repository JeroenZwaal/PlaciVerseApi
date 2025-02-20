using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaciVerseApi.Models;
using PlaciVerseApi.Repositories;

namespace PlaciVerseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
