using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestaurantBookingAPI.Models;
using System.Text;
namespace RestaurantBookingAPI.Services
{
    public class LoginService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> SendLogin(string inname, string Inpassword)
        {

            var urlDB = Environment.GetEnvironmentVariable("UrlLogin");
            var url = $"{urlDB}/{Inpassword}/{inname}";

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
            else
            {
                throw new Exception($"Error al intentar acceder: {response.StatusCode}");
            }
        }
    }
}
