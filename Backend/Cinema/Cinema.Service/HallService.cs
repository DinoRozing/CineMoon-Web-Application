using Cinema.Model;
using Cinema.Service.Common;
using Cinema.Repository.Common;

namespace Cinema.Service
{
    public class HallService : IHallService
    {
        private readonly IHallRepository _hallRepository;

        public HallService(IHallRepository hallRepository)
        {
            this._hallRepository = hallRepository;
        }

        public async Task AddHallAsync(Hall hall)
        {
            await _hallRepository.AddHallAsync(hall);
        }

        public async Task<IEnumerable<Hall>> GetAllHallsAsync()
        {
            return await _hallRepository.GetAllHallsAsync();
        }

        public async Task<Hall> GetHallByIdAsync(Guid id)
        {
            return await _hallRepository.GetHallByIdAsync(id);
        }

        public async Task UpdateHallAsync(Hall hall)
        {
            await _hallRepository.UpdateHallAsync(hall);
        }

        public async Task DeleteHallAsync(Guid id)
        {
            await _hallRepository.DeleteHallAsync(id);
        }
    }
}
