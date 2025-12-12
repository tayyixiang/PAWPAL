using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PAWPAL.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<PawPal.Models.Pet> Pet { get; set; } = default!;
        public DbSet<PawPal.Models.Shelter> Shelter { get; set; } = default!;
    }
}
