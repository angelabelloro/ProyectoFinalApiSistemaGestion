using Microsoft.AspNetCore.Mvc;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class TraerNombreController
    {
        [HttpGet]
        public string TraerNombre()
        {
            return "Mi primera API";
        }

    }
}
