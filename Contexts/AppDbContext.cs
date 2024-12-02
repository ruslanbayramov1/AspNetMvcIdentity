using AspNetIdentity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetIdentity.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {

        // DbContext options
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }
    }
}
