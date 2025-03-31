
using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Events.Application.Use_Cases.EventUseCases
{
    public class GetEventByNameUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEventByNameUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Event> ExecuteAsync(string eventName)
        {
            return await _unitOfWork.Events.Query().Where(e => e.Name == eventName).FirstOrDefaultAsync();
        }
    }
}

