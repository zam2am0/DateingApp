using API.Entities;
using Microsoft.EntityFrameworkCore;
namespace API.Data;

public class DataContext: DbContext //drived from entity framework class ->DbContext
{
    public DataContext(DbContextOptions option): base(option)
    {
        
    }
    public DbSet<AppUser> Users{get;set;}
}
