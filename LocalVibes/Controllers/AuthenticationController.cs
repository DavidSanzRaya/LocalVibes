using Azure.Identity;
using LocalVibes.DALs;
using LocalVibes.Models;
using LocalVibes.Models.ViewModels;
using LocalVibes.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

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

        [HttpPost]
        [ValidateAntiForgeryToken] // Helps prevent CSRF attacks
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDAL userDal = new UserDAL();
                var usuario = userDal.GetByName(model.Username);

                if (usuario != null && PasswordHelper.VerifityPasswordHash(model.Password, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    HttpContext.Session.SetString("UserId", usuario.IdUsers.ToString());
                    HttpContext.Session.SetString("Username", usuario.UserName);
                    HttpContext.Session.SetString("Role", "User");

                    if (usuario.Project != null)
                    {
                        var project = usuario.Project;
                        HttpContext.Session.SetString("ProjectId", project.IdProject.ToString());
                        HttpContext.Session.SetString("ProjectName", project.ProjectName);
                    }

                    TempData["SuccessMessage"] = "¡Bienvenido, " + usuario.FirstName + "!";
                    return RedirectToAction("User", "Profile");
                }

            }
            ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            return View(model);
        }

        #endregion

        #region SignUp User

        public IActionResult SignUpUser()
        {
            var model = new SignUpUserViewModel();

            ReloadSignUpUserViewModelLists(model);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUpUser(SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserDAL dal = new UserDAL();
                    var usuarioExistente = dal.GetByName(model.Username);

                    if (usuarioExistente != null)
                    {
                        ModelState.AddModelError("", "El nombre de usuario ya está en uso.");
                        ReloadSignUpUserViewModelLists(model);
                        return View(model);
                    }

                    var nuevoUsuario = new Users
                    {
                        UserName = model.Username,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserEmail = model.UserEmail,
                        Phone = model.Phone,
                        Birthdate = model.Birthdate,
                        DateRegister = DateTime.Now,
                        IdGenere = model.IdGenere,
                        DocumentNumber = model.DocumentNumber,
                        IdDocumentType = model.IdDocumentType,
                        IdTier = 1,
                        UserPoints = 0
                    };

                    PasswordHelper.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    nuevoUsuario.PasswordHash = passwordHash;
                    nuevoUsuario.PasswordSalt = passwordSalt;

                    if (model.ProfileImage != null && model.ProfileImage.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ProfileImage.CopyToAsync(memoryStream);
                            nuevoUsuario.ProfileImage = memoryStream.ToArray();
                        }
                    }

                    dal.Add(nuevoUsuario);
                    var usuarioCreado = dal.GetByName(model.Username);

                    if (usuarioCreado != null)
                    {
                        // Registrar la sesión automáticamente después del registro
                        HttpContext.Session.SetString("UserId", usuarioCreado.IdUsers.ToString());
                        HttpContext.Session.SetString("Username", usuarioCreado.UserName);
                        HttpContext.Session.SetString("Role", "User");

                        TempData["SuccessMessage"] = "Usuario registrado con éxito. ¡Bienvenido, " + usuarioCreado.FirstName + "!";
                        return RedirectToAction("Home", "Home");
                    }

                    ModelState.AddModelError("", "No se ha podido crear el usuario. Inténtelo nuevamente.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error inesperado. Por favor, inténtelo nuevamente.");
                }
            }

            ReloadSignUpUserViewModelLists(model);
            return View(model);
        }

        private void ReloadSignUpUserViewModelLists(SignUpUserViewModel model)
        {
            // Recargar géneros
            GenereDAL dalGenere = new GenereDAL();
            var generes = dalGenere.GetAll();
            model.Generes = generes.Select(g => new SelectListItem
            {
                Value = g.IdGenere.ToString(),
                Text = g.GenereName
            }).ToList();

            // Recargar tipos de documentos
            DocumentTypeDAL dalDocument = new DocumentTypeDAL();
            var documentTypes = dalDocument.GetAll();
            model.Documents = documentTypes.Select(d => new SelectListItem
            {
                Value = d.IdDocumentType.ToString(),
                Text = d.NameDocumentType
            }).ToList();
        }

        #endregion

        #region SignUp Project

        // Accion para mostrar la vista Sign Up de Project
        public IActionResult SignUpProject()
        {
            var model = new SignUpProjectViewModel();

            ReloadSignUpProjectViewModelLists(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUpProject(SignUpProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProjectDAL projectDal = new ProjectDAL();
                    UserDAL userDal = new UserDAL();

                    // Verificar si el usuario administrador existe
                    var usuarioExistente = userDal.GetByName(model.UsernameAdmin);
                    if (usuarioExistente == null)
                    {
                        ModelState.AddModelError("", "El usuario administrador no existe.");
                        ReloadSignUpProjectViewModelLists(model);
                        return View(model);
                    }

                    // Validar si ya existe un proyecto con el mismo nombre
                    var existingProject = projectDal.GetByName(model.ProjectName);
                    if (existingProject != null)
                    {
                        ModelState.AddModelError("", "Ya existe un proyecto con este nombre.");
                        ReloadSignUpProjectViewModelLists(model);
                        return View(model);
                    }

                    // Crear el nuevo proyecto
                    var newProject = new Project
                    {
                        ProjectName = model.ProjectName,
                        Biography = model.Biography,
                        FormationDate = model.FormationDate,
                        IdUsersAdmin = usuarioExistente.IdUsers // Relacionar al administrador
                    };

                    // Procesar imagen del proyecto si está presente
                    if (model.ProjectImage != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ProjectImage.CopyToAsync(memoryStream);
                            newProject.ProjectImage = memoryStream.ToArray();
                        }
                    }

                    // Guardar el nuevo proyecto en la base de datos
                    projectDal.Add(newProject);

                    // Confirmar que el proyecto se creó correctamente
                    var createdProject = projectDal.GetByName(newProject.ProjectName);
                    if (createdProject != null)
                    {
                        // Registrar la sesión del proyecto
                        HttpContext.Session.SetString("ProjectId", createdProject.IdProject.ToString());
                        HttpContext.Session.SetString("ProjectName", createdProject.ProjectName);
                        HttpContext.Session.SetString("Username", usuarioExistente.UserName);
                        HttpContext.Session.SetString("UserId", usuarioExistente.IdUsers.ToString());

                        HttpContext.Session.SetString("Role", "Band");

                        TempData["SuccessMessage"] = "Proyecto registrado con éxito. ¡Bienvenido, " + createdProject.ProjectName + "!";
                        return RedirectToAction("Home", "Home");
                    }

                    // Error en la creación del proyecto
                    ModelState.AddModelError("", "No se ha podido crear el proyecto. Inténtelo nuevamente.");
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    ModelState.AddModelError("", "Ocurrió un error inesperado. Por favor, inténtelo nuevamente.");
                    Console.WriteLine(ex); // Opcional: Log del error
                }
            }

            // Recargar listas dinámicas si hay errores
            ReloadSignUpProjectViewModelLists(model);
            return View(model);
        }

        private void ReloadSignUpProjectViewModelLists(SignUpProjectViewModel model)
        {

            // Llamar al DAL para obtener la lista de géneros musicales
            GenereMusicDAL dal = new GenereMusicDAL();
            var genereMusics = dal.GetAll();

            // Verifica si la lista de géneros está vacía o es nula
            if (genereMusics == null || !genereMusics.Any())
            {
                ModelState.AddModelError("", "No se pudieron cargar los géneros musicales.");
            }

            // Mapear los géneros musicales a SelectListItem
            model.SelectedGeneresMusic = genereMusics.Select(g => new SelectListItem
            {
                Value = g.IdGenereMusic.ToString(),
                Text = g.GenereMusicName
            }).ToList();
        }


        #endregion

        #region SignUp Shift

        [HttpGet]
        public IActionResult SignUpShift()
        {
            var userModel = LoadUserRegistrationData();
            var bandModel = LoadBandRegistrationData();

            var model = new SignUpViewModel
            {
                User = userModel,
                Band = bandModel
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUpUserShift(SignUpViewModel.UserRegistrationData model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserDAL dal = new UserDAL();
                    var usuarioExistente = dal.GetByName(model.Username);

                    if (usuarioExistente != null)
                    {
                        ModelState.AddModelError("", "El nombre de usuario ya está en uso.");
                        return View(LoadUserRegistrationData());
                    }

                    var nuevoUsuario = new Users
                    {
                        UserName = model.Username,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserEmail = model.UserEmail,
                        Phone = model.Phone,
                        Birthdate = model.Birthdate,
                        DateRegister = DateTime.Now,
                        IdGenere = model.IdGenere,
                        DocumentNumber = model.DocumentNumber,
                        IdDocumentType = model.IdDocumentType,
                        IdTier = 1,
                        UserPoints = 0
                    };

                    PasswordHelper.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    nuevoUsuario.PasswordHash = passwordHash;
                    nuevoUsuario.PasswordSalt = passwordSalt;

                    if (model.ProfileImage != null && model.ProfileImage.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ProfileImage.CopyToAsync(memoryStream);
                            nuevoUsuario.ProfileImage = memoryStream.ToArray();
                        }
                    }

                    dal.Add(nuevoUsuario);
                    var usuarioCreado = dal.GetByName(model.Username);

                    if (usuarioCreado != null)
                    {
                        // Registrar la sesión automáticamente después del registro
                        HttpContext.Session.SetString("UserId", usuarioCreado.IdUsers.ToString());
                        HttpContext.Session.SetString("Username", usuarioCreado.UserName);
                        HttpContext.Session.SetString("Role", "User");

                        TempData["SuccessMessage"] = "Usuario registrado con éxito. ¡Bienvenido, " + usuarioCreado.FirstName + "!";
                        return RedirectToAction("Home", "Home");
                    }

                    ModelState.AddModelError("", "No se ha podido crear el usuario. Inténtelo nuevamente.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error inesperado. Por favor, inténtelo nuevamente.");
                }
            }
            else
            {
                foreach (var error in ModelState)
                {
                    var key = error.Key; // Nombre de la propiedad
                    var errors = error.Value.Errors; // Errores asociados
                    foreach (var err in errors)
                    {
                        Console.WriteLine($"Property: {key}, Error: {err.ErrorMessage}");
                    }
                }
            }

            return View(model);
        }

        private SignUpViewModel.UserRegistrationData LoadUserRegistrationData()
        {
            GenereDAL dalGenere = new GenereDAL();
            var generes = dalGenere.GetAll();

            return new SignUpViewModel.UserRegistrationData
            {
                Generes = generes.Select(g => new SelectListItem
                {
                    Value = g.IdGenere.ToString(),
                    Text = g.GenereName
                })
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUpBandShift(SignUpViewModel.BandRegistrationData model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProjectDAL projectDal = new ProjectDAL();
                    UserDAL userDal = new UserDAL();

                    // Verificar si el usuario administrador existe
                    var usuarioExistente = userDal.GetByName(model.UsernameAdmin);
                    if (usuarioExistente == null)
                    {
                        ModelState.AddModelError("", "El usuario administrador no existe.");
                        return View(LoadBandRegistrationData());
                    }

                    // Validar si ya existe un proyecto con el mismo nombre
                    var existingProject = projectDal.GetByName(model.ProjectName);
                    if (existingProject != null)
                    {
                        ModelState.AddModelError("", "Ya existe un proyecto con este nombre.");
                        return View(LoadBandRegistrationData());
                    }

                    // Crear el nuevo proyecto
                    var newProject = new Project
                    {
                        ProjectName = model.ProjectName,
                        Biography = model.Biography,
                        FormationDate = model.FormationDate,
                        IdUsersAdmin = usuarioExistente.IdUsers,
                    };

                    // Procesar imagen del proyecto si está presente
                    if (model.ProjectImage != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ProjectImage.CopyToAsync(memoryStream);
                            newProject.ProjectImage = memoryStream.ToArray();
                        }
                    }

                    // Guardar el nuevo proyecto en la base de datos
                    projectDal.Add(newProject);

                    // Confirmar que el proyecto se creó correctamente
                    var createdProject = projectDal.GetByName(newProject.ProjectName);
                    if (createdProject != null)
                    {
                        AddProjectMusicGenres(createdProject.IdProject, model.SelectedGenres);

                        // Registrar la sesión del proyecto
                        HttpContext.Session.SetString("ProjectId", createdProject.IdProject.ToString());
                        HttpContext.Session.SetString("ProjectName", createdProject.ProjectName);
                        HttpContext.Session.SetString("Username", usuarioExistente.UserName);
                        HttpContext.Session.SetString("UserId", usuarioExistente.IdUsers.ToString());

                        HttpContext.Session.SetString("Role", "Band");

                        TempData["SuccessMessage"] = "Proyecto registrado con éxito. ¡Bienvenido, " + createdProject.ProjectName + "!";
                        return RedirectToAction("Home", "Home");
                    }

                    // Error en la creación del proyecto
                    ModelState.AddModelError("", "No se ha podido crear el proyecto. Inténtelo nuevamente.");
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    ModelState.AddModelError("", "Ocurrió un error inesperado. Por favor, inténtelo nuevamente.");
                    Console.WriteLine(ex); // Opcional: Log del error
                }
            }

            return View(LoadBandRegistrationData());
        }

        private SignUpViewModel.BandRegistrationData LoadBandRegistrationData()
        {
            GenereMusicDAL genereMusicDAL = new GenereMusicDAL();
            var musicGenres = genereMusicDAL.GetAll();

            return new SignUpViewModel.BandRegistrationData
            {
                SelectedGeneresMusic = musicGenres.Select(g => new SelectListItem
                {
                    Value = g.IdGenereMusic.ToString(),
                    Text = g.GenereMusicName
                })
            };
        }

        private void AddProjectMusicGenres(int projectId, List<int> musicGenres)
        {
            ProjectGenereMusicDAL projectGenereMusicDAL = new ProjectGenereMusicDAL();

            // Usa Add para insertar cada género seleccionado
            foreach (var genreId in musicGenres)
            {
                var projectGenereMusic = new ProjectGenereMusic
                {
                    IdProject = projectId,
                    IdGenereMusic = genreId
                };

                projectGenereMusicDAL.Add(projectGenereMusic);
            }
        }
        #endregion

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Limpiar sesión
            return RedirectToAction("Landing", "index");
        }
    }
}

