using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    public class SignUpViewModel
    {
        public UserRegistrationData User { get; set; } = new UserRegistrationData();
        public BandRegistrationData Band { get; set; } = new BandRegistrationData();

        public class UserRegistrationData
        {
            // Nombre de Usuario
            [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
            [StringLength(50, ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres.")]
            [Display(Name = "Username")]
            public string Username { get; set; }

            // Nombre
            [Required(ErrorMessage = "El nombre es obligatorio.")]
            [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            // Apellido
            [Required(ErrorMessage = "El apellido es obligatorio.")]
            [StringLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres.")]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            // Correo Electrónico
            [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
            [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
            [Display(Name = "Email")]
            public string UserEmail { get; set; }

            // Teléfono
            [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
            [Display(Name = "Phone")]
            public string? Phone { get; set; }

            // Contraseña
            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            // Confirmación de Contraseña
            [Required(ErrorMessage = "La confirmación de contraseña es obligatoria.")]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            public string ConfirmPassword { get; set; }

            // Fecha de Nacimiento
            [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
            [DataType(DataType.Date, ErrorMessage = "El formato de la fecha no es válido.")]
            [Display(Name = "Birthdate")]
            public DateTime Birthdate { get; set; }

            // Género
            [Display(Name = "Gender")]
            [Required(ErrorMessage = "El género es obligatorio.")]
            public int IdGenere { get; set; }
            public IEnumerable<SelectListItem> Generes { get; set; } = new List<SelectListItem>();

            // Tipo de Documento
            public int? IdDocumentType { get; set; }
            public IEnumerable<SelectListItem> Documents { get; set; } = new List<SelectListItem>();

            // Número de Documento
            [StringLength(20, ErrorMessage = "El número de documento no puede tener más de 20 caracteres.")]
            [Display(Name = "Document number")]
            public string? DocumentNumber { get; set; }

            // Imagen de Perfil
            [DataType(DataType.Upload)]
            [Display(Name = "Profile image")]
            public IFormFile? ProfileImage { get; set; } // Cambiado de byte[] a IFormFile para manejo directo del archivo subido
        }

        public class BandRegistrationData
        {
            // ID del Proyecto (propiedad de solo lectura, asignada automáticamente al crear el proyecto).
            public int IdProject { get; set; }

            // Propiedad que representa el nombre del Proyecto (Banda o artista)
            [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
            [StringLength(100, ErrorMessage = "El nombre del proyecto no puede tener más de 100 caracteres.")]
            [Display(Name = "Name of the band")]
            public string ProjectName { get; set; }

            // Propiedad que representa la biografia
            [StringLength(1000, ErrorMessage = "La biografía no puede tener más de 1000 caracteres.")]
            [Display(Name = "Biography")]
            public string? Biography { get; set; }

            // Propiedad que representa la fecha de Formación
            [DataType(DataType.Date, ErrorMessage = "El formato de la fecha no es válido.")]
            [Display(Name = "Formation date")]
            public DateTime? FormationDate { get; set; }

            // Propiedad que representa el nombre de usuario del Admin
            [Required(ErrorMessage = "El administrador del proyecto es obligatorio.")]
            [Display(Name = "Username of the admin account")]
            public string UsernameAdmin { get; set; }

            [Display(Name = "Genres")]
            [Required(ErrorMessage = "Debes seleccionar al menos un género.")]
            public List<int> SelectedGenres { get; set; } = new List<int>();

            public IEnumerable<SelectListItem> SelectedGeneresMusic { get; set; } = new List<SelectListItem>();

            // Propiedad que representa la imagen
            [DataType(DataType.Upload)]
            [Display(Name = "Profile image")]
            public IFormFile? ProjectImage { get; set; }
        }
    }
}
