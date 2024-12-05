using LocalVibes.DALs;
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

        private List<GenereMusic>? _generesMusic;
        public List<GenereMusic> GeneresMusic 
        {
            get
            {
                if( _generesMusic == null )
                    _generesMusic = new GenereMusicDAL().GetGenresByProjectId(IdProject);

                return _generesMusic;
            }

            set
            {
                _generesMusic = value;
            }
        
        } // Lista de GenereMusic. AllowNull

        public List<ProjectSocialMedia>? socialMedias { get; set; } // Lista de ProjectSocialMedia. AllowNull

        public List<MemberOfProject>? membersOfProjects { get; set; } // Lista de MemberOfProjects. AllowNull

        public List<EventProject>? eventsProject { get; set; } // Lista de EventProject. AllowNull

        public List<Review>? reviews { get; set; } // Lista de Review. AllowNull

    }
}
