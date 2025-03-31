using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Events.Data.Repositories
{
    public class ParticipantRepository : RepositoryBase<Participant>, IParticipantRepository
    {
        private readonly EventDbContext _context;

        public ParticipantRepository(EventDbContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<IEnumerable<Participant>> GetAllAsync()
        {
            return await _context.Participants
                .Include(p => p.EventParticipants)
                .AsNoTracking()
                .ToListAsync();
        }

        public new async Task<Participant> GetByIdAsync(Guid id)
        {
            return await _context.Participants
                .Include(p => p.EventParticipants)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<Participant> Query()
        {
            return _context.Participants.AsQueryable();
        }
    }
}

