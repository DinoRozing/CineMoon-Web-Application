using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Model;
using Cinema.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeatController : Controller
    {
        private readonly ISeatService _seatService;

        public SeatController(ISeatService seatService)
        {
            _seatService = seatService ?? throw new ArgumentNullException(nameof(seatService));
        }

        [HttpPost]
        public async Task<IActionResult> AddSeatAsync([FromBody] Seat seat)
        {
            await _seatService.AddSeatAsync(seat);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSeatsAsync()
        {
            var seats = await _seatService.GetAllSeatsAsync();
            return Ok(seats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeatByIdAsync(Guid id)
        {
            var seat = await _seatService.GetSeatByIdAsync(id);
            if (seat == null)
            {
                return NotFound();
            }
            return Ok(seat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeatAsync(Guid id, [FromBody] Seat seat)
        {
            if (id != seat.Id)
            {
                return BadRequest("Seat ID mismatch");
            }

            try
            {
                await _seatService.UpdateSeatAsync(seat);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeatAsync(Guid id)
        {
            try
            {
                await _seatService.DeleteSeatAsync(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
