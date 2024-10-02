using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("WebAPI/[controller]/[Action]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly IShipper _shipper;

        public ShippersController(IShipper shipper)
        {
            _shipper = shipper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Shipper>> GetShippers() => Ok(_shipper.GetShippers());

    }
}
