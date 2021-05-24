using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.ViewModels;
using DataService.Contexts;
using DataService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext db, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _db = db;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(Register model)
        {
            if(await UserExists(model.UserName)) return BadRequest("User Name is Taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser{
                UserName = model.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password)),
                PasswordSalt = hmac.Key
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return new User
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(Login model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x=>x.UserName == model.UserName.ToLower());
            if(user == null) return Unauthorized("Invalid UserName");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new User
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists(string userName){
            return await _db.Users.AnyAsync(x=>x.UserName == userName.ToLower());
        }
    }
}