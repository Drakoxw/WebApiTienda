using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApiTienda.Models
{
    public class UsuariosModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string User { get; set; }


        [Required]
        [Column(TypeName = "varchar(300)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Role { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime CreateAt { get; set; }

        [AllowNull]
        [Column(TypeName = "date")]
        [DefaultValue(null)]
        public DateTime? DeleteAt { get; set; }
    }
}
