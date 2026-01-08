using PAWPAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAWPAL.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; } = DateTime.Now.AddDays(1);

        [Required]
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Rejected, Completed

        // ADOPTER DETAILS
        [Required(ErrorMessage = "Please enter your name")]
        public string AdopterName { get; set; } = "";

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress]
        public string AdopterEmail { get; set; } = "";

        //RELATIONS
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual PAWPAL.Data.ApplicationUser? User { get; set; }

        public int PetId { get; set; }

        [ForeignKey("PetId")]
        public virtual Pet? Pet { get; set; }
    }
}