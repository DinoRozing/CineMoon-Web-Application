using Cinema.Repository.Common;
using Cinema.Service.Common;

namespace Cinema.Service;

public class MovieService: IMovieService
{
    private readonly IMovieRepository movieRepository;
    public MovieService(IMovieRepository movieRepository)
    {
        this.movieRepository = movieRepository;
    }

    public async Task<List<string>> GetMoviesAsync()
    {
        return await movieRepository.GetMoviesAsync();
    }





}