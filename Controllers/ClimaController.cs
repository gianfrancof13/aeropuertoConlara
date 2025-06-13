using Microsoft.AspNetCore.Mvc;

namespace AeropuertoConlara.Controllers
{
    public class ClimaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
