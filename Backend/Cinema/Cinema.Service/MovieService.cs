using Cinema.Model;
using Cinema.Repository.Common;
using Cinema.Service.Common;
using System;
using System.Threading.Tasks;
using DTO.MovieModel;
using Cinema.Common;

namespace Cinema.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;

        public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }
        
        public async Task AddMovieAsync(Movie movie)
        {
            if (await _movieRepository.MovieExistsAsync(movie.Title))
            {
                throw new ArgumentException("Movie with the same title already exists.");
            }
            
            await _movieRepository.AddMovieAsync(movie);
        }
        public async Task AddActorToMovieAsync(Guid movieId, Guid actorId)
        {
             await _movieRepository.AddActorToMovieAsync(movieId, actorId);
        }
        public async Task<List<MovieGet>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllMoviesAsync();
        }
        
        
        public async Task<MovieGet> GetMovieByIdAsync(Guid id)
        {
            return await _movieRepository.GetMovieByIdAsync(id);
        }

        public async Task<IEnumerable<MovieGet>> GetFilteredMoviesAsync(MovieFiltering filtering, MovieSorting sorting, MoviePaging paging)
        {
            return await _movieRepository.GetFilteredMoviesAsync(filtering, sorting, paging);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            var existingMovie = await _movieRepository.GetMovieByIdAsync(movie.Id);
            
            if (existingMovie == null)
            {
                throw new ArgumentException($"Movie with ID {movie.Id} not found.");
            }
            
            var updatedMovie = new Movie();
            
            updatedMovie.Id = movie.Id;
            updatedMovie.Title = movie.Title;
            updatedMovie.GenreId = movie.GenreId;
            updatedMovie.Description = movie.Description;
            updatedMovie.Duration = movie.Duration;
            updatedMovie.LanguageId = movie.LanguageId;
            updatedMovie.CoverUrl = movie.CoverUrl;
            updatedMovie.TrailerUrl = movie.TrailerUrl;
            updatedMovie.DateUpdated = DateTime.UtcNow;
            updatedMovie.UpdatedByUserId = movie.UpdatedByUserId; 
            
            await _movieRepository.UpdateMovieAsync(updatedMovie);
            
        }

        public async Task DeleteMovieAsync(Guid id)
        {
            await _movieRepository.DeleteMovieAsync(id);
        }
        
        public async Task DeleteActorFromMovie(Guid movieId, Guid actorId)
        {
            await _movieRepository.DeleteActorFromMovie(movieId, actorId);
        }
    }
}