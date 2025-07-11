using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace SistemaProductosMVC.Models
{
    public class CategoriaProducto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; }

    // Relación: una categoría tiene muchos productos
    public ICollection<Producto> Productos { get; set; }
}

}

