using Microsoft.AspNetCore.Mvc;

namespace AeropuertoConlara.Controllers
{
    public class FormulariosController : Controller
    {
        public IActionResult Contacto()
        {
            return View();
        }

        public IActionResult CursosEDA()
        {
            return View();
        }
    }
}