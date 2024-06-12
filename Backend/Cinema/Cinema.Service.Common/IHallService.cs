using Cinema.Model;

namespace Cinema.Service.Common
{
    public interface IHallService
    {
        Task AddHallAsync(Hall hall);
        Task<IEnumerable<Hall>> GetAllHallsAsync();
        Task<Hall> GetHallByIdAsync(Guid id);
        Task UpdateHallAsync(Hall hall);
        Task DeleteHallAsync(Guid id);
    }
}
