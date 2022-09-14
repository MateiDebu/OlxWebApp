using Core.Contracts;
using Core.Models;
using DataAccess.Users.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        //private readonly ILogger<UserRepository> _logger;

        public UserController(IUserService userService) //ILogger<UserRepository> logger)
        {
            _userService = userService;
           // _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserForList>> GetUsers([Range(0, int.MaxValue)] int offset = 1, [Range(1, 100)] int limit = 3)
        {
            //_logger.LogInformation("Getting users with offset={offset} and limit={limit}", offset, limit);
            var users = _userService.GetAll(offset, limit);
            return Ok(users);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<UserForList>> GetUserById([Required] int userId)
        {
            var user = await _userService.GetById(userId);

            if (user == null)
            {
                //_logger.LogWarning("Cannot find the user with id {userId}", userId);
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]

        public async Task<ActionResult<UserForList>> CreateUser([Required][FromForm] string name)
        {
            var createdUser = await _userService.CreateUser(name);
            return Ok(createdUser);
        }

        [HttpPatch]
        [Route("{userId}")]

        public async Task<ActionResult<UserForList>> ModifyUser(
             [Required][FromRoute] int userId,
             [Required][FromForm] string name)
        {
            var existingUser = await _userService.GetById(userId);
            if (existingUser == null)
            {
                return NotFound();
            }

            var modifiedUser = await _userService.ModifyUser(userId, name);
            return Ok(modifiedUser);
        }

        [HttpDelete]
        [Route("{userId}")]

        public async Task<ActionResult<UserForList>> DeleteUser([Required][FromRoute] int userId)
        {
            var existingUser = await _userService.GetById(userId);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(userId);
            return NoContent();
        }
    }
}
