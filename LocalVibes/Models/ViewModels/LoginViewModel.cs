using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    // Modelo de vista utilizado para manejar el proceso de inicio de sesión.
    public class LoginViewModel
    {
        // Propiedad que representa el nombre de usuario.
        [Required(ErrorMessage = "El nombre de usuaro es obligatorio.")]
        [Display(Name = "User name")]
        public string Username { get; set; }

        // Propiedad que representa la contraseña.
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")] 
        public string Password { get; set; }

        // Propiedad para indicar si la cuenta pertenece a una banda musical.
        [Display(Name = "Band account")]
        public bool IsABand { get; set; }
    }
}
