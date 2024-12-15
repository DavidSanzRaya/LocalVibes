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

        public IActionResult Home()
        {
            // Verifica si la sesión contiene un indicador de usuario autenticado.
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // Redirige a la página de aterrizaje si no hay un usuario autenticado.
                return RedirectToAction("index", "Landing");
            }

            // Si hay sesión activa, muestra la vista de Home.
            return View();
        }

        public IActionResult Events()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // Redirige a la página de aterrizaje si no hay un usuario autenticado.
                return RedirectToAction("Login", "Authentication");
            }

            EventsMapViewModel model = new EventsMapViewModel()
            {
                Events = new EventProjectDAL().GetAll()
                    .Select(e => new EventDTO
                    {
                        IdEvent = e.IdEvent,
                        EventTitle = e.EventTitle,
                        EventDescription = e.EventDescription,
                        EventDate = e.EventDate,
                        EventImage = e.EventImage,
                        Location = e.Location,
                        GeneresMusic = e.Project.GeneresMusic
                    })
                    .ToList(),
                Generes = new GenereMusicDAL().GetAll()
            };

            // Si hay sesión activa, muestra la vista de Home.
            return View(model);
        }

        public IActionResult ExploreBands()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            ExploreBandsViewModel model = new ExploreBandsViewModel();
            model.Projects = new ProjectDAL().GetAll();

            string userIdString = HttpContext.Session.GetString("UserId");

            if (int.TryParse(userIdString, out int userId))
            {
                model.FavoriteProjects = new UserDAL().GetFavoriteProjectsByUserId(userId);  
            }
            return View(model);
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
