namespace LocalVibes.Models
{
    // Tabla Instrument
    public class Instrument
    {
        public int IdInstrument { get; set; } // PK
        public required string NameInstrument { get; set; } // AllowNull
    }
}
