using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestaurantBookingAPI.Models;
using System.Text;
using System.Xml.Linq;

namespace RestaurantBookingAPI.Services

{
    public class RegisterService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task SendRegistration(string name, string password, string usertype, int phoneNumber, string Email)
        {
            
            var url = Environment.GetEnvironmentVariable("UrlRegister");

            var urlDB = Environment.GetEnvironmentVariable("UrlVerification");

            var verificationUserInDbURL = $"{urlDB}/{name}";

            HttpResponseMessage verificationResponse = await client.GetAsync(verificationUserInDbURL);

            var existingUser = await verificationResponse.Content.ReadAsStringAsync(); // Obtener el JSON como string

            var jsonObject = JObject.Parse(existingUser);

            var items = jsonObject["items"]?.ToObject<List<Dictionary<string, object>>>();

            if(items == null || !items.Any())
            {

                var datosConcatenados = $"Name={name}&Password={password}&UserType={usertype}&PhoneNumber={phoneNumber}&Email={Email}";

                var content = new StringContent(datosConcatenados, Encoding.UTF8, "application/x-www-form-urlencoded");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if(!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al registrar usuario: {response.StatusCode}");
                }
                else
                {
                    Console.WriteLine("Usuario registrado exitosamente.");
                }

            } else {
                throw new Exception($"El usuario ya existe en la DB: {verificationResponse.StatusCode}");
            }

        }
    }
}
