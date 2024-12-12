using LocalVibes.DALs;
using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla EventProject
    public class EventProject
    {
        [Key]
        public int IdEvent { get; set; } // PK
        public string EventTitle { get; set; } // Nombre de Evento
        public string EventDescription { get; set; } // descripcion del evento
        public byte[]? EventImage { get; set; } // Imagen del evento
        public int? Capacity { get; set; } // AllowNull
        public bool IsSoldOut { get; set; } // AllowNull
        public int? Sales { get; set; } // AllowNull
        public DateTime EventDate { get; set; } 
        public int IdProject { get; set; } // FK de Project
        private Project _project { get; set; }
        public Project Project 
        { 
            get {
                if (_project == null)
                    _project = new ProjectDAL().GetById(IdProject);

                return _project;
            }
            set
            {
                _project = value;
            } 
        }
        public int IdLocation { get; set; } // FK de Location

        private Locations? _location;
        public Locations Location
        {
            get
            {
                if (_location == null)
                    _location = new LocationsDAL().GetById(IdLocation);

                return _location;
            }

            set
            {
                _location = value;
            }
        }
        public List<Project>? Projects { get; set; } // Hacer lazy load y cambiara en base de dato para que event pueda teneer varios projects
    }
}
