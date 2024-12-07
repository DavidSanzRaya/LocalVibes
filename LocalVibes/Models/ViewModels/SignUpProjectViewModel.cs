using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    // Modelo de vista para el registro de un proyecto.
    public class SignUpProjectViewModel
    {
        // ID del Proyecto (propiedad de solo lectura, asignada automáticamente al crear el proyecto).
        public int IdProject { get; set; }

        // Propiedad que representa el nombre del Proyecto (Banda o artista)
        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del proyecto no puede tener más de 100 caracteres.")]
        public string ProjectName { get; set; }

        // Propiedad que representa la biografia
        [StringLength(1000, ErrorMessage = "La biografía no puede tener más de 1000 caracteres.")]
        public string? Biography { get; set; }

        // Propiedad que representa la fecha de Formación
        [Required(ErrorMessage = "La fecha de formación es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "El formato de la fecha no es válido.")]
        public DateTime? FormationDate { get; set; }

        // Propiedad que representa el nombre de usuario del Admin
        [Required(ErrorMessage = "El administrador del proyecto es obligatorio.")]
        public string UsernameAdmin { get; set; }

        // Propiedad que representa el género Musical
        public int IdGenereMusical { get; set; }
        [Required(ErrorMessage = "El género musical es obligatorio.")]

        // Lista de géneros musicales disponibles para seleccionar.
        public IEnumerable<SelectListItem> SelectedGeneresMusic { get; set; } = new List<SelectListItem>();

        // Propiedad que representa la imagen
        [DataType(DataType.Upload)]
        public IFormFile? ProjectImage { get; set; }
    }
}
