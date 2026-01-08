using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAWPAL.Models;

namespace PAWPAL.Data
{
 
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        // YIXIANG'S ENTITIES
        public DbSet<Pet> Pet { get; set; } = default!;
        public DbSet<Shelter> Shelter { get; set; } = default!;
        public DbSet<Appointment> Appointment { get; set; } = default!;

    }
}
