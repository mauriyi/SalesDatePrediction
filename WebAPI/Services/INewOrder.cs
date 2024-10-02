using WebAPI.Models;

namespace WebAPI.Services
{
    public interface INewOrder
    {
        int AddNewOrder(NewOrderDto order);
    }
}
