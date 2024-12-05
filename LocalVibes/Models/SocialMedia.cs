using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla SocialMedia
    public class SocialMedia
    {
        [Key]

        public int IdSocialMedia {  get; set; } // PK
        public required string NameSocialMedia { get; set; } 
    }
}
