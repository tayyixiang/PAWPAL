using System.ComponentModel.DataAnnotations;

namespace PAWPAL.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";

        [Required]
        public string Species { get; set; } = "Dog"; // FOR FILTERS

        public string Breed { get; set; } = "";

        [Range(0, 30, ErrorMessage = "Age must be between 0 and 30")]
        public int Age { get; set; }

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = "Available"; // Available, Adopted, Pending

        // Relationship
        public int ShelterId { get; set; }
    }
}