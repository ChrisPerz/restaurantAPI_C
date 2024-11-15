using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services;
using RestaurantBookingAPI.Models;
using Newtonsoft.Json;

namespace RestaurantBookingAPI.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService = new OrderService();

        [HttpPost]
        [Route("sendOrder")]
        public async Task<IActionResult> CreateOrder(OrderRequest orderRequest)
        {
            try
            {
                await _orderService.SendOrderAsync(orderRequest);
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(orderRequest),
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error al hacer la orden: {ex.Message}");
            }
        }
    }
}
