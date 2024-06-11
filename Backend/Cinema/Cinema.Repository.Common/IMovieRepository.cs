using Cinema.Model;

namespace Cinema.Repository.Common
{
    public interface IMovieRepository
    {
        Task<Movie> GetMovieByIdAsync(Guid id);
    }
}