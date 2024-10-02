using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IProduct
    {
        IEnumerable<Product> GetProducts();
    }
}
