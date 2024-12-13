using System.ComponentModel.DataAnnotations;
using LocalVibes.DTOs;

namespace LocalVibes.Models.ViewModels
{
    // Modelo de vista utilizado para iniciar la aplicacion.
    public class LandingViewModel
    {
        public List<EventProject> Eventos { get; set; }
    }
}
