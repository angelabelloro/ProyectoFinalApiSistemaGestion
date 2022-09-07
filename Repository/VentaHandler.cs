using MiPrimeraApi.Controllers.DTOS;
using MiPrimeraApi.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public class VentaHandler
    {
        public const string ConnectionString = "Server=DESKTOP-4BVLUFB;Initial Catalog=SistemaGestion;Trusted_Connection=true";

        public static bool CargarVenta(List<ProductoVendido> listaDeProductos)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.Connection.Open();

                    string lista = Convert.ToString(listaDeProductos);
                    sqlCommand.CommandText = "INSERT INTO [SistemaGestion].[dbo].[Venta] (Comentarios)" +
                        " VALUES ( @comentariosParameter );";
                    SqlParameter comentariosParameter = new SqlParameter("comentariosParameter", SqlDbType.VarChar) { Value = lista };
                    int recordsAffected = sqlCommand.ExecuteNonQuery();

                    foreach (var productoVendido in listaDeProductos)
                    {
                        ProductoVendidoHandler.CrearProductoVendido(productoVendido);
                    }

                    sqlCommand.Connection.Close();
                    if (recordsAffected != 1)

                    {
                        return false;
                    }

                    else

                    {
                        return true;
                    }
                }

                sqlConnection.Close();
            }
        }
        public static int GetLastIdVenta()
        {
            int id = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryGetLastId = "SELECT TOP (1) [Id]" +
                    "FROM[SistemaGestion].[dbo].[Venta]" +
                    "ORDER BY Id DESC";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryGetLastId, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                id = Convert.ToInt32(dataReader["Id"]);
                            }
                        }
                    }
                }

                sqlConnection.Close();
            }
            return id;
        }

        public static List<GetAllVentas> GetVentas()
        {
            List<GetAllVentas> ventas = new List<GetAllVentas>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "SELECT " +
                        "Venta.Id as idVenta, " +
                        "Venta.Comentarios as ventaComentario, " +
                        "ProductoVendido.Id as productoVendidoId, " +
                        "ProductoVendido.Stock as productoVendidoStock, " +
                        "Producto.Id as productoId, " +
                        "Producto.Descripciones as productoDescripciones, " +
                        "Producto.Costo as productoCosto, " +
                        "Producto.PrecioVenta as productoPrecioVenta, " +
                        "Producto.Stock as productoStock " +
                        "FROM Venta " +
                        "INNER JOIN ProductoVendido ON Venta.Id = ProductoVendido.IdVenta " +
                        "INNER JOIN Producto ON Producto.Id = ProductoVendido.IdProducto; ";

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

                    sqlDataAdapter.SelectCommand = sqlCommand;

                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);

                    sqlCommand.Connection.Close();

                    foreach (DataRow row in table.Rows)
                    {
                        GetAllVentas venta = new GetAllVentas();

                        venta.IdVenta = Convert.ToInt32(row["idVenta"]);
                        venta.VentaComentario = row["ventaComentario"].ToString();
                        venta.ProductoVendidoId = Convert.ToInt32(row["productoVendidoId"]);
                        venta.ProductoVendidoStock = Convert.ToInt32(row["productoVendidoStock"]);
                        venta.ProductoId = Convert.ToInt32(row["productoId"]);
                        venta.ProductoDescripciones = row["productoDescripciones"].ToString();
                        venta.ProductoCosto = Convert.ToInt32(row["productoCosto"]);
                        venta.PrecioVenta = Convert.ToInt32(row["productoPrecioVenta"]);
                        venta.ProductoStock = Convert.ToInt32(row["productoStock"]);

                        ventas.Add(venta);

                    }
                }
            }
            return ventas;
        }

        public static bool CrearVenta(Venta venta)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;

                    sqlCommand.Connection.Open();

                    sqlCommand.CommandText = "INSERT INTO Venta (Comentarios) VALUES ( @comentarios );";
                    SqlParameter comentariosParameter = new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios };
                    int recordsAffected = sqlCommand.ExecuteNonQuery();

                    sqlCommand.Connection.Close();

                    if (recordsAffected != 1)
                    {
                        return resultado = false;
                    }

                    else
                    {
                        return resultado = true;
                    }
                }

                sqlConnection.Close();
            }
        }
        public static bool EliminarVenta(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Venta WHERE Id = @id";

                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;

        }

        public static bool ModificarVenta(Venta venta)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Venta] " +
                    "SET Comentarios = @comentariosParameter" + " " +
                    "WHERE Id = @idParameter";

                SqlParameter comentariosParameter = new SqlParameter("comentariosParameter", SqlDbType.VarChar) { Value = venta.Comentarios };
                SqlParameter idParameter = new SqlParameter("idParameter", SqlDbType.VarChar) { Value = venta.Id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentariosParameter);
                    sqlCommand.Parameters.Add(idParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery(); // Se ejecuta la sentencia sql

                    if (numberOfRows > 0)
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
