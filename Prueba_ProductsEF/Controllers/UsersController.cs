using Microsoft.AspNetCore.Mvc;
using Prueba_productsEF.Models;
using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Helpers;
using Prueba_ProductsEF.Models;

namespace Prueba_ProductsEF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService userService)
        {
            _service = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] UserLoginDto userDto)
        {
            var user = await _service.LogInAsync(userDto.Username, userDto.Password);
            if (user == null)
                return NotFound(new APIResponse(false, "Invalid username or password"));

            //var validPassword = PasswordHelper.HashPassword(user.Password);
            return Ok(new APIResponse(true, "Login successful", user));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserDto user)
        {
            var validPassword = PasswordHelper.HashPassword(user.Password);

            var newUser = await _service.RegisterAsync(user, validPassword);
            if (newUser == null)
                return BadRequest(new APIResponse(false, "Username or email already exists"));
            return Ok(new APIResponse(true, "Registration successful", newUser));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetProducts()
        {
            var users = await _service.GetUsersAsync();
            return Ok(new APIResponse(true, "Users successfuly found", users));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new APIResponse(false, "User not found"));
            return Ok(new APIResponse(true, "User successfuly found", user));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUser(UserDto user)
        {
            var newUser = await _service.AddUserAsync(user);
            return Ok(new APIResponse(true, "User successfuly added", newUser));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto inputUser)
        {
            var updateUser = await _service.UpdateUserAsync(id, inputUser);
            if (updateUser == null)
                return NotFound(new APIResponse(false, "User not found"));

            return Ok(new APIResponse(true, "User successfuly updated", updateUser));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var delUser = await _service.DeleteUserAsync(id);
            if (!delUser)
                return NotFound(new APIResponse(false, "User not found"));

            return Ok(new APIResponse(true, "User successfuly deleted"));
        }
    }
}
