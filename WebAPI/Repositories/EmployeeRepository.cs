using System.Data.SqlClient;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public EmployeeRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            var employees = new List<Employee>();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                connection.Open();
                string query = @"
                                SELECT 
                                    empid,											-- ID del empleado
                                    CONCAT(firstname, ' ', lastname) AS FullName	-- Concatenación del nombre y apellido
                                FROM HR.Employees;									-- Desde la tabla Employees";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                Empid = reader.GetInt32(0),
                                FullName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return employees;
        }
    }
}
