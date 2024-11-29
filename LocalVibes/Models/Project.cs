namespace LocalVibes.Models
{
    public class Project
    {
        public int IdProject { get; set; }
        public string ProjectName { get; set; }
        public string? Biography { get; set; }
        public DateTime? FormationDate { get; set; }
        public byte[]? ProjectImage { get; set; }
        public int FKUser { get; set; }
    }
}
