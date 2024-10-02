using System.Data.SqlClient;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Repositories
{
    public class NewOrderDetailRepository : INewOrderDetail
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public NewOrderDetailRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public void AddOrderDetail(int orderId, NewOrderDetailDto orderDetail)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                string insertOrderDetailQuery = @"
                INSERT INTO Sales.OrderDetails (orderid, productid, unitprice, qty, discount)
                VALUES (@OrderId, @ProductId, @UnitPrice, @Qty, @Discount);";

                using (var command = new SqlCommand(insertOrderDetailQuery, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@ProductId", orderDetail.ProductId);
                    command.Parameters.AddWithValue("@UnitPrice", orderDetail.UnitPrice);
                    command.Parameters.AddWithValue("@Qty", orderDetail.Qty);
                    command.Parameters.AddWithValue("@Discount", orderDetail.Discount);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
