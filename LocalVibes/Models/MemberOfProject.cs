using LocalVibes.DALs;
using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla MemberOfProject
    public class MemberOfProject
    {
        [Key]

        public int IdMember { get; set; } // PK
        public required string MemberName { get; set; } 
        public string? ArtisticName { get; set; } // AllowNull
        public byte[]? MemberImage { get; set; } 

        private List<Instrument>? _instruments;
        public List<Instrument> Instruments
        {
            get
            {
                if (_instruments == null)
                    _instruments = new InstrumentDAL().GetMemberInstrumentsByMemberId(IdMember);

                return _instruments;
            }
            set
            {
                _instruments = value;
            }
        }

    }
}
