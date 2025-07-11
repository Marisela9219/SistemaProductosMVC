using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaProductosMVC.Data;
using SistemaProductosMVC.Models;

namespace SistemaProductosMVC.Controllers
{
    public class CategoriaProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Listado de categorías
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriasProducto.ToListAsync());
        }

        // Formulario para crear
        public IActionResult Create()
        {
            return View();
        }

        // Crear categoría (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] CategoriaProducto categoriaProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaProducto);
        }

        // Formulario para editar
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var categoriaProducto = await _context.CategoriasProducto.FindAsync(id);
            if (categoriaProducto == null) return NotFound();

            return View(categoriaProducto);
        }

        // Editar categoría (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] CategoriaProducto categoriaProducto)
        {
            if (id != categoriaProducto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.CategoriasProducto.Any(e => e.Id == categoriaProducto.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaProducto);
        }

        // Confirmar eliminación
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var categoriaProducto = await _context.CategoriasProducto.FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaProducto == null) return NotFound();

            return View(categoriaProducto);
        }

        // Eliminar categoría (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaProducto = await _context.CategoriasProducto.FindAsync(id);
            _context.CategoriasProducto.Remove(categoriaProducto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
