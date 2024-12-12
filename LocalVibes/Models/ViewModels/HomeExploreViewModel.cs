using System.ComponentModel.DataAnnotations;
using LocalVibes.DTOs;

namespace LocalVibes.Models.ViewModels
{
    public class HomeExploreViewModel
    {
        public List<EventDTO>? Events { get; set; }
        public List<GenereMusic>? Generes { get; set; }
        public List<ProjectDTO>? Projects { get; set; }
        public Users? User { get; set; }
    }
}
