// Usamos los espacios de nombres necesarios
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaProductosMVC.Data;
using SistemaProductosMVC.Models;

namespace SistemaProductosMVC.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Mostrar lista de productos con su categoría
        public async Task<IActionResult> Index()
        {
            var productos = _context.Productos.Include(p => p.CategoriaProducto);
            return View(await productos.ToListAsync());
        }

        // Mostrar formulario de creación
        public IActionResult Create()
        {
            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriasProducto, "Id", "Nombre");
            return View();
        }

        // Crear producto (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,CategoriaProductoId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriasProducto, "Id", "Nombre", producto.CategoriaProductoId);
            return View(producto);
        }

        // Mostrar formulario de edición
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriasProducto, "Id", "Nombre", producto.CategoriaProductoId);
            return View(producto);
        }

        // Editar producto (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio,CategoriaProductoId")] Producto producto)
        {
            if (id != producto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Productos.Any(e => e.Id == producto.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriasProducto, "Id", "Nombre", producto.CategoriaProductoId);
            return View(producto);
        }

        // Confirmar eliminación
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos.Include(p => p.CategoriaProducto).FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null) return NotFound();

            return View(producto);
        }

        // Eliminar producto (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
