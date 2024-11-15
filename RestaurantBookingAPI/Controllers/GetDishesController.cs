using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services;

namespace RestaurantBookingAPI.Controllers
{
    [ApiController]
    [Route("Dishes")]
    public class GetDishesController : Controller
    {
        private readonly GetDishesService _getDishesService = new GetDishesService();

        [HttpGet]
        [Route("getAvailability")]

        public async Task<IActionResult> GetAvailableDishes(string restrictions)
        {
            try
            {
                var responseData = await _getDishesService.GetDishes(restrictions);
                return new ContentResult
                {
                    Content = responseData,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error al obtener los platos disponibles: {ex.Message}");
            }
        }
    };
}
