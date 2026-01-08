using System.ComponentModel.DataAnnotations;
namespace PAWPAL.Models
{
    public class Shelter
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
