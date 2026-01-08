using Microsoft.EntityFrameworkCore;
using PAWPAL.Data;
using PAWPAL.Models;

namespace PAWPAL.Services
{
    public class PetService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public PetService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Pet>> GetPetsAsync(string? searchTerm, string? species, string? breed, int? minAge, int? shelterId)
        {
            using var context = _factory.CreateDbContext();
            var query = context.Pet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(p => p.Name.Contains(searchTerm));

            if (!string.IsNullOrWhiteSpace(species) && species != "All")
                query = query.Where(p => p.Species == species);

            if (!string.IsNullOrWhiteSpace(breed) && breed != "All")
                query = query.Where(p => p.Breed == breed);

            return await query.ToListAsync();
        }

        public async Task<List<string>> GetBreedsAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Pet.Select(p => p.Breed).Distinct().ToListAsync();
        }

        public async Task<Pet?> GetPetByIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Pet.FindAsync(id);
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
            context.Pet.Update(pet);
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
