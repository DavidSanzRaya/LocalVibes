using LocalVibes.DALs;
using System.ComponentModel.DataAnnotations;
using LocalVibes.Tools;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalVibes.Models
{
    // Tabla de Users
    public class Users
    {
        [Key]
        public int IdUsers { get; set; } // PK
        [Name]
        public string UserName { get; set; } // AllowNull
        public string FirstName { get; set; } // AllowNull
        public string? LastName { get; set; } // AllowNull
        public string UserEmail { get; set; } // AllowNull
        public string? Phone { get; set; } // AllowNull
        public byte[] PasswordHash { get; set; } 
        public byte[] PasswordSalt { get; set; }
        public DateTime? Birthdate {  get; set; } // AllowNull
        public byte[]? ProfileImage {  get; set; } // AllowNull
        public string? DocumentNumber {  get; set; } // AllowNull
        public int? UserPoints { get; set; } // AllowNull
        public DateTime DateRegister { get; set; } // AllowNull
        public int? IdDocumentType { get; set; } // FK de DocumentType. AllowNull
        public int IdGenere { get; set; } // FK de Genere. 
        public int IdTier { get; set; } // FK de Tier


        private List<Project>? _userFavoriteProjects;
        public List<Project> UserFavoriteProjects
        {
            get
            {
                if (_userFavoriteProjects == null)
                    _userFavoriteProjects = new UserDAL().GetFavoriteProjectsByUserId(IdUsers);

                return _userFavoriteProjects;
            }
            set
            {
                _userFavoriteProjects = value;               
            }
        }

        public void AddFavoriteProject(Project project)
        {
			UserFavoriteProjects.Add(project);

			new UsersFavoriteProjectDAL().Add(new UserFavoriteProject
            {
                IdProject = project.IdProject,
                IdUsers = this.IdUsers
            });
		}

		public void RemoveFavoriteProject(int idProject)
		{
			// Eliminar el proyecto de la lista en memoria
			UserFavoriteProjects.RemoveAll(p => p.IdProject == idProject);

			// Eliminar el registro de la tabla asociativa en la base de datos
			var primaryKeyValues = new Dictionary<string, object>
	        {
		        { "IdProject", idProject },
		        { "IdUsers", this.IdUsers }
	        };

			// Llamar al método Delete general
			new UsersFavoriteProjectDAL().Delete(primaryKeyValues);
		}


		private List<GenereMusic>? _userGeneresMusic;
        public List<GenereMusic> UserGeneresMusic
        {
            get
            {
                if(_userGeneresMusic == null)
                    _userGeneresMusic = new GenereMusicDAL().GetGenresByUserId(IdUsers);
                
                return _userGeneresMusic;
            }
            set
            {
                _userGeneresMusic = value;
            }
        } // Lista de UserGenereMusic. AllowNull

        public List<Ticket>? Tickets { get; set; } // Lista de tickets. AllowNull

        private Project? _project;
        [NotMapped]
        public Project Project
        {
            get
            {
                if (_project == null)
                    _project = new UserDAL().GetAdminProjectByUserId(IdUsers);

                return _project;
            }
            set
            {
                _project = value;
            }
        } // Lista de UserGenereMusic. AllowNull
    }
}
