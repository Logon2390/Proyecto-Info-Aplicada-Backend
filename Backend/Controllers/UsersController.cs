using Backend.Models;
using Backend.Custom;
using Backend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly Utility _utility;

        public UsersController(UserContext context, Utility utility)
        {
            _context = context;
            _utility = utility;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UsuarioDTO user)
        {
            var userModel = new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Password = _utility.encryptSHA256(user.Password)
            };

            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();

            if (userModel.Id != 0)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            }
            else {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
            }
        }

        [HttpPost]
        [Route("username")]
        public async Task<IActionResult> CheckUsername(String username)
        {
            var userFound = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            if (userFound == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { isAvailable = true });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isAvailable = false });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            var userFound = await _context.Users.Where(u => u.Username == user.Username && u.Password == _utility.encryptSHA256(user.Password)).FirstOrDefaultAsync();
            if (userFound == null) {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utility.generateJWT(userFound) });
            }
        }
    }
}
