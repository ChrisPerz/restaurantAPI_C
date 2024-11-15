using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestaurantBookingAPI.Models;
using System.Text;
using System.Xml.Linq;

namespace RestaurantBookingAPI.Services
{
    public class RegisterService
    {
        // Cliente HTTP estático para realizar solicitudes a las APIs
        private static readonly HttpClient client = new HttpClient();

        // Método para registrar un nuevo usuario en la base de datos si no existe
        public async Task SendRegistration(string name, string password, string usertype, int phoneNumber, string Email)
        {
            // URL de la API para registrar el usuario
            var url = Environment.GetEnvironmentVariable("UrlRegister");

            // URL para verificar si el usuario ya existe en la base de datos
            var urlDB = Environment.GetEnvironmentVariable("UrlVerification");
            var verificationUserInDbURL = $"{urlDB}/{name}";

            // Verificación de si el usuario ya existe
            HttpResponseMessage verificationResponse = await client.GetAsync(verificationUserInDbURL);
            var existingUser = await verificationResponse.Content.ReadAsStringAsync(); // Obtener el JSON de la respuesta

            // Parsear la respuesta JSON para comprobar si el usuario ya existe
            var jsonObject = JObject.Parse(existingUser);
            var items = jsonObject["items"]?.ToObject<List<Dictionary<string, object>>>();

            // Si el usuario no existe, procede con el registro
            if(items == null || !items.Any())
            {
                // Concatenar los datos del usuario para enviarlos en la solicitud POST
                var datosConcatenados = $"Name={name}&Password={password}&UserType={usertype}&PhoneNumber={phoneNumber}&Email={Email}";

                // Crear contenido de la solicitud en formato URL codificada
                var content = new StringContent(datosConcatenados, Encoding.UTF8, "application/x-www-form-urlencoded");

                // Enviar la solicitud POST para registrar el usuario
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Verificar si la solicitud fue exitosa
                if(!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al registrar usuario: {response.StatusCode}");
                }
                else
                {
                    Console.WriteLine("Usuario registrado exitosamente.");
                }
            }
            else
            {
                // Si el usuario ya existe, lanzar una excepción
                throw new Exception($"El usuario ya existe en la DB: {verificationResponse.StatusCode}");
            }
        }
    }
}
