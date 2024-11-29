namespace LocalVibes.Models
{
    public class Ticket
    {
        public int IdTicket { get; set; }
        public string Sit { get; set; }

        public bool IsValidated { get; set; }

        public int FKEvent { get; set; }

        public string FKUser { get; set; }
    }
}
