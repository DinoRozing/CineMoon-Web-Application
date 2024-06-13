using AutoMapper;
using DTO;
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
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviewsAsync()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            var reviewRests = _mapper.Map<IEnumerable<ReviewRest>>(reviews);
            return Ok(reviewRests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewByIdAsync(Guid id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            var reviewRest = _mapper.Map<ReviewRest>(review);
            return Ok(reviewRest);
        }

        [HttpPost]
        public async Task<IActionResult> AddReviewAsync([FromBody] ReviewRest reviewRest)
        {
            var review = _mapper.Map<Review>(reviewRest);
            await _reviewService.AddReviewAsync(review);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReviewAsync(Guid id, [FromBody] ReviewRest reviewRest)
        {
            if (id != reviewRest.Id)
            {
                return BadRequest();
            }

            var review = _mapper.Map<Review>(reviewRest);
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
