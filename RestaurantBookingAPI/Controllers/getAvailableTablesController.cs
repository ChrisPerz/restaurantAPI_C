using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services;

namespace RestaurantBookingAPI.Controllers
{
    [ApiController]
    [Route("Tables")]
    public class getAvailableTablesController : Controller
    {
        private readonly getAvailableTablesService _getAvailableTablesService = new getAvailableTablesService();

        [HttpGet]
        [Route("getAvailability")]

        public async Task <IActionResult> GetAvailableTables(string date, string zone, int customers_number)
        {
            try
            {
                var responseData = await _getAvailableTablesService.GetTables(date, zone, customers_number);
                return new ContentResult
                {
                    Content = responseData,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error al obtener las mesas disponibles: {ex.Message}");
            }
        }      
    };
}
