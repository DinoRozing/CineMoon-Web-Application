using Cinema.Model;
using Cinema.Repository.Common;
using Cinema.Service.Common;
using System;
using System.Threading.Tasks;
using DTO.MovieModel;

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
        
        public async Task<List<MovieGet>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllMoviesAsync();
        }
        
        public async Task<MovieGet> GetMovieByIdAsync(Guid id)
        {
            return await _movieRepository.GetMovieByIdAsync(id);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await _movieRepository.AddMovieAsync(movie);
            
            var existingActors = await _actorRepository.GetActorsByNameAsync(movie.ActorNames);
            
            var existingActorNames = existingActors.Select(a => a.Name).ToList();
            var newActorNames = movie.ActorNames.Except(existingActorNames).ToList();
            
            foreach (var actorName in newActorNames)
            {
                var newActor = new Actor
                {
                    Id = Guid.NewGuid(),
                    Name = actorName,
                    IsActive = true,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    CreatedByUserId = movie.CreatedByUserId,
                    UpdatedByUserId = movie.UpdatedByUserId
                };
                await _actorRepository.AddActorAsync(newActor);
                existingActors.Add(newActor); 
            }
            
            foreach (var actor in existingActors)
            {
                await _movieRepository.AddMovieActorAsync(movie.Id, actor.Id, movie.CreatedByUserId);
            }
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            var existingMovie = await _movieRepository.GetMovieByIdAsync(movie.Id);

            if (existingMovie == null)
            {
                throw new ArgumentException($"Movie with ID {movie.Id} not found.");
            }

            var updatedMovie = new Movie();

            // Ažuriranje podataka filma
            updatedMovie.Title = movie.Title;
            updatedMovie.Genre = movie.Genre;
            updatedMovie.Description = movie.Description;
            updatedMovie.Duration = movie.Duration;
            updatedMovie.Language = movie.Language;
            updatedMovie.CoverUrl = movie.CoverUrl;
            updatedMovie.TrailerUrl = movie.TrailerUrl;
            updatedMovie.DateUpdated = DateTime.UtcNow;
            updatedMovie.UpdatedByUserId = movie.UpdatedByUserId; 
            
            var updatedActorNames = new List<string>();

            foreach (var actorName in movie.ActorNames)
            {
                var actor = await _actorRepository.GetActorByNameAsync(actorName);

                if (actor == null)
                {
                    var newActor = new Actor
                    {
                        Id = Guid.NewGuid(),
                        Name = actorName,
                        IsActive = true,
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow,
                        CreatedByUserId = movie.UpdatedByUserId,
                        UpdatedByUserId = movie.UpdatedByUserId,
                    };

                    await _actorRepository.AddActorAsync(newActor);
                }
                else
                {
                    updatedActorNames.Add(actorName);
                }
            }
            
            existingMovie.ActorNames = updatedActorNames;
            
            await _movieRepository.UpdateMovieAsync(updatedMovie);
            
            await _movieRepository.UpdateMovieAsync(movie);
        }

        public async Task DeleteMovieAsync(Guid id)
        {
            await _movieRepository.DeleteMovieAsync(id);
        }
    }
}