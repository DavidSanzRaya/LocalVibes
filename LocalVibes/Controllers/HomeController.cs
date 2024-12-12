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
            // Obtener la cadena de conexión
            var connectionString = _databaseService.GetConnectionString();

            // Pasar la cadena de conexión a la vista o usarla en lógica
            ViewBag.ConnectionString = connectionString;

            return View();
        }

        // Accion que redirije a About
        public IActionResult About()
        {
            // Obtener la cadena de conexión
            var connectionString = _databaseService.GetConnectionString();

            // Pasar la cadena de conexión a la vista o usarla en lógica
            ViewBag.ConnectionString = connectionString;

            return View();
        }       

        // Accion que redirije a Privacity
        public IActionResult Privacy()
        {
            return View();
        }

        // Acción que redirige a Home o muestra Landing si no hay sesión activa.
        public IActionResult Home()
        {
            // Verifica si la sesión contiene un indicador de usuario autenticado.
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // Redirige a la página de aterrizaje si no hay un usuario autenticado.
                return RedirectToAction("Landing", "Home");
            }

            // Si hay sesión activa, muestra la vista de Home.
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
