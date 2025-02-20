using Microsoft.AspNetCore.Mvc;
using PlaciVerseApi.Repositories;
using PlaciVerseApi.Classes;

namespace PlaciVerseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        private readonly IEnvironmentRepository _environmentRepository;

        public EnvironmentController(IEnvironmentRepository environmentRepository)
        {
            _environmentRepository = environmentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnvironment([FromBody] Environment2D environment)
        {
            // Create the environment
            var newEnvironment = await _environmentRepository.CreateEnvironment(environment);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetEnvironments()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnvironment(int id)
        {
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnvironment(int id, [FromBody] Environment2D environment)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvironment(int id)
        {

            return Ok();
        }
    }
}