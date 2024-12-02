namespace LocalVibes.Models
{
    // Tabla de Tier
    public class Tier
    {
        public int IdTier { get; set; } // PK
        public string? TierName { get; set; } // AllowNull
        public int? TierThreshold {  get; set; } // AllowNull 
    }
}
