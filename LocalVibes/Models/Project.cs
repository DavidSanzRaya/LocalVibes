using LocalVibes.Tools;
using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla Project
    public class Project
    {
        [Key]

        public int IdProject { get; set; } // PK 
        [Name]
        public required string ProjectName { get; set; }
        public string? Biography { get; set; } // AllowNull
        public DateTime? FormationDate { get; set; } // AllowNull
        public byte[]? ProjectImage {  get; set; } // AllowNull
        public int IdUsersAdmin {  get; set; } // FK de Users. AllowNull

        public List<ProjectGenereMusic>? generesMusic { get; set; } // Lista de ProjectGenereMusic. AllowNull

        public List<ProjectSocialMedia>? socialMedias { get; set; } // Lista de ProjectSocialMedia. AllowNull

        public List<MemberOfProject>? membersOfProjects { get; set; } // Lista de MemberOfProjects. AllowNull

        public List<EventProject>? eventsProject { get; set; } // Lista de EventProject. AllowNull

        public List<Review>? reviews { get; set; } // Lista de Review. AllowNull

    }
}
