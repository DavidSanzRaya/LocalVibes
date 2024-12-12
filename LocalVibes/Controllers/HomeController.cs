using LocalVibes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LocalVibes.Models.ViewModels;
using LocalVibes.DALs;
using LocalVibes.DTOs;

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
                return RedirectToAction("Login", "Authentication");
            }

            // Si hay sesión activa, muestra la vista de Home.
            return View();
        }
        public IActionResult Explore()
        {
            // Verifica si la sesión contiene un indicador de usuario autenticado.
            //if (HttpContext.Session.GetString("UserId") == null)
            //{
            //    // Redirige a la página de aterrizaje si no hay un usuario autenticado.
            //    return RedirectToAction("Login", "Authentication");
            //}

            // Si hay sesión activa, muestra la vista de Home.
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // Redirige a la página de aterrizaje si no hay un usuario autenticado.
                return RedirectToAction("Login", "Authentication");
            }

            UserDAL userDal = new UserDAL();
            int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);
            var user = userDal.GetById(userId);
            
            HomeExploreViewModel vm = new HomeExploreViewModel
            {
                Events = new EventProjectDAL().GetAll()
                    .Select(e => new EventDTO
                    {
                        IdEvent = e.IdEvent,
                        EventTitle = e.EventTitle,
                        EventDate = e.EventDate,
                        EventImage = e.EventImage,
                        Location = e.Location,
                    })
                    .ToList(),
                Generes = new GenereMusicDAL().GetAll(),
                Projects = new ProjectDAL().GetAll()
                    .Select(e => new ProjectDTO
                    {
                        IdProject = e.IdProject,
                        ProjectName = e.ProjectName,
                        FormationDate = e.FormationDate,
                        ProjectImage = e.ProjectImage,
                        GeneresMusic = e.GeneresMusic,
                    })
                    .ToList(),
                User = user
            };

            return View(vm);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
