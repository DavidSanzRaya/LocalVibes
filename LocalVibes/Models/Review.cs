using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla Review que conecta Users con Project
    public class Review
    {
        [Key]

        public int IdReview { get; set; } // PK
        public string? ReviewText { get; set; } // AllowNull
        public DateTime ReviewDate { get; set; } //GetDate();
        public int Rating { get; set; } // 1-5
        public int IdUser { get; set; } // FK de Users. 
        public int IdProject { get; set; } // FK de Project. 
    }
}
