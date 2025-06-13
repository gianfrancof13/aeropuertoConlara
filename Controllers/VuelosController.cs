using Microsoft.AspNetCore.Mvc;
using AeropuertoConlara.Models;
using AeropuertoConlara.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;

namespace AeropuertoConlara.Controllers
{
    [Authorize]
    public class VuelosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VuelosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vuelos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vuelos.OrderByDescending(v => v.FechaHora).ToListAsync());
        }

        // GET: Vuelos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var vuelo = await _context.Vuelos.FirstOrDefaultAsync(m => m.Id == id);
            if (vuelo == null) return NotFound();
            return View(vuelo);
        }

        // GET: Vuelos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vuelos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Aerolinea,NumeroVuelo,Destino,FechaHora,Estado")] Vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                vuelo.UltimaActualizacion = DateTime.Now;
                _context.Add(vuelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vuelo);
        }

        // GET: Vuelos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var vuelo = await _context.Vuelos.FindAsync(id);
            if (vuelo == null) return NotFound();
            return View(vuelo);
        }

        // POST: Vuelos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Aerolinea,NumeroVuelo,Destino,FechaHora,Estado,UltimaActualizacion")] Vuelo vuelo)
        {
            if (id != vuelo.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    vuelo.UltimaActualizacion = DateTime.Now;
                    _context.Update(vuelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Vuelos.Any(e => e.Id == vuelo.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vuelo);
        }

        // GET: Vuelos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var vuelo = await _context.Vuelos.FirstOrDefaultAsync(m => m.Id == id);
            if (vuelo == null) return NotFound();
            return View(vuelo);
        }

        // POST: Vuelos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vuelo = await _context.Vuelos.FindAsync(id);
            if (vuelo != null)
            {
                _context.Vuelos.Remove(vuelo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
