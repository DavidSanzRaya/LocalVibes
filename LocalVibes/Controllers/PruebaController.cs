using LocalVibes.DALs;
using LocalVibes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LocalVibes.Controllers
{
    public class PruebaController : Controller
    {
        public IActionResult Prueba()
        {
            UserDAL dalUser = new UserDAL();
            PruebaViewModel viewModel = new PruebaViewModel();
            viewModel.user = dalUser.GetById(1);
            viewModel.Usuarios = dalUser.GetAll();
            return View(viewModel);
        }
    }
}
