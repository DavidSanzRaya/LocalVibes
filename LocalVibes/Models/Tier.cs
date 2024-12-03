namespace LocalVibes.Models
{
    // Tabla de Tier
    public class Tier
    {
        public int IdTier { get; set; } // PK
        public required string TierName { get; set; } 
        public int? TierThreshold {  get; set; } // AllowNull 
    }
}
