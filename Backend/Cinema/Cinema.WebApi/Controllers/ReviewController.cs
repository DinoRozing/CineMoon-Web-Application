using Cinema.Model;
using Cinema.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviewsAsync()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewByIdAsync(Guid id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> AddReviewAsync([FromBody] Review review)
        {
            await _reviewService.AddReviewAsync(review);
            return Ok(review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReviewAsync(Guid id, [FromBody] Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            await _reviewService.UpdateReviewAsync(review);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviewAsync(Guid id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
