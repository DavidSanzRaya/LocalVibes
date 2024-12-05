namespace LocalVibes.Models
{
    // Tabla MemberInstrument que conecta Instrument con Member
    public class MemberInstrument
    {

        public int IdInstrument { get; set; } // FK de Instrument
        public int IdMember { get; set; } // FK de Member
    }
}
