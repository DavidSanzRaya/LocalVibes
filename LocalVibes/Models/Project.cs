namespace LocalVibes.Models
{
    // Tabla Project
    public class Project
    {
        public int IdProject { get; set; } // PK 
        public string ProjectName { get; set; }
        public string? Biography { get; set; } // AllowNull
        public DateOnly? FormationDate { get; set; } // AllowNull
        public byte[]? ProjectImage {  get; set; } // AllowNull
        public int? UserAdmin {  get; set; } // FK de Users. AllowNull
    }
}
