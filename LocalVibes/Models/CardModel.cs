namespace LocalVibes.Models
{
    public class CardModel
    {
        public byte[]? Image { get; set; } 
        public string Title { get; set; } = "Title";
        public string Subtitle { get; set; } = "Subtitle";
        public string Description { get; set; } = "Description";
        public bool Shadow { get; set; } = false;
    }
}

