using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;

namespace RestaurantBookingAPI.Services
{
    public class getAvailableTablesService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> GetTables(string date, string zone, int customers_number)
        {
            var urlDB =Environment.GetEnvironmentVariable("UrlGetTables");
            string url = $"{urlDB}/{date}/{zone}/{customers_number}";

            HttpResponseMessage response = await client.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync(); // Obtener el JSON como string

                var jsonObject = JObject.Parse(responseData);
                var items = jsonObject["items"]?.ToObject<List<Dictionary<string, object>>>();

                // Si items es null, retorna un mensaje vacío o un JSON vacío.
                if(items == null || items.Count == 0)
                {
                    return "[]"; // Retorna un array vacío si no hay items
                }

                // Convertir items a JSON y retornar como string
                return JsonConvert.SerializeObject(items);

            }

            throw new Exception($"Error al obtener las mesas disponibles: {response.StatusCode}");            

        }
    }
}
