using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services;
using RestaurantBookingAPI.Models;

namespace RestaurantBookingAPI.Controllers
{
    // Atributos que indican que este controlador es una API y su ruta base
    [ApiController]
    [Route("register")]
    public class RegisterUserController : Controller
    {
        // Instancia del servicio que maneja la lógica de registro de usuarios
        private readonly RegisterService _registerService = new RegisterService();

        // Acción POST que maneja el registro de un nuevo usuario
        // Ruta: /register/User
        [HttpPost]
        [Route("User")]
        public async Task<IActionResult> RegisterUser(string name, string password, string usertype, int phoneNumber, string Email)
        {
            try
            {
                // Llama al servicio para registrar al nuevo usuario con los datos proporcionados
                await _registerService.SendRegistration(name, password, usertype, phoneNumber, Email);

                // Devuelve un código de estado 200 (OK) indicando que el registro fue exitoso
                return new ContentResult
                {
                    ContentType = "application/json",
                    StatusCode = 200 // Código de éxito
                };
            }
            catch(Exception ex)
            {
                // En caso de error, devuelve un código de estado 500 con un mensaje detallado
                return StatusCode(500, $"Error al registrar el usuario: {ex.Message}");
            }
        }
    }
}
