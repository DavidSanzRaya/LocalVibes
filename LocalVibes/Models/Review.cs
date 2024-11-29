namespace LocalVibes.Models
{
    public class Review
    {
        public int FKUser { get; set; }

        public int FKProject { get; set; }

        public string Text { get; set; }

        public DateTime ReviewDataTime { get; set; }

        public int Rating { get; set; }
    }
}
