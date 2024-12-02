using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    public class PruebaViewModel
    {
        public List<Users> Usuarios { get; set; } = new List<Users>(); //Lista de usuarios

        public Users? user { get; set; }

        public List<UserFavoriteProject>? userFavoriteProjects { get; set; } = new List<UserFavoriteProject>();  // Lista de UserFavoriteProject. AllowNull

        public List<UserGenereMusic>? userGeneresMusic { get; set; } = new List<UserGenereMusic>(); // Lista de UserGenereMusic. AllowNull

        public List<Ticket>? tickets { get; set; } = new List<Ticket>(); // Lista de tickets. AllowNull
    }
}
