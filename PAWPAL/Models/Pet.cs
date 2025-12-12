using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawPal.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required]
        public string Breed { get; set; }

        [Range(0, 25)]
        public int Age { get; set; }

        public string? ImageUrl { get; set; }

        public string Description { get; set; }

        public int ShelterId { get; set; }

        [ForeignKey("ShelterId")]
        public virtual Shelter? Shelter { get; set; }
    }
}