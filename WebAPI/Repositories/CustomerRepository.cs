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

        public IEnumerable<Customer> SalesDatePrediction()
        {
            var customerOrders = new List<Customer>();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                connection.Open();
                string query = @"
                    SELECT 
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
                    GROUP BY C.companyname
                    ORDER BY NextPredictedOrder;";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customerOrders.Add(new Customer
                            {
                                CustomerName = reader.GetString(0),
                                LastOrderDate = reader.GetDateTime(1),
                                NextPredictedOrder = reader.GetDateTime(2)
                            });
                        }
                    }
                }
            }

            return customerOrders;
        }
    }
}
