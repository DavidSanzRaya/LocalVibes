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

        // Accion principal que muestra el Landing de la pagina web
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

        // Accion que redirije a Privacity
        public IActionResult Privacy()
        {
            return View();
        }

        // Acci�n que redirige a Home o muestra Landing si no hay sesi�n activa.
        public IActionResult Home()
        {
            // Verifica si la sesi�n contiene un indicador de usuario autenticado.
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // Redirige a la p�gina de aterrizaje si no hay un usuario autenticado.
                return RedirectToAction("Landing", "Home");
            }

            // Si hay sesi�n activa, muestra la vista de Home.
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
