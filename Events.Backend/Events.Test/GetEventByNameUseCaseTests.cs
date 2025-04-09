using Events.Application.Use_Cases.EventUseCases;
using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using Moq;
using Xunit;
using System.Linq;
using MockQueryable.Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MockQueryable;

namespace Events.Tests
{
    public class GetEventByNameUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetEventByNameUseCase _getEventByNameUseCase;

        public GetEventByNameUseCaseTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _getEventByNameUseCase = new GetEventByNameUseCase(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WhenEventExists_ReturnsEvent()
        {
            // Arrange
            var eventName = "Music Festival";
            var eventEntity = new Event { Id = Guid.NewGuid(), Name = eventName, EventDate = DateTime.Now };

            var events = new List<Event> { eventEntity }.BuildMock(); // Используем BuildMock()

            _unitOfWorkMock.Setup(u => u.Events.Query())
                .Returns(events);

            // Act
            var result = await _getEventByNameUseCase.ExecuteAsync(eventName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventName, result.Name);
        }

        [Fact]
        public async Task ExecuteAsync_WhenEventDoesNotExist_ReturnsNull()
        {
            // Arrange
            var eventName = "Nonexistent Event";

            var events = new List<Event>().BuildMock(); // Используем BuildMock()

            _unitOfWorkMock.Setup(u => u.Events.Query())
                .Returns(events);

            // Act
            var result = await _getEventByNameUseCase.ExecuteAsync(eventName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ExecuteAsync_WhenMultipleEventsExist_ReturnsFirstMatchingEvent()
        {
            // Arrange
            var eventName = "Concert";
            var eventEntity1 = new Event { Id = Guid.NewGuid(), Name = eventName, EventDate = DateTime.Now };
            var eventEntity2 = new Event { Id = Guid.NewGuid(), Name = "Another Concert", EventDate = DateTime.Now.AddDays(1) };

            var events = new List<Event> { eventEntity1, eventEntity2 }.BuildMock(); // Используем BuildMock()

            _unitOfWorkMock.Setup(u => u.Events.Query())
                .Returns(events);

            // Act
            var result = await _getEventByNameUseCase.ExecuteAsync(eventName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventName, result.Name);
        }
    }
}