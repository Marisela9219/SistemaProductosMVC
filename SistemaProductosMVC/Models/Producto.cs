using System.ComponentModel.DataAnnotations;

namespace SistemaProductosMVC.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Range(0.01, 10000)]
        public decimal Precio { get; set; }

        [Display(Name = "Categoría")]
        public int CategoriaProductoId { get; set; }

        // Relación con la categoría
        public CategoriaProducto CategoriaProducto { get; set; }
    }
}
