using BE.DTO;
using BE.Request;
using BS.WebApplication1;

namespace BL.Services
{
    public class WebApplication1Services
    {
        public CrearOrdenResponseDTO CrearOrden(CrearOrdenRequestDTO request)
        {
            CrearOrdenResponseDTO crearOrdenResponseDTO = new CrearOrdenResponseDTO();
            var service = new WebApplication1();
            crearOrdenResponseDTO = service.CrearOrdenAsync(request).Result;
            return crearOrdenResponseDTO;
        }
    }
}
