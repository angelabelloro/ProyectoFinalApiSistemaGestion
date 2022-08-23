using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Controllers.DTOS;
using MiPrimeraApi.Model;
using MiPrimeraApi.Repository;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return new List<Usuario>();
        }

        [HttpDelete]
        public bool EliminarUsuario([FromBody] int id)
        {
            try
            {
                return UsuarioHandler.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut]
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.ModificarUsuario(new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                Mail = usuario.Mail,
            });
        }

        [HttpPost]
        public bool CrearUsuario([FromBody] PostUsuario usuario)
        {
            try
            {
                return UsuarioHandler.CrearUsuario(new Usuario
                {
                    Apellido = usuario.Apellido,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail,
                    Nombre = usuario.Nombre,
                    NombreUsuario = usuario.NombreUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
