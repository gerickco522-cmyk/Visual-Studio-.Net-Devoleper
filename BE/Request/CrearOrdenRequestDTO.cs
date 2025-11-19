using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Request
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