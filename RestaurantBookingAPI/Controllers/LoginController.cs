using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services;



namespace RestaurantBookingAPI.Controllers
{
    [ApiController]
    [Route("Login")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService = new LoginService();

        [HttpGet]
        [Route("User")]

        public async Task<IActionResult> loginUser(string username, string password)
        {
            try
            {
               var res = await _loginService.SendLogin(username, password);
                if(res == "[]") { 
                    return NotFound();
                }
                return new ContentResult
                {
                    Content = res,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error al obtener las mesas disponibles: {ex.Message}");
            }
        }
    }
}
