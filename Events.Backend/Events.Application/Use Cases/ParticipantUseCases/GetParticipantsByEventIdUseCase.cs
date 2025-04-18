using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;

namespace Events.Application.Use_Cases.ParticipantUseCases
{
    public class GetParticipantsByEventIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetParticipantsByEventIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Participant>> ExecuteAsync(Guid eventId)
        {
            List<EventParticipant> eventEntity = _unitOfWork.EventParticipant.Query()
                .Where(e => e.EventId == eventId).ToList();

            var participantList = new List<Participant>();
            for (int i = 0; i < eventEntity.Count; i++)
            {
                participantList.Add(_unitOfWork.Participants.GetByIdAsync(eventEntity[i].ParticipantId).Result);
            }
            return participantList;
        }
    }
}

