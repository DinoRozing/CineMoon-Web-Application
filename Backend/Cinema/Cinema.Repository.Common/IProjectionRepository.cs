using Cinema.Model;

namespace Cinema.Repository.Common
{
    public interface IProjectionRepository
    {
        Task AddProjection(Projection projection);
        Task<IEnumerable<Projection>> GetAllProjections();
        Task<Projection?> GetProjection(Guid id);
        Task UpdateProjection(Projection projection);
        Task DeleteProjection(Guid id);
    }
}
