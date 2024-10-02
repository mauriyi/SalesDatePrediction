using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests
{
    public class ProductsControllerTests
    {
        private readonly ProductsController _controller;
        private readonly Mock<IProduct> _mockService;

        public ProductsControllerTests()
        {
            // Inicializa el mock del servicio IProduct
            _mockService = new Mock<IProduct>();
            // Crea una instancia del controlador con el servicio simulado
            _controller = new ProductsController(_mockService.Object);
        }

        [Fact]
        public void GetProducts_ReturnsOkResult_WithProductList()
        {
            // Arrange: Configura el mock para devolver una lista de productos
            var productList = new List<Product>
            {
                new Product { Productid = 1, Productname = "Product A" },
                new Product { Productid = 2, Productname = "Product B" }
            };
            _mockService.Setup(s => s.GetProducts()).Returns(productList);

            // Act: Llama al método GetProducts del controlador
            var result = _controller.GetProducts();

            // Assert: Verifica que el resultado sea un OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifica que el valor del resultado sea una lista de productos
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            // Verifica que la lista devuelta coincida con la lista esperada
            Assert.Equal(productList, returnValue);
        }

        [Fact]
        public void GetProducts_ReturnsOkResult_WithEmptyList()
        {
            // Arrange: Configura el mock para devolver una lista vacía
            var productList = new List<Product>();
            _mockService.Setup(s => s.GetProducts()).Returns(productList);

            // Act: Llama al método GetProducts del controlador
            var result = _controller.GetProducts();

            // Assert: Verifica que el resultado sea un OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Verifica que el valor devuelto sea una lista de productos
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            // Verifica que la lista esté vacía
            Assert.Empty(returnValue);
        }
    }
}
