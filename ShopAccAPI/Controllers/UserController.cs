using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAccAPI.Dtos.User;
using ShopAccAPI.Interfaces;
using ShopAccAPI.Mappers;

namespace ShopAccAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsersAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            var userDtos = users.Select(u => u.ToUserDto());
            return Ok(userDtos);
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user.ToUserDto());
        }
        [HttpGet("GetUserByName/{name}")]
        public async Task<IActionResult> GetUserByUserName([FromRoute] string name)
        {
            var user = await _userRepo.GetUserByUsernameAsync(name);
            if (user == null)
            {
                return NotFound($"User with name '{name}' not found.");
            }
            return Ok(user.ToUserDto());
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null)
            {
                return BadRequest("Invalid user data.");
            }
            var user = await _userRepo.CreateUserAsync(createUserDto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user.ToUserDto());
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var user = await _userRepo.DeleteUserAsync(id);
                return Ok(user.ToUserDto());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] CreateUserDto updateUserDto)
        {
            if (updateUserDto == null)
            {
                return BadRequest("Invalid user data.");
            }
            try
            {
                var user = await _userRepo.UpdateUserAsync(id, updateUserDto);
                return Ok(user.ToUserDto());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
