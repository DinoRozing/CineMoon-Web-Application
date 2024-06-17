using AutoMapper;
using Cinema.Common;
using Cinema.Model;
using Cinema.Service.Common;
using DTO.MovieModel;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IActorService _actorService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovieAsync([FromBody] MoviePost moviePost)
        {
            try
            {
                var movie = _mapper.Map<Movie>(moviePost);
                movie.Id = Guid.NewGuid();
                movie.IsActive = true;
                movie.DateCreated = DateTime.UtcNow;
                movie.DateUpdated = DateTime.UtcNow;
                movie.CreatedByUserId = movie.CreatedByUserId;
                movie.UpdatedByUserId = movie.CreatedByUserId;

                await _movieService.AddMovieAsync(movie);

                return Ok("Movie created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPost("{movieId}/actor/{actorId}")]
        public async Task<IActionResult> AddActorToMovie(Guid movieId, Guid actorId)
        {
            try
            {
                await _movieService.AddActorToMovieAsync(movieId, actorId);
                
                return Ok();
            
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            try
            {
                var movies = await _movieService.GetAllMoviesAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByIdAsync(Guid id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route ("ByFilter")]
        public async Task<IActionResult> GetFilteredMoviesAsync(Guid? movieId, Guid? genreId, Guid? languageId, string sortBy = "Duration", string sortOrder = "DESC", int pageNumber = 1, int pageSize = 4)
        {
            try
            {
                var filtering = new MovieFiltering
                {
                    MovieId = movieId,
                    GenreId = genreId,
                    LanguageId = languageId
                };
                
                var sorting = new MovieSorting
                {
                    SortBy = sortBy,
                    SortOrder = sortOrder
                };

                var paging = new MoviePaging
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var movies = await _movieService.GetFilteredMoviesAsync(filtering, sorting, paging);

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieAsync(Guid id, [FromBody] MoviePut moviePut)
        {
            try
            {
                var movie = _mapper.Map<Movie>(moviePut);
                await _movieService.UpdateMovieAsync(movie);

                return Ok();
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(Guid id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpDelete("{movieId}/actor/{actorId}")]
        public async Task<IActionResult> DeleteActorFromMovie(Guid movieId, Guid actorId)
        {
            try
            {
                await _movieService.DeleteActorFromMovie(movieId, actorId);
                
                return Ok();
            
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}