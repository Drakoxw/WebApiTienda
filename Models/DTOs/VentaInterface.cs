namespace WebApiTienda.Models.DTOs
{
    public class VentaInterface
    {
        public int ClienteId { get; set; }
        public int Total { get; set; }
        public List<ItemMin> Items { get; set; }
    }
    public class ItemMin
    {
        public int NumeroItems { get; set; }
        public int ProductoId { get; set; }
    }
}
