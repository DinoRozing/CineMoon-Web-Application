using Cinema.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Cinema.Service.Common;

namespace Cinema.WebApi.Controllers
{
    

    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        
        [HttpGet]
        public async Task<OkObjectResult> GetMoviesAsync()
        {
           var movies =  await movieService.GetMoviesAsync();

            return Ok(movies);
        }


    }
}