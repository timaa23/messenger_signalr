using back_messenger_signalr.Entities;

namespace back_messenger_signalr.Repositories.Interfaces
{
    public interface IParticipantRepository : IGenericRepository<ParticipantEntity, int>
    {
        IQueryable<ParticipantEntity> ParticipantsEager { get; }
    }
}
