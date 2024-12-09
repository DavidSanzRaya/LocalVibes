using LocalVibes.DALs;
using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla EventProject
    public class EventProject
    {
        [Key]
        public int IdEvent { get; set; } // PK
        public int? Capacity { get; set; } // AllowNull
        public bool IsSoldOut { get; set; } // AllowNull
        public int? Sales { get; set; } // AllowNull
        public DateTime EventDate { get; set; } 
        public int IdProject { get; set; } // FK de Project
        public int IdLocation { get; set; } // FK de Location
        // TODO: Me falta descripcion y precio y fotoCartel y projects
        private List<GenereMusic>? _generesMusic;
        public List<GenereMusic> GeneresMusic
        {
            get
            {
                if(_generesMusic == null)
                    _generesMusic = new ProjectDAL().GetById(IdProject).GeneresMusic;

                return _generesMusic;
            }
        }

        private Locations? _location;
        public Locations Location
        {
            get
            {
                if (_location == null)
                    _location = new LocationsDAL().GetById(IdLocation);

                return _location;
            }
        }
        public List<Project> Projects { get; set; } // Hacer lazy load y cambiara en base de dato para que event pueda teneer varios projects
    }
}
