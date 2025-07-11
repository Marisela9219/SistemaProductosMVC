using Microsoft.EntityFrameworkCore;
using SistemaProductosMVC.Models;

namespace SistemaProductosMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet = Tabla en la base de datos
        public DbSet<Producto> Productos { get; set; }
        public DbSet<CategoriaProducto> CategoriasProducto { get; set; }
    }
}
