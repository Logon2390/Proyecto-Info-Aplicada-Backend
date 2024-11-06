using Backend.Models.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UsuarioDTO userDto)
        {
            var isSuccess = await _userService.RegisterUser(userDto);
            return Ok(new { isSuccess });
        }

        [HttpPost]
        [Route("username")]
        public async Task<IActionResult> CheckUsername(string username)
        {
            var isAvailable = await _userService.IsUsernameAvailable(username);
            return Ok(new { isAvailable });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO userDto)
        {
            var (isSuccess, token) = await _userService.LoginUser(userDto);
            return Ok(new { isSuccess, token });
        }
    }
}


