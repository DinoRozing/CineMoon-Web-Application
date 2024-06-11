namespace Cinema.Service.Common;

public interface IMovieService
{
    Task<List<string>> GetMoviesAsync();

}