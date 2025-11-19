namespace WebApplication1.Services
{

    public class PasarelaPagoService 
    {
        public async Task<bool> ProcesarPagoAsync(decimal monto, string metodoPago)
        {
            await Task.Delay(800); // Simula retardo del servicio externo
            return true; // Simula un pago exitoso
        }
    }
}
