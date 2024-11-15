using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestaurantBookingAPI.Models;
using System.Text;

namespace RestaurantBookingAPI.Services
{
    public class LoginService
    {
        // Cliente HTTP estático para hacer solicitudes a la API
        private static readonly HttpClient client = new HttpClient();

        // Método asincrónico que envía las credenciales de inicio de sesión y obtiene la respuesta
        public async Task<string> SendLogin(string inname, string Inpassword)
        {
            // Obtiene la URL base desde las variables de entorno
            var urlDB = Environment.GetEnvironmentVariable("UrlLogin");

            // Construye la URL completa con las credenciales de usuario y contraseña
            var url = $"{urlDB}/{Inpassword}/{inname}";

            // Realiza la solicitud HTTP GET
            HttpResponseMessage response = await client.GetAsync(url);

            // Si la respuesta es exitosa (código de estado 2xx)
            if(response.IsSuccessStatusCode)
            {
                // Lee el contenido de la respuesta como string
                var responseData = await response.Content.ReadAsStringAsync();

                // Analiza el JSON recibido y extrae los items
                var jsonObject = JObject.Parse(responseData);
                var items = jsonObject["items"]?.ToObject<List<Dictionary<string, object>>>();

                // Si no hay items, retorna un array vacío
                if(items == null || items.Count == 0)
                {
                    return "[]";
                }

                // Convierte los items a JSON y los retorna
                return JsonConvert.SerializeObject(items);
            }
            else
            {
                // Lanza una excepción si la respuesta no es exitosa
                throw new Exception($"Error al intentar acceder: {response.StatusCode}");
            }
        }
    }
}
