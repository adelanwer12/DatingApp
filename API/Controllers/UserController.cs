using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataService.Contexts;
using DataService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController: ControllerBase
    {
        private readonly DataContext _db;
        public UserController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _db.Users.ToListAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{Id:Guid}")]
        public async Task<ActionResult<AppUser>> GetUser(Guid Id)
        {
            var user = await _db.Users.FindAsync(Id);
            return Ok(user);
        }
    }
}