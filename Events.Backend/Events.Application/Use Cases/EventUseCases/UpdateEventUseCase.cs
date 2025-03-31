using Events.Application.Exceptions;
using Events.Application.Interfaces;
using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;


namespace Events.Application.Use_Cases.EventUseCases
{
    public class UpdateEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public UpdateEventUseCase(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task ExecuteAsync(Guid eventId, Event eventEntity)
        {
            var existingEvent = await _unitOfWork.Events.GetByIdAsync(eventId);
            if (existingEvent == null)
            {
                throw new NotFoundException("Event not found");
            }

            // Сравниваем изменения
            bool isDateChanged = existingEvent.EventDate != eventEntity.EventDate;
            bool isLocationChanged = existingEvent.Location != eventEntity.Location;

            // Обновляем свойства существующего события
            existingEvent.Name = eventEntity.Name;
            existingEvent.Description = eventEntity.Description;
            existingEvent.Location = eventEntity.Location;
            existingEvent.EventDate = eventEntity.EventDate;
            existingEvent.Category = eventEntity.Category;

            // Сохраняем изменения
            await _unitOfWork.Events.UpdateAsync(existingEvent);
            await _unitOfWork.SaveChangesAsync();

            // Уведомляем участников, если изменения произошли
            if (isDateChanged || isLocationChanged)
            {
                await _notificationService.NotifyParticipantsAsync(eventId, isDateChanged, isLocationChanged);
            }
        }
    }
}

