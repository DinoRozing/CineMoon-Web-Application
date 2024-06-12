using Cinema.Model;
using Cinema.Repository.Common;
using Cinema.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Service
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;

        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
        }

        public async Task<List<Seat>> GetAllSeatsAsync()
        {
            return await _seatRepository.GetAllSeatsAsync();
        }

        public async Task<Seat> GetSeatByIdAsync(Guid id)
        {
            return await _seatRepository.GetSeatByIdAsync(id);
        }

        public async Task AddSeatAsync(Seat seat)
        {
            await _seatRepository.AddSeatAsync(seat);
        }

        public async Task UpdateSeatAsync(Seat seat)
        {
            await _seatRepository.UpdateSeatAsync(seat);
        }

        public async Task DeleteSeatAsync(Guid id)
        {
            await _seatRepository.DeleteSeatAsync(id);
        }
    }
}
