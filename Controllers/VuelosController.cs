using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AeropuertoConlara.Models;

namespace AeropuertoConlara.Controllers
{
    public class VuelosController : Controller
    {
        public IActionResult Index()
        {
            var vuelos = new List<Vuelo>
            {
                new Vuelo { Id = 1, Aerolinea = "Aerolinea1", NumeroVuelo = "V001", Destino = "Destino1", FechaHora = System.DateTime.Now, Estado = "Programado" },
                new Vuelo { Id = 2, Aerolinea = "Aerolinea2", NumeroVuelo = "V002", Destino = "Destino2", FechaHora = System.DateTime.Now.AddHours(1), Estado = "Aterrizado" }
            };
            return View(vuelos);
        }
    }
}
