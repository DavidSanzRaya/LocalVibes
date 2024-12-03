using LocalVibes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LocalVibes.Controllers
{
    public class LandingController : Controller
    {
        private readonly ILogger<LandingController> _logger;

        private readonly DatabaseService _databaseService;

        public LandingController(ILogger<LandingController> logger, DatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;

        }

        // Accion principal
        public IActionResult index()
        {
            // Obtener la cadena de conexión
            var connectionString = _databaseService.GetConnectionString();

            // Pasar la cadena de conexión a la vista o usarla en lógica
            ViewBag.ConnectionString = connectionString;

            return View();
        }
    }
}
