using MiPrimeraApi.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public class ProductoVendidoHandler
    {
        public const string ConnectionString = "Server=DESKTOP-6KIGGOG\\SQLEXPRESS;Database=SistemaGestion;Trusted_Connection=True";


        public static bool CrearProductoVendido(ProductoVendido productoVendido)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;

                    sqlConnection.Open();

                    //Agregar los productos vendidos.
                    sqlCommand.CommandText = "INSERT INTO ProductoVendido (Stock, IdProducto , IdVenta) VALUES (@stock, @idproducto , @idventa );";

                    SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.Int) { Value = productoVendido.Stock };
                    SqlParameter IdProductoParameter = new SqlParameter("idproducto", SqlDbType.Int) { Value = productoVendido.Id };
                    SqlParameter idVentaParameter = new SqlParameter("idventa", SqlDbType.Int) { Value = productoVendido.IdVenta };
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(IdProductoParameter);
                    sqlCommand.Parameters.Add(idVentaParameter);
                    int recordsAffected = sqlCommand.ExecuteNonQuery();

                    //Actualiza el stock de la tabla Productos.
                    sqlCommand.CommandText = "UPDATE Producto SET Producto.Stock = Producto.Stock - ProductoVendido.Stock " +
                        "FROM Producto INNER JOIN ProductoVendido on(Producto.Id = ProductoVendido.IdProducto)";
                    recordsAffected = sqlCommand.ExecuteNonQuery();

                    if (recordsAffected > 0)
                    {
                        resultado = true;
                    }


                }

                sqlConnection.Close();
            }

            return resultado;
        }
    }
}
