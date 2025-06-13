using Microsoft.AspNetCore.Mvc;

namespace AeropuertoConlara.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Panel()
        {
            return View();
        }
    }
}
