using back_messenger_signalr.Entities;
using back_messenger_signalr.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace back_messenger_signalr.Repositories.Classes
{
    public class ParticipantRepository : GenericRepository<ParticipantEntity, int>, IParticipantRepository
    {
        private readonly AppEFContext _dbContext;
        public ParticipantRepository(AppEFContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<ParticipantEntity> Participants => GetAll()
            .Include(p => p.User)
            .Include(p => p.Conversation)
            .AsNoTracking();
    }
}
