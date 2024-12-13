using LocalVibes.Models;
using LocalVibes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LocalVibes.DALs;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

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
        //[HttpGet]
        ////[Route("Profile/Project/BySession")]
        //public IActionResult Project()
        //{
        //    // Verificar si el usuario está autenticado.
        //    var userId = HttpContext.Session.GetString("UserId");
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        // Redirigir al índice si no está autenticado.
        //        return RedirectToAction("Login", "Authentication");
        //    }

        //    // Intentar obtener el ID del proyecto asociado desde la sesión.
        //    if (!int.TryParse(HttpContext.Session.GetString("ProjectId"), out int projectId))
        //    {
        //        // Si no hay un proyecto asociado, redirigir a una página de error o inicio.
        //        TempData["ErrorMessage"] = "No tienes ningún proyecto asociado.";
        //        return RedirectToAction("User", "Profile");
        //    }

        //    // Obtener los datos del proyecto desde la base de datos.
        //    ProjectDAL projectDal = new ProjectDAL();
        //    var project = projectDal.GetById(projectId);

        //    if (project == null)
        //    {
        //        // Si el proyecto no existe en la base de datos, manejar el error.
        //        TempData["ErrorMessage"] = "El proyecto asociado no fue encontrado.";
        //        return RedirectToAction("User", "Profile");
        //    }

        //    // Preparar el modelo para la vista.
        //    ProfileProjectViewModel vm = new ProfileProjectViewModel
        //    {
        //        Project = project
        //    };

        //    return View(vm);
        //}

        [HttpGet]
        //[Route("Profile/Project/{id:int}")]
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

        [HttpPost]
        public IActionResult AddReview(int id, string reviewText, int rating)
        {
            if (string.IsNullOrEmpty(reviewText) || rating < 1 || rating > 5)
            {
                // Aquí puedes devolver un mensaje de error si es necesario.
                return RedirectToAction("Project", new { id = id });
            }

            // Obtener el ID del usuario actualmente autenticado
            UserDAL userDal = new UserDAL();
            int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);
            var user = userDal.GetById(userId);

            // Crear la nueva reseña
            var review = new Review
            {
                ReviewText = reviewText,
                Rating = rating,
                ReviewDate = DateTime.Now,
                IdUser = userId,
                User = user,
                IdProject = id
            };

            // Guardar la reseña en la base de datos
            var reviewDal = new ReviewDAL();
            reviewDal.Insert(review);

            // Redirigir a la página del proyecto para mostrar la nueva reseña
            return RedirectToAction("Project", new { id = id });
        }

        [HttpPost]
        public IActionResult CreateEvent(string eventTitle, string eventDescription, DateTime eventDate, int? eventCapacity, int locationId, bool isSoldOut = false)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Authentication");
            }

            // Obtener el proyecto asociado al usuario
            int projectId = int.Parse(HttpContext.Session.GetString("ProjectId"));
            var project = new ProjectDAL().GetById(projectId);

            // Crear el nuevo evento
            var newEvent = new EventProject
            {
                EventTitle = eventTitle,
                EventDescription = eventDescription,
                EventDate = eventDate,
                Capacity = eventCapacity,
                IsSoldOut = isSoldOut,
                IdProject = projectId,
                IdLocation = locationId 
            };

            // Guardar el evento en la base de datos
            var eventDal = new EventProjectDAL();
            eventDal.InsertEvent(newEvent);

            // Redirigir al perfil del proyecto con el evento creado
            return RedirectToAction("Project", new { id = projectId });
        }

    }
}
