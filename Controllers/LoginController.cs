using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Repository;
using MiPrimeraApi.Controllers.DTOS;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LoginController: ControllerBase
    {
        [HttpGet]
        public bool Login([FromBody] Login login)
        {
            try
            {
               return LoginHandler.Login(new Login
               {
                   Usuario = login.Usuario,
                   Contraseña = login.Contraseña
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
