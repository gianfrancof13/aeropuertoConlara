using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AeropuertoConlara.Data;
using AeropuertoConlara.Models;
using Microsoft.AspNetCore.Authorization;

namespace AeropuertoConlara.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VuelosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VuelosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vuelos = await _context.Vuelos
                .OrderBy(v => v.Fecha)
                .ThenBy(v => v.HoraArribo)
                .ToListAsync();
            return View(vuelos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vuelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vuelo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelos.FindAsync(id);
            if (vuelo == null)
            {
                return NotFound();
            }
            return View(vuelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vuelo vuelo)
        {
            if (id != vuelo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vuelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VueloExists(vuelo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vuelo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vuelo == null)
            {
                return NotFound();
            }

            return View(vuelo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vuelo = await _context.Vuelos.FindAsync(id);
            if (vuelo != null)
            {
                _context.Vuelos.Remove(vuelo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VueloExists(int id)
        {
            return _context.Vuelos.Any(e => e.Id == id);
        }
    }
}
