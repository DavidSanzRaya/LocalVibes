namespace LocalVibes.Models
{
    public class Member
    {
        public int IdMember { get; set; }
        public string MemberName { get; set; }
        public string? ArtisticName { get; set; }
        public byte[]? MemberImage { get; set; }
        public int FKRolInstrument { get; set; }
    }
}
