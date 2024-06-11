using Cinema.Model;

namespace Cinema.Service.Common
{
    public interface IProjectionService
    {
        Task AddProjectionAsync(Projection projection);

        Task<IEnumerable<Projection>> GetAllProjectionsAsync();

        Task<Projection> GetProjectionByIdAsync(Guid id);

        Task UpdateProjectionAsync(Projection projection);

        Task DeleteProjectionAsync(Guid id);
    }
}
