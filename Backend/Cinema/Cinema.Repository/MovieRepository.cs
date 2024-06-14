using Cinema.Model;
using Cinema.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.MovieModel;

namespace Cinema.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string _connectionString;

        public MovieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        

        public async Task<List<MovieGet>> GetAllMoviesAsync()
        {
            var movies = new List<MovieGet>();

            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var commandText = @"
                                SELECT m.*, string_agg(a.""Name"", ', ') AS ""ActorNames""
                                FROM ""Movie"" m
                                LEFT JOIN ""MovieActor"" ma ON m.""Id"" = ma.""MovieId""
                                LEFT JOIN ""Actor"" a ON ma.""ActorId"" = a.""Id""
                                GROUP BY m.""Id""";

            await using var command = new NpgsqlCommand(commandText, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var movieGet = new MovieGet
                {
                    MovieId = reader.GetGuid(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Genre = reader.GetString(reader.GetOrdinal("Genre")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                    Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                    Language = reader.IsDBNull(reader.GetOrdinal("Language")) ? null : reader.GetString(reader.GetOrdinal("Language")),
                    CoverUrl = reader.IsDBNull(reader.GetOrdinal("CoverUrl")) ? null : reader.GetString(reader.GetOrdinal("CoverUrl")),
                    TrailerUrl = reader.IsDBNull(reader.GetOrdinal("TrailerUrl")) ? null : reader.GetString(reader.GetOrdinal("TrailerUrl")),
                    ActorNames = reader.IsDBNull(reader.GetOrdinal("ActorNames")) ? null : reader.GetString(reader.GetOrdinal("ActorNames")).Split(", ").ToList(),
                };

                movies.Add(movieGet);
            }

            return movies;
        }

        
        public async Task<MovieGet> GetMovieByIdAsync(Guid id)
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
    
            var commandText = @"
                            SELECT m.*, string_agg(a.""Name"", ', ') AS ""ActorNames""
                            FROM ""Movie"" m
                            LEFT JOIN ""MovieActor"" ma ON m.""Id"" = ma.""MovieId""
                            LEFT JOIN ""Actor"" a ON ma.""ActorId"" = a.""Id""
                            WHERE m.""Id"" = @Id
                            GROUP BY m.""Id""";

            var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("Id", id);

            var reader = await command.ExecuteReaderAsync();

            MovieGet movie = null;

            if (await reader.ReadAsync())
            {
                movie = new MovieGet
                {
                    MovieId = reader.GetGuid(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Genre = reader.GetString(reader.GetOrdinal("Genre")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                    Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                    Language = reader.IsDBNull(reader.GetOrdinal("Language")) ? null : reader.GetString(reader.GetOrdinal("Language")),
                    CoverUrl = reader.IsDBNull(reader.GetOrdinal("CoverUrl")) ? null : reader.GetString(reader.GetOrdinal("CoverUrl")),
                    TrailerUrl = reader.IsDBNull(reader.GetOrdinal("TrailerUrl")) ? null : reader.GetString(reader.GetOrdinal("TrailerUrl")),
                    ActorNames = reader.IsDBNull(reader.GetOrdinal("ActorNames")) ? null : reader.GetString(reader.GetOrdinal("ActorNames")).Split(", ").ToList(),
                };
            }

            connection.Close(); 

            return movie;
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var commandText = @"
                INSERT INTO ""Movie"" 
                (""Id"", ""Title"", ""Genre"", ""Description"", ""Duration"", ""Language"", ""CoverUrl"", ""TrailerUrl"", ""IsActive"", ""DateCreated"", ""DateUpdated"", ""CreatedByUserId"", ""UpdatedByUserId"") 
                VALUES 
                (@Id, @Title, @Genre, @Description, @Duration, @Language, @CoverUrl, @TrailerUrl, @IsActive, @DateCreated, @DateUpdated, @CreatedByUserId, @UpdatedByUserId);";

            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", movie.Id);
            command.Parameters.AddWithValue("@Title", movie.Title);
            command.Parameters.AddWithValue("@Genre", movie.Genre);
            command.Parameters.AddWithValue("@Description", movie.Description);
            command.Parameters.AddWithValue("@Duration", movie.Duration);
            command.Parameters.AddWithValue("@Language", movie.Language);
            command.Parameters.AddWithValue("@CoverUrl", movie.CoverUrl);
            command.Parameters.AddWithValue("@TrailerUrl", movie.TrailerUrl);
            command.Parameters.AddWithValue("@IsActive", movie.IsActive);
            command.Parameters.AddWithValue("@DateCreated", movie.DateCreated);
            command.Parameters.AddWithValue("@DateUpdated", movie.DateUpdated);
            command.Parameters.AddWithValue("@CreatedByUserId", movie.CreatedByUserId);
            command.Parameters.AddWithValue("@UpdatedByUserId", movie.UpdatedByUserId);

            await command.ExecuteNonQueryAsync();
        }
        
        public async Task AddMovieActorAsync(Guid movieId, Guid actorId, Guid userId)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var commandText = @"
                INSERT INTO ""MovieActor"" 
                (""Id"", ""MovieId"", ""ActorId"", ""DateCreated"", ""DateUpdated"", ""CreatedByUserId"", ""UpdatedByUserId"") 
                VALUES 
                (@Id, @MovieId, @ActorId, @DateCreated, @DateUpdated, @CreatedByUserId, @UpdatedByUserId);";

            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", Guid.NewGuid());
            command.Parameters.AddWithValue("@MovieId", movieId);
            command.Parameters.AddWithValue("@ActorId", actorId);
            command.Parameters.AddWithValue("@DateCreated", DateTime.UtcNow);
            command.Parameters.AddWithValue("@DateUpdated", DateTime.UtcNow);
            command.Parameters.AddWithValue("@CreatedByUserId", userId);
            command.Parameters.AddWithValue("@UpdatedByUserId", userId);

            await command.ExecuteNonQueryAsync();
        }


        public async Task UpdateMovieAsync(Movie movie)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var commandText = @"UPDATE ""Movie"" SET ""Title"" = @Title, ""Description"" = @Description, ""Duration"" = @Duration, ""Language"" = @Language, 
                                    ""CoverUrl"" = @CoverUrl, ""TrailerUrl"" = @TrailerUrl, ""IsActive"" = @IsActive, ""DateUpdated"" = @DateUpdated, ""UpdatedByUserId"" = @UpdatedByUserId 
                                    WHERE ""Id"" = @Id";
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Id", movie.Id);
                    command.Parameters.AddWithValue("@Title", movie.Title ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", (object?)movie.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Duration", movie.Duration);
                    command.Parameters.AddWithValue("@Language", (object?)movie.Language ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CoverUrl", (object?)movie.CoverUrl ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TrailerUrl", (object?)movie.TrailerUrl ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", movie.IsActive);
                    command.Parameters.AddWithValue("@DateUpdated", movie.DateUpdated);
                    command.Parameters.AddWithValue("@UpdatedByUserId", movie.UpdatedByUserId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteMovieAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var commandText = @"DELETE FROM ""Movie"" WHERE ""Id"" = @Id";
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // public async Task<Movie> GetMovieWithActorsAsync(Guid movieId)
        // {
        //     Movie movie = null;
        //     using (var connection = new NpgsqlConnection(connectionString))
        //     {
        //         await connection.OpenAsync();
        //
        //         using (var movieCommand = new NpgsqlCommand(@"SELECT * FROM ""Movie"" WHERE ""Id"" = @MovieId", connection))
        //         {
        //             movieCommand.Parameters.AddWithValue("MovieId", movieId);
        //             using (var reader = await movieCommand.ExecuteReaderAsync())
        //             {
        //                 if (await reader.ReadAsync())
        //                 {
        //                     movie = new Movie
        //                     {
        //                         Id = reader.GetGuid(reader.GetOrdinal("Id")),
        //                         Title = reader.GetString(reader.GetOrdinal("Title")),
        //                         Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
        //                         Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
        //                         Language = reader.IsDBNull(reader.GetOrdinal("Language")) ? null : reader.GetString(reader.GetOrdinal("Language")),
        //                         CoverUrl = reader.IsDBNull(reader.GetOrdinal("CoverUrl")) ? null : reader.GetString(reader.GetOrdinal("CoverUrl")),
        //                         TrailerUrl = reader.IsDBNull(reader.GetOrdinal("TrailerUrl")) ? null : reader.GetString(reader.GetOrdinal("TrailerUrl")),
        //                         IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
        //                         DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
        //                         DateUpdated = reader.IsDBNull(reader.GetOrdinal("DateUpdated")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
        //                         CreatedByUserId = reader.IsDBNull(reader.GetOrdinal("CreatedByUserId")) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal("CreatedByUserId")),
        //                         UpdatedByUserId = reader.IsDBNull(reader.GetOrdinal("UpdatedByUserId")) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal("UpdatedByUserId")),
        //                     };
        //                 }
        //             }
        //         }
        //
        //         if (movie == null)
        //         {
        //             return null;
        //         }
        //
        //         using (var actorsCommand = new NpgsqlCommand(
        //             @"SELECT a.""Id"", a.""Name"", a.""IsActive"", a.""DateCreated"", a.""DateUpdated"", a.""CreatedByUserId"", a.""UpdatedByUserId"" 
        //               FROM ""Actor"" a 
        //               INNER JOIN ""MovieActor"" ma ON a.""Id"" = ma.""ActorId"" 
        //               WHERE ma.""MovieId"" = @MovieId", connection))
        //         {
        //             actorsCommand.Parameters.AddWithValue("MovieId", movieId);
        //             using (var reader = await actorsCommand.ExecuteReaderAsync())
        //             {
        //                 var actors = new List<Actor>();
        //                 while (await reader.ReadAsync())
        //                 {
        //                     var actor = new Actor
        //                     {
        //                         Id = reader.GetGuid(reader.GetOrdinal("Id")),
        //                         Name = reader.GetString(reader.GetOrdinal("Name")),
        //                         IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
        //                         DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
        //                         DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
        //                         CreatedByUserId = reader.GetGuid(reader.GetOrdinal("CreatedByUserId")),
        //                         UpdatedByUserId = reader.GetGuid(reader.GetOrdinal("UpdatedByUserId"))
        //                     };
        //                     actors.Add(actor);
        //                 }
        //                 movie.Actors = actors;
        //             }
        //         }
        //     }
        //     return movie;
        // }
    }
}