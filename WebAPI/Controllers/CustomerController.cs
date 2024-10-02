using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("WebAPI/[controller]/[Action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _customerOrder;

        public CustomerController(ICustomer customerOrder)
        {
            _customerOrder = customerOrder;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> SalesDatePrediction() => Ok(_customerOrder.SalesDatePrediction());

    }
}
