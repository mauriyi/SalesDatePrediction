using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ICustomer
    {
        IEnumerable<Customer> SalesDatePrediction(string searchTerm);
    }
}
