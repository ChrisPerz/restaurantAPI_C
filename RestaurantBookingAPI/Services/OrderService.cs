using System.Text;
using Newtonsoft.Json;
using RestaurantBookingAPI.Models;

namespace RestaurantBookingAPI.Services
{
    public class OrderService
    {
        // Cliente HTTP estático para realizar solicitudes POST a la API de Oracle APEX
        private static readonly HttpClient client = new HttpClient();

        // Método que envía una orden a Oracle APEX utilizando la información del objeto OrderRequest
        public async Task SendOrderAsync(OrderRequest orderRequest)
        {
            // Obtiene la URL de la API de Oracle APEX desde las variables de entorno
            var url = Environment.GetEnvironmentVariable("UrlOrder");

            // Variable para manejar el plato actual de la orden
            int currentDish = 0;

            // Itera sobre todos los platos en la orden
            for(int i = 0; i < orderRequest.DishIds.Length; i++)
            {
                currentDish = orderRequest.DishIds[i];

                // Crea un objeto con la información de la orden para enviarla
                var orderData = new
                {
                    table_id = orderRequest.TableId,
                    customer_id = orderRequest.CustomerId,
                    dish_id = currentDish,
                    dates = orderRequest.Dates,
                    num_people = orderRequest.Num_people
                };

                // Convierte el objeto orderData a formato JSON
                var json = JsonConvert.SerializeObject(orderData);

                // Crea el contenido de la solicitud HTTP con el JSON serializado
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Envía la solicitud POST a la API
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Si la respuesta no es exitosa, lanza una excepción
                if(!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al enviar la orden: {response.StatusCode}");
                }
            };
        }
    }
}
