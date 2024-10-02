using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IEmployee
    {
        IEnumerable<Employee> GetEmployees();
    }
}
