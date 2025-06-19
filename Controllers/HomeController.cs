using System.Diagnostics;
using AeropuertoConlara.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AeropuertoConlara.Data;
using Microsoft.AspNetCore.Diagnostics;
using AeropuertoConlara.Services;

namespace AeropuertoConlara.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Tomar los 6 vuelos más próximos ordenados por FechaHora
            var vuelos = await Task.Run(() => _context.Vuelos.OrderBy(v => v.Fecha).Take(6).ToList());

            /* try
            {
                var noticiasService = new NoticiasSanLuisService();
                var noticias = await noticiasService.ObtenerUltimasNoticiasAsync();
                ViewBag.NoticiasSanLuis = noticias;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener noticias de San Luis");
                ViewBag.NoticiasSanLuis = new List<NoticiaSanLuis>();
            } */

            return View(vuelos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var errorMessage = error?.Error?.Message ?? "Ha ocurrido un error inesperado.";

            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = errorMessage
            });
        }
    }
}
