namespace LocalVibes.Models
{
    // Tabla UserGenereMusic que conecta Users con Genere
    public class UserGenereMusic
    {
        public int idUser {  get; set; } // FK de Users
        public int idGenereMusic { get; set; } // FK de Genere
    }
}
