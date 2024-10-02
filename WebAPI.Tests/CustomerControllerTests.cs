using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests
{
    public class CustomerControllerTests
    {
        private readonly CustomerController _controller;
        private readonly Mock<ICustomer> _mockService;

        public CustomerControllerTests()
        {
            _mockService = new Mock<ICustomer>(); // Inicializa el mock del servicio
            _controller = new CustomerController(_mockService.Object); // Crea una instancia del controlador
        }

        [Fact]
        public void SalesDatePrediction_ReturnsOkResult_WithCustomerOrders()
        {
            // Arrange
            var customerOrders = new List<Customer>
            {
                new Customer { CustomerName = "John Doe", LastOrderDate = DateTime.Now.AddDays(-30), NextPredictedOrder = DateTime.Now.AddDays(30) },
                new Customer { CustomerName = "Jane Doe", LastOrderDate = DateTime.Now.AddDays(-60), NextPredictedOrder = DateTime.Now.AddDays(60) }
            };

            _mockService.Setup(s => s.SalesDatePrediction()).Returns(customerOrders); // Configura el mock

            // Act
            var result = _controller.SalesDatePrediction();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Cambiar a result.Result
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Customer>>(okResult.Value); // Verifica el tipo del valor devuelto

            // Compara las listas
            Assert.Equal(customerOrders, returnValue);
        }

        [Fact]
        public void SalesDatePrediction_ReturnsOkResult_WithEmptyList()
        {
            // Arrange
            var customerOrders = new List<Customer>();

            _mockService.Setup(s => s.SalesDatePrediction()).Returns(customerOrders); // Configura el mock para devolver una lista vacía

            // Act
            var result = _controller.SalesDatePrediction();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Cambiar a result.Result
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Customer>>(okResult.Value); // Verifica el tipo del valor devuelto

            // Compara que la lista devuelta esté vacía
            Assert.Empty(returnValue);
        }
    }
}
