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

        // TODO: Algunos atributos estan en mayus y otros da toc XDXD juasjuas

        private List<Review>? _reviews;
        public List<Review> Reviews
    {
            get
            {
                if (_reviews == null)
                {
                    _reviews = new ReviewDAL().GetReviewsByProjectId(IdProject);
                }
                return _reviews;
            }

            set
            {
                _reviews = value;
            }
        }
        public List<ProjectSocialMedia>? SocialMedias { get; set; } = new List<ProjectSocialMedia>();
        private List<MemberOfProject>? _members;
        public List<MemberOfProject> MembersOfProjects
        {
            get
            {
                if (_members == null)
                {
                    _members = new ProjectDAL().GetMembersByProjectId(IdProject);
                }
                return _members;
            }

            set
            {
                _members = value;
            }
        }
        
        public List<EventProject>? EventsProject { get; set; } = new List<EventProject>();

    }
}
