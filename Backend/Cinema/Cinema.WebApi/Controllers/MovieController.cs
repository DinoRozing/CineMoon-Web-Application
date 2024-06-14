using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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

        [HttpGet]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieAsync(Guid id, [FromBody] MoviePut moviePut)
        {
            
            var movie = _mapper.Map<Movie>(moviePut);
            await _movieService.UpdateMovieAsync(movie);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(Guid id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        
    }
}