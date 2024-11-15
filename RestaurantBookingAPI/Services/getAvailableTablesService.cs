using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;

namespace RestaurantBookingAPI.Services
{
    // Servicio que obtiene las mesas disponibles desde una URL externa
    public class getAvailableTablesService
    {
        // Cliente HTTP estático para realizar solicitudes a la API externa
        private static readonly HttpClient client = new HttpClient();

        // Método asincrónico que obtiene las mesas disponibles según fecha, zona y número de clientes
        public async Task<string> GetTables(string date, string zone, int customers_number)
        {
            // Obtiene la URL base desde las variables de entorno (configuración)
            var urlDB = Environment.GetEnvironmentVariable("UrlGetTables");

            // Construye la URL completa utilizando los parámetros proporcionados
            string url = $"{urlDB}/{date}/{zone}/{customers_number}";

            // Realiza una solicitud GET a la URL construida
            HttpResponseMessage response = await client.GetAsync(url);

            // Si la respuesta es exitosa (código de estado 2xx)
            if(response.IsSuccessStatusCode)
            {
                // Lee el contenido de la respuesta como una cadena JSON
                var responseData = await response.Content.ReadAsStringAsync();

                // Analiza el JSON recibido para convertirlo en un objeto JObject
                var jsonObject = JObject.Parse(responseData);

                // Extrae los elementos de "items" y los convierte a una lista de diccionarios
                var items = jsonObject["items"]?.ToObject<List<Dictionary<string, object>>>();

                // Si no se encuentran elementos o si items es null, retorna un array vacío
                if(items == null || items.Count == 0)
                {
                    return "[]"; // Retorna un array vacío si no hay items
                }

                // Convierte los items a JSON y los retorna como una cadena
                return JsonConvert.SerializeObject(items);
            }

            // Si la respuesta no es exitosa, lanza una excepción con el mensaje de error
            throw new Exception($"Error al obtener las mesas disponibles: {response.StatusCode}");
        }
    }
}
