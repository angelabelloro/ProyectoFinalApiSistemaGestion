using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Controllers.DTOS;
using MiPrimeraApi.Model;
using MiPrimeraApi.Repository;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class VentaController: ControllerBase
    {
        [HttpGet (Name = "GetVentas")]
        public List<GetAllVentas> GetVentas()
        {
            try
            {
                return VentaHandler.GetVentas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        [HttpPost]
        public bool CargarVenta([FromBody] PostProductoVendido postProducto)
        {
            List<ProductoVendido> ventaList = new List<ProductoVendido>();
            return VentaHandler.CargarVenta(ventaList);
        }
        
        [HttpPut(Name = "ModificarVenta")]
        public void ModificarVenta([FromBody] PutVenta venta)
        {
            VentaHandler.ModificarVenta(new Venta
            {
                Comentarios = venta.Comentarios
            });
        }


        [HttpDelete(Name = "EliminarVenta")]
        public void EliminarVenta([FromBody] int Id)
        {
            VentaHandler.EliminarVenta(Id);
        }


    }
}
