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

            return CreatedAtAction(nameof(CreateObject), result);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateObject(int id, [FromBody] Object2D obj)
        {
            if (obj == null || id != obj.ObjectId)
                return BadRequest("Invalid object data.");

            var updatedObject = await _objectRepository.UpdateObject(obj);
            if (updatedObject == null)
                return NotFound("Object not found.");

            return Ok(updatedObject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObject(int id)
        {
            var userId = _userRepository.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not Authorized");
            }
            var result = await _objectRepository.DeleteObject(id);
            if (!result)
            {
                return BadRequest("Object not deleted");
            }
            return Ok("Object deleted");
        }
    }
}
