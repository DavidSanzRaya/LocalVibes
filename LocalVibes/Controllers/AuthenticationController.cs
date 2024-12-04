using Azure.Identity;
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

        // Acccion para mostrar la vista de Login
        public IActionResult Login()
        {
            return View();
        }

        // Accion para realizar el login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                UserDAL dal = new UserDAL();
                Users usuario = dal.GetUserByUsername(model.Username);

                //Validar usuario
                if (usuario != null)
                {

                    HttpContext.Session.SetString("Username", model.Username);

                    // Autenticacion exitosa
                    return RedirectToAction("Home", "Home");
                }

                // Autenticacion fracasada
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            }
            return View(model);
        }
        #endregion

        #region SignUp User

        // Accion para mostrar la vista Sign Up de User
        public IActionResult SignUpUser()
        {
            return View();
        }

        // Accion para realizar el registro de User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpUser(SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDAL dal = new UserDAL();

                // Verificar si el usuario ya existe por el nombre de usuario
                Users usuarioExistente = dal.GetUserByUsername(model.Username);

                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("", "El nombre de usuario ya está en uso.");
                    return View(model);
                }

                Users nuevoUsuario = new Users
                {
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserEmail = model.UserEmail,
                    Phone = model.Phone,
                    Birthdate = model.Birthdate,
                    DateRegister = DateTime.Now,
                    IdGenere = model.IdGenere, // Asignación del ID de género
                    DocumentNumber = model.DocumentNumber,
                    IdDocumentType = model.IdDocumentType, // Asignación del ID de tipo de documento
                    IdTier = 1, // Asumiendo un valor por defecto
                    UserPoints = 0 // Valor por defecto
                };

                // Generar el hash y salt para la contraseña
                PasswordHelper.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                nuevoUsuario.PasswordHash = passwordHash;
                nuevoUsuario.PasswordSalt = passwordSalt;

                // Guardar el nuevo usuario
                dal.Add(nuevoUsuario);

                // Verificar que el usuario ha sido creado
                Users validarCreacion = dal.GetUserByUsername(model.Username);
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

        // Accion para mostrar la vista Sign Up de Project
        public IActionResult SignUpProject()
        {
            return View();
        }

        // Accion para mrealizar el registro de Project
        // TODO: Falta mejorar e implementar del sign up para projects
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpProject(SignUpProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProjectDAL projectDal = new ProjectDAL();
                Project newProject = new Project { 
                    ProjectName = model.ProjectName,
                    Biography = model.Biography,
                    FormationDate = model.FormationDate,
                    IdUserAdmin = model.IdUserAdmin
                };

                // Asignar valores desde el ViewModel al modelo
                

                // Procesar imagen si está presente
                if (model.ProjectImage != null && model.ProjectImage.Length > 0)
                {
                    newProject.ProjectImage = model.ProjectImage;
                }

                // Validar si ya existe un proyecto con el mismo nombre
                var existingProject = projectDal.GetProjectByName(newProject.ProjectName);
                if (existingProject != null)
                {
                    ModelState.AddModelError("", "Ya existe un proyecto con este nombre.");
                    return View(model);
                }

                // Guardar el proyecto en la base de datos
                projectDal.Add(newProject);

                // Verificar si se ha creado correctamente
                var createdProject = projectDal.GetProjectByName(newProject.ProjectName);
                if (createdProject != null)
                {
                    TempData["SuccessMessage"] = "Proyecto creado con éxito.";
                    return RedirectToAction("ProjectList", "Project");
                }

                // Error en la creación
                ModelState.AddModelError("", "No se ha podido crear el proyecto. Inténtelo nuevamente.");
            }

            // Si hay errores, devolver el modelo a la vista
            return View(model);
        }


        #endregion
    }
}

