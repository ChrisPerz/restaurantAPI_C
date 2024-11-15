using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services;
using RestaurantBookingAPI.Models;

namespace RestaurantBookingAPI.Controllers
{
    [ApiController]
    [Route("register")]
    public class RegisterUserController : Controller
    {
        private readonly RegisterService _registerService = new RegisterService();

        [HttpPost]
        [Route("User")]

        public async Task<IActionResult> RegisterUser(string name, string password, string usertype, int phoneNumber, string Email)
        {
            try
            {
                await _registerService.SendRegistration(name, password,usertype, phoneNumber, Email);
                return new ContentResult
                {
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error al registrar el usuario: {ex.Message}");
            }
        }

    }
}
