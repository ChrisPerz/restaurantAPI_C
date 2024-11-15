using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services;
using RestaurantBookingAPI.Models;
using Newtonsoft.Json;

namespace RestaurantBookingAPI.Controllers
{
    // Atributos que indican que este controlador es una API y su ruta base
    [ApiController]
    [Route("orders")]
    public class OrdersController : Controller
    {
        // Instancia del servicio que maneja la lógica de procesamiento de órdenes
        private readonly OrderService _orderService = new OrderService();

        // Acción POST que maneja la creación de una nueva orden
        // Ruta: /orders/sendOrder
        [HttpPost]
        [Route("sendOrder")]
        public async Task<IActionResult> CreateOrder(OrderRequest orderRequest)
        {
            try
            {
                // Llama al servicio para enviar la orden y procesarla
                await _orderService.SendOrderAsync(orderRequest);

                // Devuelve la orden recibida en formato JSON con un código de éxito 200
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(orderRequest),
                    ContentType = "application/json",
                    StatusCode = 200 // Código de éxito
                };
            }
            catch(Exception ex)
            {
                // En caso de error, devuelve un código de estado 500 con un mensaje detallado
                return StatusCode(500, $"Error al hacer la orden: {ex.Message}");
            }
        }
    }
}
