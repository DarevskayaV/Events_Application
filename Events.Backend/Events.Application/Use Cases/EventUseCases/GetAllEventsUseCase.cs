using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;


namespace Events.Application.Use_Cases.EventUseCases
{
    public class GetAllEventsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllEventsUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Event>> ExecuteAsync()
        {
            return await _unitOfWork.Events.GetAllAsync();
        }
    }
}

