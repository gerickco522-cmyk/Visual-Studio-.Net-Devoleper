using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMessage()
        {
            var message = new
            {
                Message = "Bienvenido a mi API 😎",
                Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Version = "1.0"
            };

            return Ok(message);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearOrden([FromBody] CrearOrdenRequestDTO request)
        {
            // 1. Validar request
            if (request == null)
                return BadRequest("El request no puede ser nulo.");

            if (string.IsNullOrEmpty(request.ClienteId))
                return BadRequest("Debe enviar el ClienteId.");

            if (request.Items == null || request.Items.Count == 0)
                return BadRequest("Debe existir al menos un producto en la orden.");

            // 2. Calcular total de la orden
            decimal total = 0;
            foreach (var item in request.Items)
            {
                // Simulación: cada producto cuesta $12
                total += item.Cantidad * 12;
            }

            // 3. Simular llamado a pasarela de pago
            bool pagoAprobado = await SimularPagoAsync(total, request.MetodoPago);

            if (!pagoAprobado)
                return StatusCode(402, "El pago fue rechazado.");

            // 4. Simular guardado en "base de datos"
            string numeroOrden = Guid.NewGuid().ToString("N").Substring(0, 10);

            // 5. Registrar auditoría (simulado)
            var auditoria = new
            {
                Fecha = DateTime.Now,
                Accion = "CREACION_ORDEN",
                Cliente = request.ClienteId,
                TotalPagado = total,
                OrdenGenerada = numeroOrden
            };

            Console.WriteLine("AUDITORÍA:");
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(auditoria));

            // 6. Respuesta final al cliente
            return Ok(new
            {
                Mensaje = "La orden fue creada exitosamente.",
                NumeroOrden = numeroOrden,
                Total = total,
                Pago = "Aprobado",
                Fecha = DateTime.Now
            });
        }

        // Método auxiliar privado
        private async Task<bool> SimularPagoAsync(decimal monto, string metodoPago)
        {
            await Task.Delay(700); // Simula la comunicación externa
            return true; // Siempre aprobado
        }
    }
}

