using LocalVibes.Models;

namespace LocalVibes.DTOs
{
    public class EventDTO
    {
        public int IdEvent { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public byte[]? EventImage { get; set; }
        public DateTime EventDate { get; set; } 
        public Locations Location { get; set; }
        public List<GenereMusic> GeneresMusic { get; set; } = new List<GenereMusic>();
    }
}
