using Cinema.Model;
using Cinema.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectionController : Controller
    {
        private readonly IProjectionService _projectionService;

        public ProjectionController(IProjectionService projectionService)
        {
            _projectionService = projectionService ?? throw new ArgumentNullException(nameof(projectionService));
        }

        [HttpPost]
        public async Task<IActionResult> AddProjectionAsync([FromBody] Projection projection)
        {
            await _projectionService.AddProjectionAsync(projection);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjectionsAsync()
        {
            var projections = await _projectionService.GetAllProjectionsAsync();
            return Ok(projections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectionByIdAsync(Guid id)
        {
            var projection = await _projectionService.GetProjectionByIdAsync(id);
            if (projection == null)
            {
                return NotFound();
            }
            return Ok(projection);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjectionAsync(Guid id, [FromBody] Projection projection)
        {
            if (id != projection.Id)
            {
                return BadRequest("Projection ID mismatch");
            }

            try
            {
                await _projectionService.UpdateProjectionAsync(projection);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectionAsync(Guid id)
        {
            try
            {
                await _projectionService.DeleteProjectionAsync(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}