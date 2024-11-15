using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Models;

namespace RestaurantBookingAPI.Controllers

{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("listar")]
        public dynamic listarCliente()
        {
            List<Usuario> Usuarios = new List<Usuario> {
            new Usuario
            {
                Id = 1,
                Name = "Jose",
                Surname = "Negro",
                Email = "negroJose@gmail.com",
                Phone = 31424412
            },
            new Usuario
            {
                Id = 2,
                Name = "Korean",
                Surname = "Pack",
                Email = "KoreanP@gmail.com",
                Phone = 4555544
            }
        };
            return Usuarios;
        }

        [HttpPost]
        [Route("guardar")]
        public dynamic guardarUsuario(Usuario usuario)
        {
            List<Usuario> Usuarios = new List<Usuario> {
            new Usuario
            {
                Id = 1,
                Name = "Jose",
                Surname = "Negro",
                Email = "negroJose@gmail.com",
                Phone = 31424412
            }
            };
            usuario.Id= Usuarios.Count+1;
            return new
            {
                success = true,
                message = "Usuario registrado",
                result = usuario
            };
          
        }


    }
}
