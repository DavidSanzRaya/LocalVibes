using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    public class SignUpProjectViewModel
    {
        // ID del Proyecto (solo lectura)
        public int IdProject { get; set; }

        // Nombre del Proyecto
        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del proyecto no puede tener más de 100 caracteres.")]
        public string ProjectName { get; set; }

        // Biografía
        [StringLength(1000, ErrorMessage = "La biografía no puede tener más de 1000 caracteres.")]
        public string? Biography { get; set; }

        // Fecha de Formación
        [Required(ErrorMessage = "La fecha de formación es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "El formato de la fecha no es válido.")]
        public DateTime? FormationDate { get; set; }

        // Imagen del Proyecto
        [DataType(DataType.Upload)]
        public IFormFile? ProjectImage { get; set; } // Usar IFormFile para manejar archivos subidos

        // Administrador del Proyecto (ID de Usuario)
        [Required(ErrorMessage = "El administrador del proyecto es obligatorio.")]
        public string UsernameAdmin { get; set; } // Cambiado a string para reflejar nombres de usuario

        // Género Musical
        public int IdGenereMusical { get; set; }
        [Required(ErrorMessage = "El género musical es obligatorio.")]

        public IEnumerable<SelectListItem> SelectedGeneresMusic { get; set; } = new List<SelectListItem>();

        // Redes Sociales (Opcional)
        public List<int>? SelectedSocialMediaIds { get; set; } = new List<int>();

        // Miembros del Proyecto (Opcional)
        public List<int>? SelectedMembers { get; set; } = new List<int>();

        // Eventos Asociados (Opcional)
        public List<int>? SelectedEvents { get; set; } = new List<int>();

        // Reseñas (Opcional)
        public List<int>? SelectedReviews { get; set; } = new List<int>();
    }
}
