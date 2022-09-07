namespace MiPrimeraApi.Controllers.DTOS
{
    public class GetAllVentas
    {
        public int IdVenta { get; set; }
        public string VentaComentario { get; set; }
        public int ProductoVendidoId { get; set; }
        public int ProductoVendidoStock { get; set; }
        public int ProductoId { get; set; }
        public string ProductoDescripciones { get; set; }
        public double ProductoCosto { get; set; }
        public double PrecioVenta { get; set; }
        public int ProductoStock { get; set; }

    }
}

