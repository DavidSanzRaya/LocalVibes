using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models
{
    // Tabla DocumentType
    public class DocumentType
    {
        [Key]
        public int IdDocumentType { get; set; } // PK
        public required string NameDocumentType { get; set; } 
    }
}
