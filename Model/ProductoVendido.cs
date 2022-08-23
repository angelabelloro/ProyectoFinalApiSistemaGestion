namespace MiPrimeraApi.Model
{
    public class ProductoVendido:Producto
    {
        public int Id { get; set; }
        public long IdVenta { get; set; }
    }
}
