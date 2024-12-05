using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    public class SignUpUserViewModel
    {
        // Imagen del Proyecto
        [DataType(DataType.Upload)]
        public byte[]? ProfileImage { get; set; }

        // Nombre de usuario
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres.")]
        public string Username { get; set; }

        // Nombre
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string FirstName { get; set; }

        // Apellido
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        public string? LastName { get; set; }

        // Correo electrónico
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string UserEmail { get; set; }

        // Teléfono
        [Phone(ErrorMessage = "El teléfono no es válido.")]
        public string? Phone { get; set; }

        // Fecha de nacimiento
        public DateTime? Birthdate { get; set; }

        // Contraseña
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [StringLength(235, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; }

        // Confirmación de contraseña
        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        // Género: ahora será un entero (ID)
        [Required(ErrorMessage = "El género es obligatorio.")]
        public int IdGenere { get; set; }
        public IEnumerable<SelectListItem> Generes { get; set; } // Lista de géneros


        // Tipo de documento: ahora será un entero (ID)
        public int? IdDocumentType { get; set; }

        // Número de documento
        [StringLength(20, ErrorMessage = "El número de documento no puede tener más de 20 caracteres.")]
        public string? DocumentNumber { get; set; }
    }
}
