using Microsoft.AspNetCore.Identity;

namespace LocalVibes.Models
{
    // Tabla UserFavoriteProject que conecta Users con Project
    public class UserFavoriteProject
    {
        public int idUser {  get; set; } // FK de Users
        public int idProject { get; set; } // FK de Project
    }
}
