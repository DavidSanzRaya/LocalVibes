using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    // Modelo de vista utilizado para iniciar la aplicacion.
    public class LandingViewModel
    {
        public List<EventProject> Eventos { get; set; }
    }
}
