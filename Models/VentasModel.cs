using System.ComponentModel.DataAnnotations;

namespace WebApiTienda.Models
{
    public class VentasModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        public int Total { get; set; }

        [Required]
        public int ClienteId { get; set; }
        public UsuariosModel Cliente { get; set; }
    }
}
