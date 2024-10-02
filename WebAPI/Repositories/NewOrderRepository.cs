using System.Data.SqlClient;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Repositories
{
    public class NewOrderRepository : INewOrder
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public NewOrderRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public int AddNewOrder(NewOrderDto order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "El objeto de orden no puede ser nulo.");
            }

            int orderId;

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertOrderQuery = @"
                    INSERT INTO Sales.Orders (empid, custid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
                    VALUES (@EmpId, @CustId, @ShipperId, @ShipName, @ShipAddress, @ShipCity, @Orderdate, @RequiredDate, @ShippedDate, @Freight, @ShipCountry);
                    SELECT SCOPE_IDENTITY();"; // ID de la orden recién creada

                        using (var command = new SqlCommand(insertOrderQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@EmpId", order.EmpId);
                            command.Parameters.AddWithValue("@CustId", order.CustId);
                            command.Parameters.AddWithValue("@ShipperId", order.ShipperId);
                            command.Parameters.AddWithValue("@ShipName", order.ShipName);
                            command.Parameters.AddWithValue("@ShipAddress", order.ShipAddress);
                            command.Parameters.AddWithValue("@ShipCity", order.ShipCity);
                            command.Parameters.AddWithValue("@Orderdate", order.Orderdate);
                            command.Parameters.AddWithValue("@RequiredDate", order.RequiredDate);
                            command.Parameters.AddWithValue("@ShippedDate", order.ShippedDate ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Freight", order.Freight);
                            command.Parameters.AddWithValue("@ShipCountry", order.ShipCountry);

                            // Obtener el ID de la nueva orden
                            orderId = Convert.ToInt32(command.ExecuteScalar());
                        }

                        transaction.Commit(); // Confirmar la transacción si todo salió bien
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Revertir los cambios si ocurre un error
                        throw new InvalidOperationException("No se pudo agregar la nueva orden.", ex);
                    }
                }
            }

            return orderId; // Retornar el ID de la nueva orden
        }
    }
}
