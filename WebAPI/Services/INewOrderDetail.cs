using WebAPI.Models;

namespace WebAPI.Services
{
    public interface INewOrderDetail
    {
        void AddOrderDetail(int orderId, NewOrderDetailDto orderDetail);
    }
}
