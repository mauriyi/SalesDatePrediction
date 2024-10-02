using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests
{
    public class OrdersControllerTests
    {
        private readonly OrdersController _controller;
        private readonly Mock<IOrder> _mockOrderService;
        private readonly Mock<INewOrder> _mockNewOrderService;
        private readonly Mock<INewOrderDetail> _mockNewOrderDetailService;

        public OrdersControllerTests()
        {
            // Inicializa los mocks
            _mockOrderService = new Mock<IOrder>();
            _mockNewOrderService = new Mock<INewOrder>();
            _mockNewOrderDetailService = new Mock<INewOrderDetail>();

            // Crea una instancia del controlador con los servicios simulados
            _controller = new OrdersController(_mockOrderService.Object, _mockNewOrderService.Object, _mockNewOrderDetailService.Object);
        }

        [Fact]
        public void GetClientOrders_ReturnsOkResult_WithOrderList()
        {
            // Arrange: Configura el mock para devolver una lista de órdenes
            var orderList = new List<Order>
            {
                new Order { OrderId = 1, ShipName = "Ship A", ShipCity = "City A" },
                new Order { OrderId = 2, ShipName = "Ship B", ShipCity = "City B" }
            };
            _mockOrderService.Setup(s => s.GetClientOrders(1)).Returns(orderList);

            // Act: Llama al método GetClientOrders del controlador
            var result = _controller.GetClientOrders(1);

            // Assert: Verifica que el resultado sea un OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifica que el valor devuelto sea una lista de órdenes
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Order>>(okResult.Value);

            // Verifica que la lista devuelta coincida con la lista esperada
            Assert.Equal(orderList, returnValue);
        }

        [Fact]
        public void AddNewOrder_ReturnsCreatedAtActionResult_WithOrderId()
        {
            // Arrange: Configura el mock para crear una nueva orden y devolver un orderId
            var newOrder = new NewOrderDto
            {
                CustId = 1,
                EmpId = 2,
                ShipperId = 3,
                ShipName = "Ship A",
                ShipAddress = "Address A",
                ShipCity = "City A",
                RequiredDate = System.DateTime.Now.AddDays(5),
                OrderDetails = new NewOrderDetailDto { ProductId = 1, UnitPrice = 10, Qty = 5, Discount = 0.1m }
            };

            _mockNewOrderService.Setup(s => s.AddNewOrder(newOrder)).Returns(100); // Devuelve un orderId = 100
            _mockNewOrderDetailService.Setup(s => s.AddOrderDetail(100, newOrder.OrderDetails));

            // Act: Llama al método AddNewOrder del controlador
            var result = _controller.AddNewOrder(newOrder);

            // Assert: Verifica que el resultado sea un CreatedAtActionResult
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            // Verifica que el valor devuelto sea el orderId generado
            Assert.Equal(100, createdResult.Value);
        }
    }
}
