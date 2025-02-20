using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaciVerseApi.Classes;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new User
            {
                Username = register.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(register.Password)
            };

            var createdUser = await _userRepository.RegisterUser(newUser);

            return Ok(new UserDto { UserId = createdUser.UserId, Username = createdUser.Username });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.LoginUser(login.Username);

            if (user == null)
            {
                return Unauthorized("Ongeldige gebruikersnaam");
            }

            string storedPasswordHash = await _userRepository.GetPasswordHash(login.Username);
            if (!BCrypt.Net.BCrypt.Verify(login.Password, storedPasswordHash))
            {
                return Unauthorized("Ongeldige wachtwoord.");
            }

            var userDtoResponse = new UserDto
            {
                UserId = user.UserId,
                Username = user.Username
            };

            return Ok(userDtoResponse);
        }


    }
}
