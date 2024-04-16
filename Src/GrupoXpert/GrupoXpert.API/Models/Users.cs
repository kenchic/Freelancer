using System.ComponentModel.DataAnnotations;

namespace GrupoXpert.Api.Models
{
    public class Users
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
