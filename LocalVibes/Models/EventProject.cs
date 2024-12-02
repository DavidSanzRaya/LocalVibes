namespace LocalVibes.Models
{
    // Tabla EventProject
    public class EventProject
    {
        public int IdEvent { get; set; } // PK
        public int? Capacity { get; set; } // AllowNull
        public bool IsSoldOut { get; set; } // AllowNull
        public int? Sales { get; set; } // AllowNull
        public DateTime? EventDate { get; set; } // AllowNull
        public int IdProject { get; set; } // FK de Project
        public int IdLocation { get; set; } // FK de Location
    }
}
