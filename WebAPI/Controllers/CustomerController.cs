using Microsoft.AspNetCore.Mvc;
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
        public IActionResult SalesDatePrediction(string search = "")
        {
            var customers = _customerOrder.SalesDatePrediction(search); 
            return Ok(customers);
        }

    }
}
