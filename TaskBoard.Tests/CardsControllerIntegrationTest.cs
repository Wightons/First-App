using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskBoard.API.Controllers;
using TaskBoard.API.Dtos;
using Xunit;
using Moq;
using TaskBoard.API.Services;

namespace TaskBoard.Tests
{
    public class CardsControllerIntegrationTest : IDisposable
    {
        private readonly HttpClient _client;
        private readonly Mock<CardsService> _cardsServiceMock;

        public CardsControllerIntegrationTest()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:yourPort/api/cards") };
            _cardsServiceMock = new Mock<CardsService>();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        [Fact]
        public async Task Add_ValidCardDto_ReturnsOk()
        {
            // Arrange
            var cardDto = new CardDto { Name = "Test Card" };
            _cardsServiceMock.Setup(service => service.Add(cardDto)).Returns(Task.CompletedTask);

            var controller = new CardsController(_cardsServiceMock.Object);

            // Act
            var response = await controller.Add(cardDto);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Add_InvalidCardDto_ReturnsBadRequest()
        {
            // Arrange (no setup needed for invalid data)
            var controller = new CardsController(_cardsServiceMock.Object);

            // Act
            var response = await controller.Add(null); // Or any invalid CardDto

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Add_ServiceThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var cardDto = new CardDto { Name = "Test Card" };
            _cardsServiceMock.Setup(service => service.Add(cardDto)).Throws(new Exception("Simulated service error"));

            var controller = new CardsController(_cardsServiceMock.Object);

            // Act
            var response = await controller.Add(cardDto);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.ShouldContain("Simulated service error"); // Verify error message in response body (optional)
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsOk()
        {
            // Arrange
            var id = 1;
            _cardsServiceMock.Setup(service => service.Delete(id)).Returns(Task.CompletedTask);

            var controller = new CardsController(_cardsServiceMock.Object);

            // Act
            var response = await controller.Delete(id);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_NonexistentId_ReturnsNotFound()
        {
            // Arrange
            var id = 1;
            _cardsServiceMock.Setup(service => service.Delete(id)).Throws(new Exception("Card not found")); // Simulate card not found exception

            var controller = new CardsController(_cardsServiceMock.Object);

            // Act
            var response = await controller.Delete(id);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound); // Adjusted to NotFound for missing card
        }

        [Fact]
        public async Task Delete_ServiceThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var id = 1;
            _cardsServiceMock.Setup(service => service.Delete(id)).Throws(new Exception("Simulated service error"));

            var controller = new CardsController(_cardsServiceMock.Object);

            // Act
            var response = await controller.Delete(id);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.ShouldContain("Simulated service error"); // Verify error message in response body (optional)
        }

        [Fact]
        public async Task Update_ValidCardDto_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var cardDto = new CardDto { Name = "Updated Card" };
            _cardsServiceMock.Setup(service => service.Update(id, cardDto)).Returns(Task.CompletedTask);

            var controller = new CardsController(_cardsServiceMock.Object);

            // Act
            var response = await controller.Update(id, cardDto);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
