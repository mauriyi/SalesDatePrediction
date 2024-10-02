using System.Data.SqlClient;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ProductRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                connection.Open();
                string query = @"
                                SELECT 
                                    productid,				-- ID del producto
                                    productname				-- Nombre del producto
                                FROM Production.Products;	-- Desde la tabla Products";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = reader.GetInt32(0),
                                ProductName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return products;
        }
    }
}
