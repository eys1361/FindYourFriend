using FindYourFriend.Data;
using FindYourFriend.DTOs;
using FindYourFriend.Entities;
using FindYourFriend.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FindYourFriend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {

        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context , ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {

            if (await UserExsist(registerDTO.UserName)) { BadRequest("username has taken!"); }

            using var hmac = new HMACSHA512(); /*hash password*/

            var user = new AppUser
            {
                UserName = registerDTO.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new UserDTO { 
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExsist(string username)

        {
            return await _context.Users.AnyAsync(x => x.UserName == username);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.UserName);

            if (user == null) return  Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}