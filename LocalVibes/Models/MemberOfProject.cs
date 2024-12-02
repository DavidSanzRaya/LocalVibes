namespace LocalVibes.Models
{
    // Tabla MemberOfProject
    public class MemberOfProject
    {
        public int IdMember { get; set; } // PK
        public required string MemberName { get; set; } 
        public string? ArtisticName { get; set; } // AllowNull
        public byte[]? MemberImage { get; set; } // AllowNull

        public List<MemberInstrument>? MemberInstruments { get; set; } // Lista de MemberInstrument. AllowNull
    }
}
