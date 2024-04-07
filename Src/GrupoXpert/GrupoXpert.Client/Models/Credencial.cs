using System.ComponentModel.DataAnnotations;

namespace GrupoXpert.Client.Models
{
    public class Credencial(string usuario, string clave)
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Usuario { get; set; } = usuario;

        [Required(ErrorMessage = "El usuario es requerido")]
        public string Clave { get; set; } = clave;
    }
}
