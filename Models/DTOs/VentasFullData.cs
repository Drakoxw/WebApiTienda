namespace WebApiTienda.Models.DTOs
{
    public class VentasFullData
    {
        public int ItemId { get; set; }

        public int VentaId { get; set; }

        public int ProductoId { get; set; }

        public string? DescProduct { get; set; }

        public int NumeroItems { get; set; }

        public int Precio { get; set; }

        public int Total { get; set; }
    }
}
