namespace LocalVibes.Models
{
    // Tabla ProjectGenereMusic que conecta Project con GenereMusic
    public class ProjectGenereMusic
    {
        public int IdProject { get; set; } // FK de Project
        public int IdGenereMusic { get; set; } // FK de GenereMusic
    }
}
