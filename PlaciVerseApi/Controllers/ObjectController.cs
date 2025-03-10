using Microsoft.AspNetCore.Mvc;
using PlaciVerseApi.Models;
using PlaciVerseApi.Repositories;

namespace PlaciVerseApi.Controllers
{
    [Route("objects")]
    [ApiController]
    public class ObjectController : ControllerBase
    {
        private readonly IObjectRepository _objectRepository;
        private readonly IUserRepository _userRepository;

        public ObjectController(IObjectRepository objectRepository, IUserRepository userRepository) 
        {
            _objectRepository = objectRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateObject(int envId, [FromBody] Object2D obj)
        {
            var userId = _userRepository.GetCurrentUserId();
            
            if (obj == null)
            {
                return BadRequest("Object is empty");
            }

            var result = await _objectRepository.CreateObject(obj, envId);
            
            if (result == null)
            {
                return BadRequest("Object not created");
            }

            return CreatedAtAction(nameof(CreateObject), obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetObjectsByEnvironmentId(int envId)
        {
            var userId = _userRepository.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not Authorized");
            }

            var objects = await _objectRepository.GetObjectsByEnvironmentId(envId);
            if (objects == null)
            {
                return NotFound("Objects not found");
            }
            
            return Ok(objects);
        }
    }
}
