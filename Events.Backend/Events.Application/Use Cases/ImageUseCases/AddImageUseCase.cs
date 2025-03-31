using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Events.Application.Use_Cases.ImageUseCases
{
    public class AddImageUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddImageUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Event> ExecuteAsync(Guid eventId, IFormFile imageFile)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var directoryPath = Path.Combine("wwwroot/images");

            // Проверка и создание директории, если она не существует
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, fileName);

            // Сохранение файла
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Создание сущности изображения
            var imageEntity = new Events.Core.Entity.Image
            {
                Id = Guid.NewGuid(),
                FilePath = filePath,
                EventId = eventId
            };

            // Добавление изображения в базу данных
            await _unitOfWork.Images.AddAsync(imageEntity);

            // Обновление события
            var updateableEvent = await _unitOfWork.Events.GetByIdAsync(eventId);
            updateableEvent.ImageId = imageEntity.Id;

            return updateableEvent;
        }
    }
}