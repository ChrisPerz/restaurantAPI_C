using System.Text;
using Newtonsoft.Json;
using RestaurantBookingAPI.Models;



namespace RestaurantBookingAPI.Services
{
    public class OrderService
    {
        private static readonly HttpClient client = new HttpClient();

        // Método que contiene la lógica para enviar la orden a Oracle APEX
        public async Task SendOrderAsync(OrderRequest orderRequest)
        {

            var url = Environment.GetEnvironmentVariable("UrlOrder");

            int currentDish = 0;

            for(int i = 1; i < orderRequest.DishIds.Length; i++)
            {
                currentDish = orderRequest.DishIds[i-1];


                var orderData = new
                {
                    table_id = orderRequest.TableId,
                    customer_id = orderRequest.CustomerId,
                    dish_id = currentDish,
                    dates = orderRequest.Dates,
                    num_people = orderRequest.Num_people
                };

                var json = JsonConvert.SerializeObject(orderData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if(!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al enviar la orden: {response.StatusCode}");
                }
            };
        }
    }

}
