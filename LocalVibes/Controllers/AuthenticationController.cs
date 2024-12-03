using LocalVibes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LocalVibes.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Landing", "Home");
            }

            return View(model);
        }

        public IActionResult UserSignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserSignUp(UserSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Landing", "Home");
            }

            return View(model);
        }
    }
}
