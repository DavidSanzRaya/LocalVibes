

namespace LocalVibes.Models.ViewModels
{
    public class ProfileUserViewModel
    {
        public Users? User { get; set; }
        public List<EventProject>? Eventos { get; set; }
        public List<Project>? ProjectsFavotire { get; set; } 
    }
}
