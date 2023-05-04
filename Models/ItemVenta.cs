using Microsoft.Build.Framework;

namespace WebApiTienda.Models
{
    public class ItemVenta
    {
        public int Id { get; set; }

        public int NumeroItems { get; set; }

        [Required]
        public int VentaId { get; set; }
        public VentasModel Venta { get; set; }

        [Required]
        public int ProductoId { get; set; }
        public ProductosModel Producto { get; set; }
    }
}
