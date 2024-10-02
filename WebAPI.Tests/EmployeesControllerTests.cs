using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests
{
    public class EmployeesControllerTests
    {
        private readonly EmployeesController _controller;
        private readonly Mock<IEmployee> _mockService;

        public EmployeesControllerTests()
        {
            _mockService = new Mock<IEmployee>(); // Inicializa el mock del servicio
            _controller = new EmployeesController(_mockService.Object); // Crea una instancia del controlador
        }

        [Fact]
        public void GetEmployees_ReturnsOkResult_WithEmployeeList()
        {
            // Arrange
            var employeeList = new List<Employee>
            {
                new Employee { Empid = 1, FullName = "Alice Roob" },
                new Employee { Empid = 2, FullName = "Bob Martin" }
            };

            _mockService.Setup(s => s.GetEmployees()).Returns(employeeList); // Configura el mock

            // Act
            var result = _controller.GetEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verifica que el resultado sea un OkObjectResult
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value); // Verifica el tipo del valor devuelto

            // Compara las listas
            Assert.Equal(employeeList, returnValue);
        }

        [Fact]
        public void GetEmployees_ReturnsOkResult_WithEmptyList()
        {
            // Arrange
            var employeeList = new List<Employee>();

            _mockService.Setup(s => s.GetEmployees()).Returns(employeeList); // Configura el mock para devolver una lista vacía

            // Act
            var result = _controller.GetEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Verifica que el resultado sea un OkObjectResult
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value); // Verifica el tipo del valor devuelto

            // Compara que la lista devuelta esté vacía
            Assert.Empty(returnValue);
        }
    }
}
