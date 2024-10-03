using System.Data;
using System.Data.SqlClient;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Repositories
{
    public class CustomerRepository : ICustomer
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public CustomerRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IEnumerable<Customer> SalesDatePrediction(string searchTerm = "")
        {
            var customerOrders = new List<Customer>();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                // Modificar la consulta para filtrar por nombre de cliente
                string query = @"
                    SELECT 
                        C.custid AS CustomerId,
                        C.companyname AS CustomerName,
                        MAX(O.orderdate) AS LastOrderDate,
                        DATEADD(
                            DAY, 
                            ISNULL(AVG(DATEDIFF(DAY, O1.orderdate, O.orderdate)), 0),
                            MAX(O.orderdate)
                        ) AS NextPredictedOrder
                    FROM Sales.Orders O
                    JOIN Sales.Customers C ON O.custid = C.custid
                    LEFT JOIN Sales.Orders O1 ON O.custid = O1.custid AND O1.orderdate < O.orderdate
                    WHERE C.companyname LIKE @SearchTerm
                    GROUP BY C.custid, C.companyname  
                    ORDER BY NextPredictedOrder;";

                using (var command = new SqlCommand(query, connection))
                {
                    // Añadir el parámetro de búsqueda
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customerOrders.Add(new Customer
                            {
                                CustId = reader.GetInt32(0),
                                CustomerName = reader.GetString(1),
                                LastOrderDate = reader.GetDateTime(2),
                                NextPredictedOrder = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }

            return customerOrders;
        }
    }
}
