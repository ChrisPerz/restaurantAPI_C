using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services;

namespace RestaurantBookingAPI.Controllers
{
    // Atributos que indican que este controlador es una API y su ruta base
    [ApiController]
    [Route("Login")]
    public class LoginController : ControllerBase
    {
        // Instancia del servicio que maneja la lógica de autenticación
        private readonly LoginService _loginService = new LoginService();

        // Acción GET que maneja el login de un usuario
        // Ruta: /Login/User
        [HttpGet]
        [Route("User")]
        public async Task<IActionResult> loginUser(string username, string password)
        {
            try
            {
                // Llama al servicio para enviar las credenciales de login y obtener la respuesta
                var res = await _loginService.SendLogin(username, password);

                // Si no se encuentran resultados, devuelve un código de estado 404 (No Encontrado)
                if(res == "[]")
                {
                    return NotFound();
                }

                // Devuelve la respuesta de login con los datos de usuario en formato JSON
                return new ContentResult
                {
                    Content = res,
                    ContentType = "application/json",
                    StatusCode = 200 // Código de éxito
                };
            }
            catch(Exception ex)
            {
                // En caso de error, devuelve un código de estado 500 con un mensaje detallado
                return StatusCode(500, $"Error al obtener los datos de login: {ex.Message}");
            }
        }
    }
}
