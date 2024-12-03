using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre de usuaro es obligatorio.")]
        [Display(Name = "User name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La direccion email es obligatoria.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email adress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Band account")]
        public bool IsABand { get; set; }
    }
}
