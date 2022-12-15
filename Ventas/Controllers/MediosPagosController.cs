using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ventas.Context;
using Ventas.Moldels;

namespace Ventas.Controllers
{
    public class MediosPagosController : Controller
    {
        private readonly ventasContext _context;

        public MediosPagosController(ventasContext context)
        {
            _context = context;
        }

        // GET: MediosPagos
        public async Task<IActionResult> Index()
        {
              return _context.MediosPagos != null ? 
                          View(await _context.MediosPagos.ToListAsync()) :
                          Problem("Entity set 'ventasContext.MediosPagos'  is null.");
        }

        // GET: MediosPagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MediosPagos == null)
            {
                return NotFound();
            }

            var mediosPagos = await _context.MediosPagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediosPagos == null)
            {
                return NotFound();
            }

            return View(mediosPagos);
        }

        // GET: MediosPagos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MediosPagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedioPago")] MediosPagos mediosPagos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediosPagos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mediosPagos);
        }

        // GET: MediosPagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MediosPagos == null)
            {
                return NotFound();
            }

            var mediosPagos = await _context.MediosPagos.FindAsync(id);
            if (mediosPagos == null)
            {
                return NotFound();
            }
            return View(mediosPagos);
        }

        // POST: MediosPagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedioPago")] MediosPagos mediosPagos)
        {
            if (id != mediosPagos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediosPagos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediosPagosExists(mediosPagos.Id))
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
            return View(mediosPagos);
        }

        // GET: MediosPagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MediosPagos == null)
            {
                return NotFound();
            }

            var mediosPagos = await _context.MediosPagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediosPagos == null)
            {
                return NotFound();
            }

            return View(mediosPagos);
        }

        // POST: MediosPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MediosPagos == null)
            {
                return Problem("Entity set 'ventasContext.MediosPagos'  is null.");
            }
            var mediosPagos = await _context.MediosPagos.FindAsync(id);
            if (mediosPagos != null)
            {
                _context.MediosPagos.Remove(mediosPagos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediosPagosExists(int id)
        {
          return (_context.MediosPagos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
