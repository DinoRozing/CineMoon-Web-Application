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

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await movieRepository.GetAllMoviesAsync();
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await movieRepository.AddMovieAsync(movie);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            await movieRepository.UpdateMovieAsync(movie);
        }

        public async Task DeleteMovieAsync(Guid id)
        {
            await movieRepository.DeleteMovieAsync(id);
        }

        public async Task<Movie> GetMovieWithActorsAsync(Guid movieId)
        {
            return await movieRepository.GetMovieWithActorsAsync(movieId);
        }
    }
}