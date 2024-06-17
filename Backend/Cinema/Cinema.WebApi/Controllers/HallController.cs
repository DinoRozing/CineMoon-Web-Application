using Cinema.Model;
using Cinema.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HallController : Controller
    {
        private readonly IHallService _hallService;

        public HallController(IHallService hallService)
        {
            _hallService = hallService ?? throw new ArgumentNullException(nameof(hallService));
        }

        [HttpPost]
        public async Task<IActionResult> AddHallAsync([FromBody] Hall hall)
        {
            await _hallService.AddHallAsync(hall);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHallsAsync()
        {
            var halls = await _hallService.GetAllHallsAsync();
            return Ok(halls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHallByIdAsync(Guid id)
        {
            var hall = await _hallService.GetHallByIdAsync(id);
            if (hall == null)
            {
                return NotFound();
            }
            return Ok(hall);
        }
        
        [HttpGet("halls")]
        public async Task<IActionResult> GetAvailableHallsAsync(DateOnly date, TimeOnly time, Guid movieId)
        {

            var halls = await _hallService.GetAvailableHallsAsync(date, time, movieId);
            

            return Ok(halls);
            
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHallAsync(Guid id, [FromBody] Hall hall)
        {
            if (id != hall.Id)
            {
                return BadRequest("Hall ID mismatch");
            }

            try
            {
                await _hallService.UpdateHallAsync(hall);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHallAsync(Guid id)
        {
            try
            {
                await _hallService.DeleteHallAsync(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}