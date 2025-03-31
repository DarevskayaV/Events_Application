using Events.Core.Entity;

namespace Events.Core.Interfaces.Repositories
{
    public interface IEventParticipantRepository : IRepository<EventParticipant>
    {
        //для получения IQueryable для сложных запросов
        IQueryable<EventParticipant> Query();
    }
}

