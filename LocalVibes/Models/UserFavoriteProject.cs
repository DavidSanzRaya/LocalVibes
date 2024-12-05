using Microsoft.AspNetCore.Identity;

namespace LocalVibes.Models
{
    // Tabla UserFavoriteProject que conecta Users con Project
    public class UserFavoriteProject
    {
        public int IdUsers {  get; set; } // FK de Users
        public int IdProject { get; set; } // FK de Project
    }
}
