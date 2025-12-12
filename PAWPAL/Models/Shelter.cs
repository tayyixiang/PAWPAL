using System.ComponentModel.DataAnnotations;

namespace PawPal.Models
{
    public class Shelter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        // Relationship: One Shelter has many Pets
        public virtual ICollection<Pet>? Pets { get; set; }
    }
}
