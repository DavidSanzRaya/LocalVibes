using LocalVibes.Models;
using LocalVibes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LocalVibes.DALs;

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
        public IActionResult Project()
        {
            // Verifica si la sesión contiene un indicador de usuario autenticado.
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // Redirige a la página de aterrizaje si no hay un usuario autenticado.
                return RedirectToAction("Landing", "Home");
            }
            
            ProjectDAL projectDal = new ProjectDAL();

            // Obtención de ProjectId 
            int.TryParse(HttpContext.Session.GetString("ProjectId"), out int projectId);

            var project = projectDal.GetById(projectId);

            project.Reviews ??= new List<Review>();
            project.SocialMedias ??= new List<ProjectSocialMedia>();
            project.MembersOfProjects ??= new List<MemberOfProject>();
            project.EventsProject ??= new List<EventProject>();

            ProfileProjectViewModel vm = new ProfileProjectViewModel
            {
                Project = project
            };
            
            return View(vm);
        }
        public IActionResult Event()
        {
            ProfileEventViewModel vm = new ProfileEventViewModel();

            return View(vm);
        }

        public IActionResult User()
        {
            //ProfilUserViewModel vm = new ProfileUserViewModel();

            // Verifica si la sesión contiene un indicador de usuario autenticado.
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // Redirige a la página de aterrizaje si no hay un usuario autenticado.
                return RedirectToAction("Landing", "Home");
            }

            UserDAL userDal = new UserDAL();

            int.TryParse(HttpContext.Session.GetString("UserId"), out int userId);

            var user = userDal.GetById(userId);

            return View();
        }
    }
}
