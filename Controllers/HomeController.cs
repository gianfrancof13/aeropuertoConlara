using System.Diagnostics;
using AeropuertoConlara.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AeropuertoConlara.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var vuelos = new List<Vuelo>
            {
                new Vuelo { Id = 1, Aerolinea = "Aerolineas Argentinas", NumeroVuelo = "AR1234", Destino = "Buenos Aires", FechaHora = System.DateTime.Now.AddHours(1), Estado = "En horario" },
                new Vuelo { Id = 2, Aerolinea = "Flybondi", NumeroVuelo = "FO5678", Destino = "Córdoba", FechaHora = System.DateTime.Now.AddHours(2), Estado = "Demorado" },
                new Vuelo { Id = 3, Aerolinea = "JetSMART", NumeroVuelo = "JS9101", Destino = "Mendoza", FechaHora = System.DateTime.Now.AddHours(3), Estado = "Aterrizado" }
            };

            var noticiasService = new NoticiasSanLuisService();
            var noticias = await noticiasService.ObtenerUltimasNoticiasAsync();
            ViewBag.NoticiasSanLuis = noticias;
            return View(vuelos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
