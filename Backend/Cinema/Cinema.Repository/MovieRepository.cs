using Cinema.Repository.Common;

namespace Cinema.Repository;

public class MovieRepository: IMovieRepository
{

    public async Task<List<string>> GetMoviesAsync()
    {
        var movies = new List<string>
        {
            "Inception",
            "The Matrix",
            "Interstellar",
            "The Dark Knight"
        };
        return movies;
    }

}