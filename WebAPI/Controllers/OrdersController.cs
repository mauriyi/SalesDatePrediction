using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("WebAPI/[controller]/[Action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _order;
        private readonly INewOrder _newOrder;
        private readonly INewOrderDetail _newOrderDetail;

        public OrdersController(IOrder order, INewOrder newOrder, INewOrderDetail newOrderDetail)
        {
            _order = order;
            _newOrder = newOrder;
            _newOrderDetail = newOrderDetail;
        }

        [HttpGet("{customerId}")]
        public ActionResult<IEnumerable<Order>> GetClientOrders(int customerId) => Ok(_order.GetClientOrders(customerId));

        [HttpPost]
        public ActionResult<int> AddNewOrder(NewOrderDto order)
        {
            // Crear la nueva orden
            int orderId = _newOrder.AddNewOrder(order);

            // Agregar el detalle de la orden
            _newOrderDetail.AddOrderDetail(orderId, order.OrderDetails);

            return CreatedAtAction(nameof(AddNewOrder), new { id = orderId }, orderId);
        }
    }
}
