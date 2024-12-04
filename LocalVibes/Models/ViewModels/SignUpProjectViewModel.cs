namespace LocalVibes.Models.ViewModels
{
    public class SignUpProjectViewModel
    {
        public string ProjectName { get; set; }
        public string Biography { get; set; }
        public DateTime? FormationDate { get; set; }
        public int IdUserAdmin { get; set; }  // ID del usuario administrador (FK)
        public IFormFile ProjectImage { get; set; }  // Imagen del proyecto (opcional)
    }
}
