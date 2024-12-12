using LocalVibes.Models;

namespace LocalVibes.DTOs
{
    public class ProjectDTO
    {
        public int IdProject { get; set; } 
        public required string ProjectName { get; set; }
        public string? Biography { get; set; }
        public DateTime? FormationDate { get; set; }
        public byte[]? ProjectImage {  get; set; }
        public List<GenereMusic> GeneresMusic {  get; set; }
    }
}
