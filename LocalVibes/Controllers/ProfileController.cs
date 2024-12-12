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
        public IActionResult Project(int id)
        {
            
            ProjectDAL projectDal = new ProjectDAL();

            var project = projectDal.GetById(id);

            if(project == null){
                return RedirectToAction("Index", "Landing");

            }
            // Verifica si la sesión contiene un indicador de usuario autenticado.
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // Redirige a la página de aterrizaje si no hay un usuario autenticado.
                return RedirectToAction("Index", "Landing");
            }

            // Obtención de ProjectId 
            //int.TryParse(HttpContext.Session.GetString("ProjectId"), out int projectId);

            //var project = projectDal.GetById(projectId);

            //project.Reviews ??= new List<Review>();
            //project.SocialMedias ??= new List<ProjectSocialMedia>();
            //project.MembersOfProjects ??= new List<MemberOfProject>();
            //project.EventsProject ??= new List<EventProject>();

            ProfileProjectViewModel vm = new ProfileProjectViewModel
            {
                Project = project
            };
            
            return View(vm);
        }
        public IActionResult Event()
        {
            ProfileEventViewModel vm = new ProfileEventViewModel();

            //Evento de prueba para tener las coords para el mapa
            vm.Event = new EventProject
            {
                IdEvent = 1,
                EventTitle = "Title",
                EventDescription = "Description",
                IsSoldOut = false,
                EventDate = DateTime.Now,
                IdProject = 1,
                IdLocation = 1,
            };

            return View(vm);
        }
        
        public IActionResult User()
        {

            // Verifica si la sesión contiene un indicador de usuario autenticado.
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // Redirige a la página de aterrizaje si no hay un usuario autenticado.
                return RedirectToAction("Login", "Authentication");
            }

            UserDAL userDal = new UserDAL();
            int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);
            var user = userDal.GetById(userId);
            var eventos = userDal.GetAssistEventsByUserId(userId);
            var projectFavorites = userDal.GetFavoriteProjectsByUserId(userId);


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
