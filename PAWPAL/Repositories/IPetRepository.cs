using PAWPAL.Models;

namespace PAWPAL.Repositories
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetAllPetsAsync();
        Task<Pet?> GetPetByIdAsync(int id);
        Task AddPetAsync(Pet pet);
        Task UpdatePetAsync(Pet pet);
        Task DeletePetAsync(int id);
    }
}
