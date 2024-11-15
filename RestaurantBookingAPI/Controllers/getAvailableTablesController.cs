using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services;

namespace RestaurantBookingAPI.Controllers
{
    // Atributos de clase que indican que esta es una API y la ruta base para este controlador
    [ApiController]
    [Route("Tables")]
    public class getAvailableTablesController : Controller
    {
        // Instancia del servicio que proporciona la lógica de negocio para obtener las mesas disponibles
        private readonly getAvailableTablesService _getAvailableTablesService = new getAvailableTablesService();

        // Acción GET que se encarga de obtener las mesas disponibles según los parámetros de fecha, zona y número de clientes
        // Ruta: /Tables/getAvailability
        [HttpGet]
        [Route("getAvailability")]
        public async Task<IActionResult> GetAvailableTables(string date, string zone, int customers_number)
        {
            try
            {
                // Llama al servicio para obtener los datos de disponibilidad de mesas
                var responseData = await _getAvailableTablesService.GetTables(date, zone, customers_number);

                // Devuelve la respuesta con los datos obtenidos en formato JSON
                return new ContentResult
                {
                    Content = responseData,
                    ContentType = "application/json",
                    StatusCode = 200 // Código de éxito
                };
            }
            catch(Exception ex)
            {
                // Si ocurre un error, devuelve un código de error 500 con un mensaje descriptivo
                return StatusCode(500, $"Error al obtener las mesas disponibles: {ex.Message}");
            }
        }
    }
};
