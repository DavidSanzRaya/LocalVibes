namespace LocalVibes.Models
{
    // Tabla ProjectMember que conecta Project con Member
    public class ProjectMember
    {
        public int IdMember { get; set; } // FK de Member
        public int IdProject { get; set; } // FK de Project
    }
}
