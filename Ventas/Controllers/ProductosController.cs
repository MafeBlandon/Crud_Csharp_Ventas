using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ventas.Context;
using Ventas.Moldels;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Ventas.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ventasContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductosController(ventasContext context, IWebHostEnvironment webHost)
        {

            _context = context;
            webHostEnvironment = webHost;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var ventasContext = _context.Productos.Include(p => p.ProvedorNavigation);
            return View(await ventasContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .Include(p => p.ProvedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["Provedor"] = new SelectList(_context.Provedores, "Id", "CodigoEmpresa");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Productos productos)
        {
            if (ModelState.IsValid)
            {
                string uFilename = UploadedFile(productos);
                productos.imagen = uFilename;
                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Provedor"] = new SelectList(_context.Provedores, "Id", "CodigoEmpresa", productos.Provedor);
            return View(productos);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            ViewData["Provedor"] = new SelectList(_context.Provedores, "Id", "CodigoEmpresa", productos.Provedor);
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Valor,imagen,Provedor")] Productos productos)
        {
            if (id != productos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.Id))
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
            ViewData["Provedor"] = new SelectList(_context.Provedores, "Id", "CodigoEmpresa", productos.Provedor);
            return View(productos);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .Include(p => p.ProvedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'ventasContext.Productos'  is null.");
            }
            var productos = await _context.Productos.FindAsync(id);
            if (productos != null)
            {
                _context.Productos.Remove(productos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
          return (_context.Productos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private string UploadedFile(Productos productos)
        {
            string uFileNme = null;
            if (productos.ImagenFile != null)
            {
                string uploadsdFolder = Path.Combine(webHostEnvironment.WebRootPath, "Img");
                uFileNme = Guid.NewGuid().ToString() + "_" + productos.ImagenFile.FileName;
                string filePath = Path.Combine(uploadsdFolder, uFileNme);
                using (var myFileStream = new FileStream(filePath, FileMode.Create))
                {
                    productos.ImagenFile.CopyTo(myFileStream);
                }
            }
            return uFileNme;
        }
    }
}
