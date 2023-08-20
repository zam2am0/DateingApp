using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public readonly ITokenService _tokenService;
        public AccountController(DataContext context , ITokenService tokenService) 
        {
            _tokenService = tokenService;
            _context = context;
        }
        
        [HttpPost("register")]  //POST: api/account/register?username=dave&password=pwd
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            //username
            if  (await UserExists(registerDto.UserName)) return BadRequest("Username is taken");
            //password
            using  var hmac = new HMACSHA512(); //just initilizes new instance of the max 512 class with randomlly generated key.

            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                passwordSalt = hmac.Key
            };

            _context.Users.Add(user); 
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        } 

        [HttpPost("login")]  
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if (user == null) return Unauthorized("Invalid username");

            using  var hmac = new HMACSHA512(user.passwordSalt); //user.passwordSalt -> here need to specify user password salts as the MAC key

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < computeHash.Length; i++)
            {
                if(computeHash[i] != user.passwordHash[i]) return Unauthorized("Invalid password"); //any element doesn't match then mean password are not equivalent.
            }

             return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists(string username) 
        {
            return await _context.Users.AnyAsync(x=>x.UserName == username.ToLower()); //x -> refer to app user
        } 
    }
}