using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services;

namespace RestaurantBookingAPI.Controllers
{
    // Atributos que indican que este controlador es una API y su ruta base
    [ApiController]
    [Route("Dishes")]
    public class GetDishesController : Controller
    {
        // Instancia del servicio que contiene la lógica para obtener los platos disponibles
        private readonly GetDishesService _getDishesService = new GetDishesService();

        // Acción GET que obtiene los platos disponibles según restricciones dietéticas
        // Ruta: /Dishes/getAvailability
        [HttpGet]
        [Route("getAvailability")]
        public async Task<IActionResult> GetAvailableDishes(string restrictions)
        {
            try
            {
                // Llama al servicio para obtener los platos disponibles, filtrados por las restricciones proporcionadas
                var responseData = await _getDishesService.GetDishes(restrictions);

                // Devuelve la respuesta con los datos de los platos en formato JSON
                return new ContentResult
                {
                    Content = responseData,
                    ContentType = "application/json",
                    StatusCode = 200 // Código de éxito
                };
            }
            catch(Exception ex)
            {
                // En caso de error, devuelve un código de estado 500 con un mensaje detallado
                return StatusCode(500, $"Error al obtener los platos disponibles: {ex.Message}");
            }
        }
    }
};
