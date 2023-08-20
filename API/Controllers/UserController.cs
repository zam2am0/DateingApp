using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class UsersController: BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;

        }
        [AllowAnonymous]
        [HttpGet] //endpoint
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users =await _context.Users.ToListAsync();

            return users; //return the list of usrs
        }

        [HttpGet("{id}")] // /api/user/2
            public async Task<ActionResult<AppUser>> GetUsers(int id)
            {
                return  await _context.Users.FindAsync(id);
            }
    }
}