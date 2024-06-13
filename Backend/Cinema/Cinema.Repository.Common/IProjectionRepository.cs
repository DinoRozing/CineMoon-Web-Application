using Cinema.Model;

namespace Cinema.Repository.Common
{
    public interface IProjectionRepository
    {
        Task AddProjectionAsync(Projection projection);
        Task<List<Projection>> GetAllProjectionsAsync();
        Task<Projection?> GetProjectionByIdAsync(Guid id);
        Task UpdateProjectionAsync(Projection projection);
        Task DeleteProjectionAsync(Guid id);
        Task<List<Projection>> GetAllProjectionsWithHallsAsync();
    }
}
