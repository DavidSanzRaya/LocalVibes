using LocalVibes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LocalVibes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DatabaseService _databaseService;

        public HomeController(ILogger<HomeController> logger, DatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;

        }

        // Accion principal
        public IActionResult Landing()
        {
            // Obtener la cadena de conexi�n
            var connectionString = _databaseService.GetConnectionString();

            // Pasar la cadena de conexi�n a la vista o usarla en l�gica
            ViewBag.ConnectionString = connectionString;

            return View();
        }

        // Accion que redirije a About
        public IActionResult About()
        {
            // Obtener la cadena de conexi�n
            var connectionString = _databaseService.GetConnectionString();

            // Pasar la cadena de conexi�n a la vista o usarla en l�gica
            ViewBag.ConnectionString = connectionString;

            return View();
        }

        // Accion que redirije a Prueba
        public IActionResult Prueba()
        {
            // Obtener la cadena de conexi�n
            var connectionString = _databaseService.GetConnectionString();

            // Pasar la cadena de conexi�n a la vista o usarla en l�gica
            ViewBag.ConnectionString = connectionString;

            return View();
        }

        // Accion que redirije a Privacity
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
