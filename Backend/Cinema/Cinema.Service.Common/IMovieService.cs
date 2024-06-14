using Cinema.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.MovieModel;

namespace Cinema.Service.Common
{
    public interface IMovieService
    {
        Task<List<MovieGet>> GetAllMoviesAsync();
        Task<MovieGet> GetMovieByIdAsync(Guid id);
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(Guid id);
        
    }
}
