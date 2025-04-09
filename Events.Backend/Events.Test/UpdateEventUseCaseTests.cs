
using Events.Application.Use_Cases.EventUseCases;
using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using Events.Data.Repositories;
using Events.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Events.Application.Interfaces;
using Events.Application.Exceptions;

namespace Events.Tests
{
    public class UpdateEventUseCaseTests
    {
        private IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private
            UpdateEventUseCase _updateEventUseCase;
        private readonly EventDbContext _context;
        private readonly Mock<IUserRepository> _userRepositoryMock; // ��������� ��� ��� IUserRepository

        public UpdateEventUseCaseTests()
        {
            // ��������� ��������� ���� ������ ��� ������������
            var options = new DbContextOptionsBuilder<EventDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new EventDbContext(options);
            _userRepositoryMock = new Mock<IUserRepository>(); // ������� ��� ��� IUserRepository

            // ������� UnitOfWork � ������������ �������������
            _unitOfWork = new UnitOfWork(
                _context,
                _userRepositoryMock.Object, // �������� ��� IUserRepository
                new Mock<IParticipantRepository>().Object,
                new Mock<IEventRepository>().Object,
                new Mock<IEventParticipantRepository>().Object,
                new Mock<IImageRepository>().Object
            );

            _notificationService = new Mock<INotificationService>().Object; // ������� ��� ��� INotificationService
            _updateEventUseCase = new UpdateEventUseCase(_unitOfWork, _notificationService);
        }

        [Fact]
        public async Task UpdateEvent_Should_Update_Existing_Event()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var existingEvent = new Event
            {
                Id = eventId,
                Name = "Original Event",
                Description = "Original Description",
                Location = "Original Location",
                Category = "Original Category",
                EventDate = DateTime.UtcNow
            };

            // ������� ��� IEventRepository � ����������� ��� ���������
            var eventRepositoryMock = new Mock<IEventRepository>();
            eventRepositoryMock.Setup(r => r.GetByIdAsync(eventId))
                .ReturnsAsync(existingEvent);
            eventRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Event>()))
                .Returns(Task.CompletedTask);

            // ����������� UnitOfWork � ����������� ����� IEventRepository
            _unitOfWork = new UnitOfWork(
                _context,
                _userRepositoryMock.Object,
                new Mock<IParticipantRepository>().Object,
                eventRepositoryMock.Object, // ���������� ����������� ���
                new Mock<IEventParticipantRepository>().Object,
                new Mock<IImageRepository>().Object
            );

            // ����������� use case � ����� UnitOfWork
            _updateEventUseCase = new UpdateEventUseCase(_unitOfWork, _notificationService);

            var updatedEvent = new Event
            {
                Name = "Updated Event",
                Description = "Updated Description",
                Location = "Updated Location",
                Category = "Updated Category",
                EventDate = existingEvent.EventDate
            };

            // Act
            await _updateEventUseCase.ExecuteAsync(eventId, updatedEvent);

            // Assert
            eventRepositoryMock.Verify(r => r.GetByIdAsync(eventId), Times.Once);
            eventRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Event>(e =>
                e.Name == "Updated Event" &&
                e.Description == "Updated Description" &&
                e.Location == "Updated Location" &&
                e.Category == "Updated Category")),
            Times.Once);
        }
    }
}

