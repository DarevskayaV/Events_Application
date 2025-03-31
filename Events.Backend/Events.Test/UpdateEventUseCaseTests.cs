
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly UpdateEventUseCase _updateEventUseCase;
        private readonly EventDbContext _context;
        private readonly Mock<IUserRepository> _userRepositoryMock; // Добавляем мок для IUserRepository

        public UpdateEventUseCaseTests()
        {
            // Настройка контекста базы данных для тестирования
            var options = new DbContextOptionsBuilder<EventDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new EventDbContext(options);
            _userRepositoryMock = new Mock<IUserRepository>(); // Создаем мок для IUserRepository

            // Создаем UnitOfWork с необходимыми зависимостями
            _unitOfWork = new UnitOfWork(
                _context,
                _userRepositoryMock.Object, // Передаем мок IUserRepository
                new Mock<IParticipantRepository>().Object,
                new Mock<IEventRepository>().Object,
                new Mock<IEventParticipantRepository>().Object,
                new Mock<IImageRepository>().Object
            );

            _notificationService = new Mock<INotificationService>().Object; // Создаем мок для INotificationService
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
                EventDate = DateTime.UtcNow // Убедитесь, что вы устанавливаете дату
            };

            // Сохранение события в контексте базы данных
            _context.Events.Add(existingEvent);
            await _context.SaveChangesAsync(); // Убедитесь, что событие сохранено

            var updatedEvent = new Event
            {
                Name = "Updated Event",
                Description = "Updated Description",
                Location = "Updated Location",
                Category = "Updated Category",
                EventDate = existingEvent.EventDate // Сохраните дату, если она не изменяется
            };

            // Act
            await _updateEventUseCase.ExecuteAsync(eventId, updatedEvent);

            // Assert
            var result = await _context.Events.FindAsync(eventId);
            Assert.NotNull(result); // Убедитесь, что событие найдено
            Assert.Equal("Updated Event", result.Name);
            Assert.Equal("Updated Description", result.Description);
            Assert.Equal("Updated Location", result.Location);
            Assert.Equal("Updated Category", result.Category);
            Assert.Equal(existingEvent.EventDate, result.EventDate); // Проверка даты
        }
    }
}

