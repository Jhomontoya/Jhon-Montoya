using System.ComponentModel.DataAnnotations;

namespace WebAppTienda.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El número de celular es requerido")]
        [Phone(ErrorMessage = "El número de celular no es válido")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; }
        public string Clave { get; set; }
        public string Roles { get; set; }
    }
}
