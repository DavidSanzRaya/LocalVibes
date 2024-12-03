using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    public class SignUpUserViewModel
    {
        // Nombre de usuario
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres.")]
        public string Username { get; set; }

        // Nombre
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string FirstName { get; set; }

        // Apellido
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        public string LastName { get; set; }

        // Correo electrónico
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string UserEmail { get; set; }

        // Teléfono
        [Phone(ErrorMessage = "El teléfono no es válido.")]
        public string Phone { get; set; }

        // Fecha de nacimiento
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime Birthdate { get; set; }

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

        // Tipo de documento: ahora será un entero (ID)
        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        public int IdDocumentType { get; set; }

        // Número de documento
        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [StringLength(20, ErrorMessage = "El número de documento no puede tener más de 20 caracteres.")]
        public string DocumentNumber { get; set; }
    }
}
