using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoard.API.Database;
using TaskBoard.API.Database.Entities;
using TaskBoard.API.Dtos;
using TaskBoard.API.Services;
using Xunit;

namespace TaskBoard.Tests
{
    public class CardServiceTests
    {
        private Mock<TaskBoardContext> _contextMock;
        private Mock<IMapper> _mapperMock;
        private CardsService _cardService;

        public CardServiceTests()
        {
            _contextMock = new Mock<TaskBoardContext>();
            _mapperMock = new Mock<IMapper>();
            _cardService = new CardsService(_contextMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Add_WithValidDto_AddsCard()
        {
            // Arrange
            var dto = new CardDto { Priority = (int)Priority.High };

            // Act
            await _cardService.Add(dto);

            // Assert
            _contextMock.Verify(c => c.Cards.AddAsync(It.IsAny<Card>(), It.IsAny<CancellationToken>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Delete_WithValidId_DeletesCard()
        {
            // Arrange
            var id = 1;

            // Act
            await _cardService.Delete(id);

            // Assert
            _contextMock.Verify(c => c.Cards.Remove(It.IsAny<Card>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsCardDto()
        {
            // Arrange
            var id = 1;
            var card = new Card { Id = id };
            _contextMock.Setup(c => c.Cards.FindAsync(id)).ReturnsAsync(card);

            // Act
            var result = await _cardService.Get(id);

            // Assert
            Assert.NotNull(result);
            _mapperMock.Verify(m => m.Map<CardDto>(It.IsAny<Card>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_ReturnsAllCards()
        {
            // Arrange
            var cards = new List<Card> { new Card(), new Card() };
            _contextMock.Setup(c => c.Cards.ToListAsync()).ReturnsAsync(cards);

            // Act
            var result = await _cardService.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            _mapperMock.Verify(m => m.Map<IEnumerable<CardDto>>(It.IsAny<IEnumerable<Card>>()), Times.Once);
        }

        [Fact]
        public async Task Update_WithValidIdAndDto_UpdatesCard()
        {
            // Arrange
            var id = 1;
            var dto = new CardDto { Id = id };
            var card = new Card { Id = id };
            _contextMock.Setup(c => c.Cards.FindAsync(id)).ReturnsAsync(card);

            // Act
            await _cardService.Update(id, dto);

            // Assert
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }

}
