using Events.Application.Use_Cases.ImageUseCases;
using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Events.Tests
{
    public class AddImageUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IImageRepository> _imageRepositoryMock;
        private readonly Mock<IEventRepository> _eventRepositoryMock; // Добавляем мок для IEventRepository
        private readonly AddImageUseCase _addImageUseCase;

        public AddImageUseCaseTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _imageRepositoryMock = new Mock<IImageRepository>();
            _eventRepositoryMock = new Mock<IEventRepository>(); // Инициализация мока для репозитория событий

            _unitOfWorkMock.Setup(u => u.Images).Returns(_imageRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.Events).Returns(_eventRepositoryMock.Object); // Настройка мока для событий

            _addImageUseCase = new AddImageUseCase(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddImage_Should_Add_Image_To_Event()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var imageFileMock = new Mock<IFormFile>();
            imageFileMock.Setup(f => f.FileName).Returns("test.jpg");
            imageFileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default)).Returns(Task.CompletedTask);

            var existingEvent = new Event { Id = eventId }; // Создаем существующее событие
            _eventRepositoryMock.Setup(x => x.GetByIdAsync(eventId)).ReturnsAsync(existingEvent); // Настройка для получения события

            // Act
            var result = await _addImageUseCase.ExecuteAsync(eventId, imageFileMock.Object); // Передаем eventId и файл

            // Assert
            _imageRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Image>()), Times.Once);
            Assert.Equal(existingEvent.ImageId, result.ImageId); // Проверяем, что ImageId обновился
        }
    }
}