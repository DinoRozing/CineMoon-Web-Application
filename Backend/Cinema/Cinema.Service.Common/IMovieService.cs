using Cinema.Model;

namespace Cinema.Service.Common
{
    public interface IMovieService
    {
        Task<Movie> GetMovieByIdAsync(Guid id);
    }
}