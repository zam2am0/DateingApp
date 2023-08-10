using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController] //controller
[Route("api/[controller]")] // /api/user
public class UserController: ControllerBase
{
    private readonly DataContext _context;
    public UserController(DataContext context)
    {
        _context = context;

    }

    [HttpGet] //endpoint
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users =await _context.Users.ToListAsync();

        return users; //return the list of usrs
    }

    [HttpGet("{id}")] // /api/users/2
        public async Task<ActionResult<AppUser>> GetUsers(int id)
        {
            return  await _context.Users.FindAsync(id);
        }
}