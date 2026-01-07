using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;

namespace PawPal.Models
{
    public class Shelter
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Shelter Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        // Navigation Property: One Shelter has many Pets
        public virtual ICollection<Pet>? Pets { get; set; }
    }
}
