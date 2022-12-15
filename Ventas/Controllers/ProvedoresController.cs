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
    public class ProvedoresController : Controller
    {
        private readonly ventasContext _context;

        public ProvedoresController(ventasContext context)
        {
            _context = context;
        }

        // GET: Provedores
        public async Task<IActionResult> Index()
        {
              return _context.Provedores != null ? 
                          View(await _context.Provedores.ToListAsync()) :
                          Problem("Entity set 'ventasContext.Provedores'  is null.");
        }

        // GET: Provedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Provedores == null)
            {
                return NotFound();
            }

            var provedores = await _context.Provedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provedores == null)
            {
                return NotFound();
            }

            return View(provedores);
        }

        // GET: Provedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Provedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,CodigoEmpresa")] Provedores provedores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provedores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provedores);
        }

        // GET: Provedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Provedores == null)
            {
                return NotFound();
            }

            var provedores = await _context.Provedores.FindAsync(id);
            if (provedores == null)
            {
                return NotFound();
            }
            return View(provedores);
        }

        // POST: Provedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,CodigoEmpresa")] Provedores provedores)
        {
            if (id != provedores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provedores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvedoresExists(provedores.Id))
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
            return View(provedores);
        }

        // GET: Provedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Provedores == null)
            {
                return NotFound();
            }

            var provedores = await _context.Provedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provedores == null)
            {
                return NotFound();
            }

            return View(provedores);
        }

        // POST: Provedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Provedores == null)
            {
                return Problem("Entity set 'ventasContext.Provedores'  is null.");
            }
            var provedores = await _context.Provedores.FindAsync(id);
            if (provedores != null)
            {
                _context.Provedores.Remove(provedores);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvedoresExists(int id)
        {
          return (_context.Provedores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
