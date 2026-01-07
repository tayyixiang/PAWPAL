using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawPal.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [StringLength(200)]
        public string Notes { get; set; } = string.Empty;

        // Status: "Pending", "Confirmed", "Rejected"
        public string Status { get; set; } = "Pending";

        // Relationship: Appointment is for ONE Pet
        [Required]
        public int PetId { get; set; }

        [ForeignKey("PetId")]
        public virtual Pet? Pet { get; set; }

        // Relationship: Appointment is made by ONE User (Adopter)
        // We use the Identity User ID (Guid string)
        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}