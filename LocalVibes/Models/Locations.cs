using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla Locations
    public class Locations
    {
        [Key]

        public int IdLocation { get; set; } // PK
        public required float Altitude { get; set; }
        public required float Latitude { get; set; }
        public required string NameLocation { get; set; } 
    }
}
