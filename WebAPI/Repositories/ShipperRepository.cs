using System.Data.SqlClient;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Repositories
{
    public class ShipperRepository : IShipper
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ShipperRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IEnumerable<Shipper> GetShippers()
        {
            var shippers = new List<Shipper>();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                connection.Open();
                string query = @"
                                SELECT 
                                    shipperid,			-- ID del transportista
                                    companyname			-- Nombre de la compañía del transportista
                                FROM Sales.Shippers;	-- Desde la tabla Shippers";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shippers.Add(new Shipper
                            {
                                shipperid = reader.GetInt32(0),
                                Companyname = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return shippers;
        }
    }
}
