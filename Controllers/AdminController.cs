using Microsoft.AspNetCore.Mvc;

namespace AeropuertoConlara.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario, string password)
        {
            if (usuario == "admin" && password == "admin")
            {
                // TODO: Implementar autenticación real
                return RedirectToAction("Panel");
            }
            ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            return View();
        }

        public IActionResult Panel()
        {
            return View();
        }

        public IActionResult CreateUser()
        {
            return View();
        }
    }
}
