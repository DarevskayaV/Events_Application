using Events.Core.Entity;

namespace Events.Core.Interfaces.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<List<Event>> GetEventByPage(int page, int pageSize);
        IQueryable<Event> Query();
    }
}

