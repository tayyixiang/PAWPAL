using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawPal.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name is too long.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Breed { get; set; } = string.Empty;

        [Range(0, 30, ErrorMessage = "Age must be between 0 and 30.")]
        public int Age { get; set; }

        public string? ImageUrl { get; set; } // We will store the file path here

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        // Relationship: A Pet belongs to a Shelter
        [Required]
        public int ShelterId { get; set; }

        [ForeignKey("ShelterId")]
        public virtual Shelter? Shelter { get; set; }

        // Status tracking (Available, Adopted) - Important for Logic
        public string Status { get; set; } = "Available";
    }
}