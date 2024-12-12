using LocalVibes.Models;
using LocalVibes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LocalVibes.DALs;
using Microsoft.Extensions.Logging;

namespace LocalVibes.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;

        private readonly DatabaseService _databaseService;

        public ProfileController(ILogger<ProfileController> logger, DatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;

        }

        // Accion principal
        [HttpGet]
        [Route("Profile/Project/BySession")]
        public IActionResult Project()
        {
            // Verificar si el usuario está autenticado.
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                // Redirigir al índice si no está autenticado.
                return RedirectToAction("Login", "Authentication");
            }

            // Intentar obtener el ID del proyecto asociado desde la sesión.
            if (!int.TryParse(HttpContext.Session.GetString("ProjectId"), out int projectId))
            {
                // Si no hay un proyecto asociado, redirigir a una página de error o inicio.
                TempData["ErrorMessage"] = "No tienes ningún proyecto asociado.";
                return RedirectToAction("User", "Profile");
            }

            // Obtener los datos del proyecto desde la base de datos.
            ProjectDAL projectDal = new ProjectDAL();
            var project = projectDal.GetById(projectId);

            if (project == null)
            {
                // Si el proyecto no existe en la base de datos, manejar el error.
                TempData["ErrorMessage"] = "El proyecto asociado no fue encontrado.";
                return RedirectToAction("User", "Profile");
            }

            // Preparar el modelo para la vista.
            ProfileProjectViewModel vm = new ProfileProjectViewModel
            {
                Project = project
            };

            return View(vm);
        }

        [HttpGet]
        [Route("Profile/Project/{id:int}")]
        public IActionResult Project(int id)
        {
            // Obtén el proyecto por su ID
            ProjectDAL projectDal = new ProjectDAL();
            var project = projectDal.GetById(id);

            if (project == null)
            {
                TempData["ErrorMessage"] = "El proyecto no existe o no está disponible.";
                return RedirectToAction("User");
            }

            // Verifica si el usuario está autenticado
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            // Crea el ViewModel para la vista
            ProfileProjectViewModel vm = new ProfileProjectViewModel
            {
                Project = project
            };

            return View(vm);
        }


        public IActionResult Event(int id)
        {
            // Obtén el evento por su ID
            EventProjectDAL eventDal = new EventProjectDAL();
            var _event = eventDal.GetById(id);

            if (_event == null)
            {
                TempData["ErrorMessage"] = "El evento no existe o no está disponible.";
                return RedirectToAction("User");
            }

            // Crea el ViewModel para la vista
            ProfileEventViewModel vm = new ProfileEventViewModel
            {
                Event = _event
            };

            return View(vm);
        }


        public IActionResult User()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            UserDAL userDal = new UserDAL();
            int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);
            var user = userDal.GetById(userId);
            var eventos = userDal.GetAssistEventsByUserId(userId);
            var projectFavorites = userDal.GetFavoriteProjectsByUserId(userId);

            // Pasar datos necesarios para el diseño
            ViewData["Users"] = userDal.GetAll();

            var vm = new ProfileUserViewModel
            {
                User = user,
                Eventos = eventos,
                ProjectsFavotire = projectFavorites
            };

            return View(vm);
        }
    }
}
