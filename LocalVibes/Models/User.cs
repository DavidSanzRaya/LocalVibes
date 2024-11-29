namespace LocalVibes.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserEmail { get; set; }
        public string? Phone { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[]? ProfileImage { get; set; }
        public string? DocumentNumber { get; set; }
        public int UserPoints { get; set; }
        public int FKTier { get; set; }
        public DateTime RegisterDate { get; set; }
        public int? FKDocumentType { get; set; }
        public int FKGenere { get; set ; }
    }
}
