using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAWPAL.Data;
using PAWPAL.Models;

namespace PAWPAL.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Pet> Pet { get; set; } = default!;
        public DbSet<Shelter> Shelter { get; set; } = default!;
        public DbSet<Appointment> Appointment { get; set; } = default!;
    }
}
