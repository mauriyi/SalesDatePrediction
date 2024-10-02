using System.Data.SqlClient;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public OrderRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IEnumerable<Order> GetClientOrders(int customerId)
        {
            var orders = new List<Order>();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                connection.Open();
                string query = @"
                                SELECT 
                                    O.orderid,					-- ID de la orden
                                    O.requireddate,				-- Fecha en que se requiere el envío
                                    O.shippeddate,				-- Fecha en que se envió la orden
                                    O.shipname,					-- Nombre del destinatario
                                    O.shipaddress,				-- Dirección de envío
                                    O.shipcity					-- Ciudad de envío
                                FROM Sales.Orders O
                                WHERE O.custid = @CustomerId;	-- Filtra por el ID del cliente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                OrderId = reader.GetInt32(0),
                                RequiredDate = reader.GetDateTime(1),
                                ShippedDate = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                                ShipName = reader.GetString(3),
                                ShipAddress = reader.GetString(4),
                                ShipCity = reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return orders;
        }
    }
}
