using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests
{
    public class ShippersControllerTests
    {
        private readonly ShippersController _controller;
        private readonly Mock<IShipper> _mockService;

        public ShippersControllerTests()
        {
            // Inicializa el mock del servicio IShipper
            _mockService = new Mock<IShipper>();
            // Crea una instancia del controlador con el servicio simulado
            _controller = new ShippersController(_mockService.Object);
        }

        [Fact]
        public void GetShippers_ReturnsOkResult_WithShipperList()
        {
            // Arrange: Configura el mock para devolver una lista de shippers
            var shipperList = new List<Shipper>
            {
                new Shipper { Shipperid = 1, Companyname = "Shipper A" },
                new Shipper { Shipperid = 2, Companyname = "Shipper B" }
            };
            _mockService.Setup(s => s.GetShippers()).Returns(shipperList);

            // Act: Llama al método GetShippers del controlador
            var result = _controller.GetShippers();

            // Assert: Verifica que el resultado sea un OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifica que el valor del resultado sea una lista de shippers
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Shipper>>(okResult.Value);

            // Verifica que la lista devuelta coincida con la lista esperada
            Assert.Equal(shipperList, returnValue);
        }

        [Fact]
        public void GetShippers_ReturnsOkResult_WithEmptyList()
        {
            // Arrange: Configura el mock para devolver una lista vacía
            var shipperList = new List<Shipper>();
            _mockService.Setup(s => s.GetShippers()).Returns(shipperList);

            // Act: Llama al método GetShippers del controlador
            var result = _controller.GetShippers();

            // Assert: Verifica que el resultado sea un OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifica que el valor devuelto sea una lista de shippers
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Shipper>>(okResult.Value);

            // Verifica que la lista esté vacía
            Assert.Empty(returnValue);
        }
    }
}
