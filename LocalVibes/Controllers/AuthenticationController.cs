using LocalVibes.DALs;
using LocalVibes.Models;
using LocalVibes.Models.ViewModels;
using LocalVibes.Tools;
using Microsoft.AspNetCore.Mvc;

namespace LocalVibes.Controllers
{
    public class AuthenticationController : Controller
    {

        #region Login
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

                UserDAL dal = new UserDAL();
                Users usuario = dal.GetUsuarioByUsername(model.Username);

                //Validar usuario
                if (usuario != null)
                {

                    HttpContext.Session.SetString("Username", model.Username);

                    // Autenticacion exitosa
                    return RedirectToAction("Home", "Home");
                }


                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            }
            return View(model);
        }
        #endregion

        #region SignUp User

        public IActionResult SignUpUser()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpUser(SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Aquí inyectas la dependencia UserDAL en lugar de instanciarla directamente.
                UserDAL dal = new UserDAL();

                // Verificar si el usuario ya existe
                Users usuarioExistente = dal.GetUsuarioByUsername(model.Username);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("", "El nombre de usuario ya está en uso.");
                    return View(model);
                }

                // Crear el usuario
                Users nuevoUsuario = new Users
                {
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserEmail = model.UserEmail,
                    Phone = model.Phone,
                    Birthdate = model.Birthdate,
                    DateRegister = DateTime.Now,
                    IdGenere = 1, // Asumiendo un valor por defecto
                    IdTier = 1, // Asumiendo un valor por defecto
                    UserPoints = 0 // Valor por defecto
                };

                // Generar el hash y salt para la contraseña
                PasswordHelper.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                nuevoUsuario.PasswordHash = passwordHash;
                nuevoUsuario.PasswordSalt = passwordSalt;

                // Guardar el nuevo usuario
                dal.CreateUsuario(nuevoUsuario);

                // Verificar que el usuario ha sido creado
                Users validarCreacion = dal.GetUsuarioByUsername(model.Username);
                if (validarCreacion != null)
                {
                    // Guardar el nombre de usuario en la sesión
                    HttpContext.Session.SetString("Username", nuevoUsuario.UserName);
                    return RedirectToAction("Home", "Home");
                }

                ModelState.AddModelError("", "No se ha podido crear el usuario.");
            }

            // Si hay errores de validación, regresa a la vista
            return View(model);
        }


        #endregion

        #region SignUp Project

        public IActionResult SignUpProject()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpProject(SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDAL dal = new UserDAL();
                Users usuario = new Users();

                usuario.UserName = model.Username;

                Users usuarioExistente = dal.GetUsuarioByUsername(usuario.UserName);

                // Validar Usuario
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("", "Usuario existente");
                    return View(model);
                }

                dal.CreateUsuario(usuario);

                Users validarCreacion = dal.GetUsuarioByUsername(model.Username);

                if (validarCreacion != null)
                {
                    HttpContext.Session.SetString("Username", usuario.UserName);
                    return RedirectToAction("Home", "Home");
                }

                ModelState.AddModelError("", "No se ha podido crear usuario");
            }
            return View(model);
        }

        #endregion
    }
}

