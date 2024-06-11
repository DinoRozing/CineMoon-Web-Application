using Cinema.Model;
using Cinema.Service.Common;
using Microsoft.AspNetCore.Mvc;


namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieByIdAsync(Guid id)
        {
            var movie = await movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }
    }
}
