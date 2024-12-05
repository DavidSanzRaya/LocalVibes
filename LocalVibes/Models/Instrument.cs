using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla Instrument
    public class Instrument
    {
        [Key]

        public int IdInstrument { get; set; } // PK
        public required string NameInstrument { get; set; } // AllowNull
    }
}
