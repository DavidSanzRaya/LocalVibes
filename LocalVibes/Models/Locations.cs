using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla Locations
    public class Locations
    {
        [Key]

        public int IdLocation { get; set; } // PK
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
        public required string NameLocation { get; set; } 
    }
}
