using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IShipper
    {
        IEnumerable<Shipper> GetShippers();
    }
}
