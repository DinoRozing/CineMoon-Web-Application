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
            _projectionRepository = projectionRepository ?? throw new ArgumentNullException(nameof(projectionRepository));
        }

        public async Task AddProjectionAsync(Projection projection)
        {
            if (projection == null)
                throw new ArgumentNullException(nameof(projection));

            await _projectionRepository.AddProjection(projection);
        }

        public async Task<IEnumerable<Projection>> GetAllProjectionsAsync()
        {
            return await _projectionRepository.GetAllProjections();
        }

        public async Task<Projection> GetProjectionByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Projection ID cannot be empty or null", nameof(id));

            return await _projectionRepository.GetProjection(id);
        }

        public async Task UpdateProjectionAsync(Projection projection)
        {
            if (projection == null)
                throw new ArgumentNullException(nameof(projection));

            await _projectionRepository.UpdateProjection(projection);
        }

        public async Task DeleteProjectionAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Projection ID cannot be empty or null", nameof(id));

            await _projectionRepository.DeleteProjection(id);
        }
    }
}
