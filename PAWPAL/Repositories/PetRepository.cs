using Microsoft.EntityFrameworkCore;
using PAWPAL.Data;
using PAWPAL.Models;

namespace PAWPAL.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public PetRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Pet>> GetAllPetsAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Pet.ToListAsync();
        }

        public async Task<Pet?> GetPetByIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Pet.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPetAsync(Pet pet)
        {
            using var context = _factory.CreateDbContext();
            context.Pet.Add(pet);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePetAsync(Pet pet)
        {
            using var context = _factory.CreateDbContext();
            context.Entry(pet).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeletePetAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            var pet = await context.Pet.FindAsync(id);
            if (pet != null)
            {
                context.Pet.Remove(pet);
                await context.SaveChangesAsync();
            }
        }
    }
}
