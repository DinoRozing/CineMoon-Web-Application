using Cinema.Model;
using Cinema.Repository.Common;
using Cinema.Service.Common;

namespace Cinema.Service
{
    public class ProjectionService : IProjectionService
    {
        private readonly IProjectionRepository _projectionRepository;

        public ProjectionService(IProjectionRepository projectionRepository)
        {
            this._projectionRepository = projectionRepository;
        }

        public async Task<List<Projection>> GetAllProjectionsAsync()
        {
            return await _projectionRepository.GetAllProjectionsAsync();
        }

        public async Task<Projection> GetProjectionByIdAsync(Guid id)
        {
            return await _projectionRepository.GetProjectionByIdAsync(id);
        }

        public async Task AddProjectionAsync(Projection projection)
        {
            await _projectionRepository.AddProjectionAsync(projection);
        }

        public async Task UpdateProjectionAsync(Projection projection)
        {
            await _projectionRepository.UpdateProjectionAsync(projection);
        }

        public async Task DeleteProjectionAsync(Guid id)
        {
            await _projectionRepository.DeleteProjectionAsync(id);
        }
    }
}
