using Microsoft.AspNetCore.Mvc;
using PlaciVerseApi.Repositories;
using PlaciVerseApi.Models;

namespace PlaciVerseApi.Controllers
{
    [Route("api/[controller]")]
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