namespace LocalVibes.Models
{
    // Tabla Ticket que une Event y Users
    public class Ticket
    {
        public required string Sit { get; set; } 
        public bool? IsValidated { get; set; } // AllowNull
        public int IdUser { get; set; } // FK de Users
        public int IdEvent { get; set; } // FK de Event
    }
}
