using Microsoft.EntityFrameworkCore;
using PAWPAL.Data;
using PAWPAL.Models;

namespace PAWPAL.Services
{
    public class ShelterService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public ShelterService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Shelter>> GetSheltersAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Shelter.ToListAsync();
        }

        public async Task<Shelter?> GetShelterByIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Shelter.FindAsync(id);
        }

        public async Task AddShelterAsync(Shelter shelter)
        {
            using var context = _factory.CreateDbContext();
            context.Shelter.Add(shelter);
            await context.SaveChangesAsync();
        }

        public async Task UpdateShelterAsync(Shelter shelter)
        {
            using var context = _factory.CreateDbContext();
            context.Shelter.Update(shelter);
            await context.SaveChangesAsync();
        }

        public async Task DeleteShelterAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            var shelter = await context.Shelter.FindAsync(id);
            var hasPets = await context.Pet.AnyAsync(p => p.ShelterId == id);

            if (shelter != null && !hasPets)
            {
                context.Shelter.Remove(shelter);
                await context.SaveChangesAsync();
            }
        }
    }
}
