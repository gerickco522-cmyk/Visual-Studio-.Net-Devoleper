namespace WebApplication1.Models
{
    public class CrearOrdenRequestDTO
    {
        public string ClienteId { get; set; }
        public List<ItemOrdenDTO> Items { get; set; }
        public string MetodoPago { get; set; }
    }

    public class ItemOrdenDTO
    {
        public string ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}