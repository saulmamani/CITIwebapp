using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CITIwebApp.Context;
using CITIwebApp.Models;
using Microsoft.AspNetCore.Hosting;

namespace CITIwebApp.Controllers
{
    public class IngenieroesController : Controller
    {
        private readonly MiContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IngenieroesController(MiContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Ingenieroes
        public async Task<IActionResult> Index()
        {
              return _context.Ingeniero != null ? 
                          View(await _context.Ingeniero.ToListAsync()) :
                          Problem("Entity set 'MiContext.Ingeniero'  is null.");
        }

        // GET: Ingenieroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ingeniero == null)
            {
                return NotFound();
            }

            var ingeniero = await _context.Ingeniero
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingeniero == null)
            {
                return NotFound();
            }

            return View(ingeniero);
        }

        // GET: Ingenieroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingenieroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rni,Ci,NombreCompleto,FechaRegistro,Monto,Foto,Especialidad")] Ingeniero ingeniero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingeniero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingeniero);
        }

        // GET: Ingenieroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ingeniero == null)
            {
                return NotFound();
            }

            var ingeniero = await _context.Ingeniero.FindAsync(id);
            if (ingeniero == null)
            {
                return NotFound();
            }
            return View(ingeniero);
        }

        // POST: Ingenieroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ingeniero ingeniero)
        {
            if (id != ingeniero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //cargar la foto
                    if (ingeniero.FotoFile != null)
                    {
                        await SubirFoto(ingeniero);
                    }

                    _context.Update(ingeniero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngenieroExists(ingeniero.Id))
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
            return View(ingeniero);
        }

        private async Task SubirFoto(Ingeniero ingeniero)
        {
            //formar el nombre del archivo foto
            string wwRootPath = _webHostEnvironment.WebRootPath;
            string extension = Path.GetExtension(ingeniero.FotoFile!.FileName);
            string nombreFoto = $"{ingeniero.Id}{extension}";

            ingeniero.Foto = nombreFoto;

            //copiar la foto en el proyecto del servidor
            string path = Path.Combine($"{wwRootPath}/fotos/", nombreFoto);
            var fileStream = new FileStream(path, FileMode.Create);
            await ingeniero.FotoFile.CopyToAsync(fileStream);
        }

        // GET: Ingenieroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ingeniero == null)
            {
                return NotFound();
            }

            var ingeniero = await _context.Ingeniero
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingeniero == null)
            {
                return NotFound();
            }

            return View(ingeniero);
        }

        // POST: Ingenieroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ingeniero == null)
            {
                return Problem("Entity set 'MiContext.Ingeniero'  is null.");
            }
            var ingeniero = await _context.Ingeniero.FindAsync(id);
            if (ingeniero != null)
            {
                _context.Ingeniero.Remove(ingeniero);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngenieroExists(int id)
        {
          return (_context.Ingeniero?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
