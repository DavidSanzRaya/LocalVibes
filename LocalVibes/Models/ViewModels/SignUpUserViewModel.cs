using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    // Modelo de vista para el registro de un usuario.
    public class SignUpUserViewModel
    {
        // Propiedad que representa el nombre de usuario (Nickname)
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres.")]
        public string Username { get; set; }

        // Propiedad que representa el nombre real de un usuario
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string FirstName { get; set; }

        // Propiedad que representa el apellido de un usuario
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres.")]
        public string LastName { get; set; }

        // Propiedad que representa el mail
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string UserEmail { get; set; }

        // Propiedad que representa un telefono
        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        public string? Phone { get; set; }

        // Propiedad que representa la contraseña
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Propiedad que representa la confirmación de Contraseña
        [Required(ErrorMessage = "La confirmación de contraseña es obligatoria.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        // Propiedad que representra la fecha de Nacimiento
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "El formato de la fecha no es válido.")]
        public DateTime Birthdate { get; set; }

        // Propiedad que represenra el género
        public int IdGenere { get; set; }
        [Required(ErrorMessage = "El género es obligatorio.")]

        // Lista de géneros disponibles para seleccionar.
        public IEnumerable<SelectListItem> Generes { get; set; } = new List<SelectListItem>();

        // Propiedad que representa el tipo de Documento
        public int IdDocumentType { get; set; }
        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]

        // Lista de tipos de documentos disponibles para seleccionar.
        public IEnumerable<SelectListItem> Documents { get; set; } = new List<SelectListItem>();

        // Propiedad que representra el número de Documento
        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [StringLength(20, ErrorMessage = "El número de documento no puede tener más de 20 caracteres.")]
        public string DocumentNumber { get; set; }

        // Propiedad que representa la imagen de Perfil
        [DataType(DataType.Upload)]
        public IFormFile? ProfileImage { get; set; } 
    }
}
