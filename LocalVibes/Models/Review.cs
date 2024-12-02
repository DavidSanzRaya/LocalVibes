namespace LocalVibes.Models
{
    // Tabla Review que conecta Users con Project
    public class Review
    {
        public int IdReview { get; set; } // PK
        public string? ReviewText { get; set; } // AllowNull
        public DateTime? ReviewDate { get; set; } // AllowNull
        public int? Rating { get; set; } // AllowNull
        public int? IdUser { get; set; } // FK de Users. AllowNull
        public int? IdProject { get; set; } // FK de Project. AllowNull
    }
}
