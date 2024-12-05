using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla GenereMusic
    public class GenereMusic
    {
        [Key]

        public int IdGenereMusic { get; set; } // PK
        public required string GenereMusicName { get; set; }
    }
}
