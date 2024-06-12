using Cinema.Model;

namespace Cinema.Repository.Common
{
    public interface IActorRepository
    {
        Task AddActorAsync(Actor actor);
        Task<IEnumerable<Actor>> GetAllActorsAsync();
        Task<Actor?> GetActorAsync(Guid id);
        Task UpdateActorAsync(Actor actor);
        Task DeleteActorAsync(Guid id);
    }
}
