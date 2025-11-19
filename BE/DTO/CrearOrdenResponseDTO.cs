using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DTO
{
    public class CrearOrdenResponseDTO
    {
        public string mensaje { get; set; } 
        public string numeroOrden { get; set; }
        public decimal total { get; set; }
        public string pago { get; set; }
        public DateTime fecha { get; set; }
    }
}
