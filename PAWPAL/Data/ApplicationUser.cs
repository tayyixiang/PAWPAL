using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAWPAL.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FullName { get; set; } = "";

        public int? ShelterId { get; set; }

        [ForeignKey("ShelterId")]
        public virtual PAWPAL.Models.Shelter? Shelter { get; set; }
    }
}

