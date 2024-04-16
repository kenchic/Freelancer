using System.ComponentModel.DataAnnotations;

namespace GrupoXpert.Client.Models
{
    public class AuthorizationRequest
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        public string Password { get; set; }
    }
}
