using System.Globalization;

namespace LocalVibes.Models
{
    // Tabla de ProjectSocialMedia que conecta Project con SocialMedia
    public class ProjectSocialMedia
    {
        public int IdSocialMedia { get; set; } // FK de SocialMedia
        public int IdProject { get; set; } // FK de Project
    }
}
