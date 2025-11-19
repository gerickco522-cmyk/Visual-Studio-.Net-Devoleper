using System.Net.Http;
using System.Text.Json;
using System.Text;
using BE.Request;
using BE.DTO;

namespace BS.WebApplication1
{
    public class WebApplication1
    {
        private readonly HttpClient _httpClient;
        public WebApplication1()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7104/");
        }
        public async Task<CrearOrdenResponseDTO?> CrearOrdenAsync(CrearOrdenRequestDTO request)
        {
            string url = "Home/crear";

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception("Error en la llamada: " + error);
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var reponse = JsonSerializer.Deserialize<CrearOrdenResponseDTO>(jsonResponse);
            //return JsonSerializer.Deserialize<CrearOrdenResponseDTO>(jsonResponse);
            return reponse;
        }
    }
}
