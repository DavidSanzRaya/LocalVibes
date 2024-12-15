namespace LocalVibes.Models.ViewModels
{
    public class ExploreBandsViewModel
    {
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Project> FavoriteProjects { get; set; } = new List<Project>();
    }
}
