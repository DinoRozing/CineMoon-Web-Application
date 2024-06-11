namespace Cinema.Repository.Common;

public interface IMovieRepository
{
    Task<List<string>> GetMoviesAsync();
}