namespace LocalVibes.Models
{
    // Tabla de Users
    public class Users
    {
        public int IdUser { get; set; } // PK
        public string UserName { get; set; } // AllowNull
        public string? FirstName { get; set; } // AllowNull
        public string? LastName { get; set; } // AllowNull
        public string UserEmail { get; set; } // AllowNull
        public string? Phone { get; set; } // AllowNull
        public byte[] PasswordHash { get; set; } 
        public byte[] PasswordSalt { get; set; }
        public DateTime? Birthdate {  get; set; } // AllowNull
        public byte[]? ProfileImage {  get; set; } // AllowNull
        public string? DocumentNumber {  get; set; } // AllowNull
        public int? UserPoints { get; set; } // AllowNull
        public DateTime DateRegister { get; set; } // AllowNull
        public int? IdDocumentType { get; set; } // FK de DocumentType. AllowNull
        public int IdGenere { get; set; } // FK de Genere. 
        public int IdTier { get; set; } // FK de Tier

        public List<UserFavoriteProject>? userFavoriteProjects { get; set; } // Lista de UserFavoriteProject. AllowNull

        public List<GenereMusic> userGeneresMusic { get; set; } // Lista de UserGenereMusic. AllowNull

        public List<Ticket>? tickets { get; set; } // Lista de tickets. AllowNull
    }
}
