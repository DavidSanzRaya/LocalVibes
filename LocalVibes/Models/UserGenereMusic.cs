namespace LocalVibes.Models
{
    // Tabla UserGenereMusic que conecta Users con Genere
    public class UserGenereMusic
    {
        public int IdUsers {  get; set; } // FK de Users
        public int IdGenereMusic { get; set; } // FK de Genere
    }
}
