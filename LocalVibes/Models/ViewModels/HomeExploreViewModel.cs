
using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    public class HomeExploreViewModel
    {
        public List<EventProject>? Events { get; set; }
        public List<GenereMusic>? Generes { get; set; }
        public List<Project>? Projects { get; set; }
        public Users? User { get; set; }
    }
}
