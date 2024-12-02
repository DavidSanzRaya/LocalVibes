namespace LocalVibes.Models
{
    // Tabla Locations
    public class Locations
    {
        public int IdLocation { get; set; } // PK
        public float Altitude { get; set; }
        public float Latitude { get; set; }
        public string NameLocation { get; set; } 
    }
}
