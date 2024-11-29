namespace LocalVibes.Models
{
    public class Event
    {
        public int IdEvent { get; set; }

        public int Capacity { get; set; }

        public bool IsSoldDate { get; set; }

        public int Sales { get; set; }

        public DateTime EventDate { get; set; }

        public float Price { get; set; }

        public int FKProject { get; set; }

        public int FKLocation { get; set;}  
    }
}
