using Cinema.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Repository.Common
{
    public interface IMovieRepository
    {
        Task<Movie> GetMovieByIdAsync(Guid id);
        Task<List<Movie>> GetAllMoviesAsync();
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(Guid id);
    }
}
