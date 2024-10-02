using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("WebAPI/[controller]/[Action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _order;

        public OrdersController(IOrder order)
        {
            _order = order;
        }

        [HttpGet("{customerId}")]
        public ActionResult<IEnumerable<Order>> GetClientOrders(int customerId) => Ok(_order.GetClientOrders(customerId));

    }
}
