using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    public class SignUpProjectViewModel
    {
        // ID del Proyecto (sólo lectura)
        public int IdProject { get; set; }

        // Nombre del Proyecto
        [Required(ErrorMessage = "El nombre del proyecto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del proyecto no puede tener más de 100 caracteres.")]
        public string ProjectName { get; set; }

        // Biografía
        [StringLength(1000, ErrorMessage = "La biografía no puede tener más de 1000 caracteres.")]
        public string? Biography { get; set; }

        // Fecha de Formación
        [DataType(DataType.Date)]
        public DateTime? FormationDate { get; set; }

        // Imagen del Proyecto
        [DataType(DataType.Upload)]
        public byte[]? ProjectImage { get; set; }

        // Administrador del Proyecto (ID de Usuario)
        [Required(ErrorMessage = "El administrador del proyecto es obligatorio.")]
        public int IdUsersAdmin { get; set; }

        // Géneros Musicales
        public List<int>? SelectedGeneresMusic { get; set; }

        // Redes Sociales
        public List<int>? SelectedSocialMediaIds { get; set; }

        // Miembros del Proyecto
        public List<int>? SelectedMembers { get; set; }

        // Eventos Asociados
        public List<int>? SelectedEvents { get; set; }

        // Reseñas
        public List<int>? SelectedReviews { get; set; }
    }
}
