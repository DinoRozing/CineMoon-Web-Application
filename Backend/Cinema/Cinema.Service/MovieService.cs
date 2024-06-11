using Cinema.Model;
using Cinema.Repository.Common;
using Cinema.Service.Common;
using System;
using System.Threading.Tasks;

namespace Cinema.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<Movie> GetMovieByIdAsync(Guid id)
        {
            return await movieRepository.GetMovieByIdAsync(id);
        }
    }
}