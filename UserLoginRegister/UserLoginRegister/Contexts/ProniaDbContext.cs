
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using UserLoginRegister.Models;

namespace UserLoginRegister.Contexts;

public class ProniaDbContext : IdentityDbContext<appUser>
{
    public ProniaDbContext(DbContextOptions<ProniaDbContext> options) : base(options)
    {
        
    }
    public DbSet<appUser> AppUsers { get; set; } = null!;
}
