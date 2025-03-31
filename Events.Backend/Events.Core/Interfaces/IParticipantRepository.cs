using Events.Core.Entity;

namespace Events.Core.Interfaces.Repositories
{
    public interface IParticipantRepository : IRepository<Participant>
    {
        IQueryable<Participant> Query();
    }
}

