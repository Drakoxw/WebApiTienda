using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTienda.Models
{
    public class ProductosModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Descripcion { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Precio { get; set; }

        public string Validate()
        {
            string error = "";
            if (this.Descripcion.Length > 150)
            {
                error = "Maximo largo de descripcion excedido";
            }
            if (this.Precio > 999999995)
            {
                error = "precio demasio alto";
            }
            return error;

        }
    }
}
