using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Api.Controllers;
using Api.Data;
using Api.Entities;
using Api.Dtos;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Interfaces;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<User> _logger;

        public AccountController(DataContext context, ITokenService tokenService, ILogger<User> logger)
        {
            _context = context;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto regDto)
        {
            if (await UserExists(regDto.Email)) return BadRequest("Email is taken");
            using var hmac = new HMACSHA512();

            var user = new User
            {
                Name = regDto.UserName,
                Email = regDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(regDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var a = _tokenService.CreateToken(user);
            return new UserDto
            {
                Id = user.Id,
                Name = regDto.UserName,
                Email = regDto.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(user => user.Email == loginDto.Email);

            if (user == null) return BadRequest("Invalid Email or Password");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email.ToLower());
        }
    }
}