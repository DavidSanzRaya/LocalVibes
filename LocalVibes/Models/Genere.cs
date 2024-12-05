using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla Genere
    public class Genere
    {
        [Key]

        public int IdGenero { get; set; } // PK
        public required string GenereName { get; set; } 
    }
}
