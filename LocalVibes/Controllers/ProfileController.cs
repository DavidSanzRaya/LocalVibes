using LocalVibes.Models;
using LocalVibes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LocalVibes.DALs;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Linq;

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

			var userId = int.Parse(HttpContext.Session.GetString("UserId"));
			var user = new UserDAL().GetById(userId);

			// Crea el ViewModel para la vista
			ProfileProjectViewModel vm = new ProfileProjectViewModel
            {
                Project = project,
				IsFavorite = user.UserFavoriteProjects.Any(p => p.IdProject == id)

			};

            return View(vm);
        }

		[HttpPost]
		public IActionResult ToggleFavorite(int projectId)
		{
            ProjectDAL projectDAL = new ProjectDAL();
            Project project = projectDAL.GetById(projectId);
			// Obtén el ID del usuario desde la sesión
			var userId = HttpContext.Session.GetString("UserId");

			// Recupera el usuario y su lista de favoritos
			var user = new UserDAL().GetById(int.Parse(userId));


			if (user.UserFavoriteProjects.Any(p => p.IdProject == projectId))
			{
				user.RemoveFavoriteProject(projectId);
			}
			else
			{
				user.AddFavoriteProject(project);
			}

			// Actualiza al usuario en la base de datos
			new UserDAL().Update(user);

			return RedirectToAction("Project", new { id = projectId });
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
