
using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Events.Application.Use_Cases.EventUseCases
{
    public class AddEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Event> ExecuteAsync(Event newEvent)
        {
            await _unitOfWork.Events.AddAsync(newEvent);
            await _unitOfWork.SaveChangesAsync();
            return newEvent;
        }
    }
}

