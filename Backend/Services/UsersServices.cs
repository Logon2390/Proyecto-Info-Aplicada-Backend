using Backend.Custom;
using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class UserService
    {
        private readonly UserContext _context;
        private readonly Utility _utility;

        public UserService(UserContext context, Utility utility)
        {
            _context = context;
            _utility = utility;
        }

        public async Task<bool> RegisterUser(UsuarioDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate,
                Password = Utility.encryptSHA256(userDto.Password)
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id != 0;
        }

        public async Task<bool> IsUsernameAvailable(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user == null;
        }

        public async Task<(bool isSuccess, string token)> LoginUser(LoginDto userDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == userDto.Username && u.Password == Utility.encryptSHA256(userDto.Password));

            if (user == null)
                return (false, "");

            var token = _utility.generateJWT(user);
            return (true, token);
        }
    }
}
