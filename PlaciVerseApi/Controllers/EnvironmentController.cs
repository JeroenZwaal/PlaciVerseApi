using Microsoft.AspNetCore.Mvc;
using PlaciVerseApi.Repositories;
using PlaciVerseApi.Models;

namespace PlaciVerseApi.Controllers
{
    [Route("environments")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        private readonly IEnvironmentRepository _environmentRepository;
        private readonly IUserRepository _userRepository;

        public EnvironmentController(IEnvironmentRepository environmentRepository, IUserRepository userRepository)
        {
            _environmentRepository = environmentRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnvironment([FromBody] Environment2D environment)
        {
            if (environment == null)
            {
                return BadRequest("Environment is empty");
            }

            if (environment.MaxLenght < 20 || environment.MaxLenght > 200)
            {
                return BadRequest("Lengte moet tussen 20 en 200 zijn!");
            }

            if (environment.MaxHeight < 10 || environment.MaxHeight > 100)
            {
                return BadRequest("Hoogte moet tussen 10 en 100 zijn!");
            }

            // Controleer of de naam tussen 1 en 25 tekens is
            if (string.IsNullOrEmpty(environment.Name) || environment.Name.Length < 1 || environment.Name.Length > 25)
            {
                return BadRequest("Naam moet tussen 1 en 25 tekens zijn!");
            }

            var userId = _userRepository.GetCurrentUserId();

            if (string.IsNullOrEmpty(userId)) {
                return Unauthorized("User not Authorized"); 
            }

            

            IEnumerable<Environment2D> envCount = await _environmentRepository.GetEnvironmentByUserId(userId);
            if (envCount.Count() >= 5)
            {
                return BadRequest("User has reached the maximum number of environments");
            }

            var result = await _environmentRepository.CreateEnvironment(environment, userId);
            
            if (result == null)
            {
                return BadRequest("Environment not created");
            }

            return CreatedAtAction(nameof(CreateEnvironment), environment);
        }

        [HttpGet]
        public async Task<IActionResult> GetEnvironmentsForUser()
        {
            var userId = _userRepository.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not Authorized");
            }

            var env = await _environmentRepository.GetEnvironmentByUserId(userId);

            return Ok(env);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvironment(int id)
        {
            var userId = _userRepository.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not Authorized");
            }

            var result = await _environmentRepository.DeleteEnvironment(id, userId);
            if (!result)
            {
                return BadRequest("Environment not deleted");
            }

            return Ok("Environment deleted");

        }
    }
}