using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace WebApiTienda.Models
{
    public class LogsModel
    {
        public int Id { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Fecha { get; set; } = DateTime.Now;
        [MaxLength(30)]
        public string Env { get; set; } = Convert.ToString(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "").ToUpper();
        [MaxLength(50)]
        public string Tipo { get; set; }
        [MaxLength(3000)]
        public string Mensaje { get; set;}

        public int IdUser { get; set; }

        [MaxLength(8000)]
        public string? Data { get; set; }

        [MaxLength(50)]
        public string? Evento { get; set; }

    }
}
