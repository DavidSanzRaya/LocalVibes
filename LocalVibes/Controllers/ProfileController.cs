using LocalVibes.Models;
using LocalVibes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            ProfileProjectViewModel vm = new ProfileProjectViewModel();

            return View(vm);
        }
    }
}
