using Events.Core.Entity;

namespace Events.Core.Interfaces.Repositories
{
    public interface IEventParticipantRepository : IRepository<EventParticipant>
    {
        //��� ��������� IQueryable ��� ������� ��������
        IQueryable<EventParticipant> Query();
    }
}

