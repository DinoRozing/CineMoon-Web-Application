using Cinema.Model;
using Cinema.Common;
using Cinema.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.MovieModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.NetworkInformation;
using System.Text;

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

        public async Task<IEnumerable<MovieGet>> GetFilteredMoviesAsync(MovieFiltering filtering, MovieSorting sorting, MoviePaging paging)
        {
            var queryBuilder = new StringBuilder(@"SELECT m.*, string_agg(a.""Name"", ', ') AS ""ActorNames""
                                          FROM ""Movie"" m
                                          LEFT JOIN ""MovieActor"" ma ON m.""Id"" = ma.""MovieId""
                                          LEFT JOIN ""Actor"" a ON ma.""ActorId"" = a.""Id""
                                          WHERE 1 = 1");

            if (filtering.MovieId != null)
            { 
                queryBuilder.Append(" AND m.\"Id\" = @MovieId");
            }

            if (!string.IsNullOrEmpty(filtering.Genre))
            {
                queryBuilder.Append(" AND m.\"Genre\" = @Genre");
            }

            if (!string.IsNullOrEmpty(filtering.Language))
            {
                queryBuilder.Append(" AND m.\"Language\" = @Language");
            }
            queryBuilder.Append(" GROUP BY m.\"Id\"");

            queryBuilder.Append($" ORDER BY \"{sorting.SortBy}\" {sorting.SortOrder}");

            queryBuilder.Append($" OFFSET {paging.PageSize * (paging.PageNumber - 1)} LIMIT {paging.PageSize};");

            await using var connection = new NpgsqlConnection(_connectionString);
            var movies = new List<MovieGet>();

            await connection.OpenAsync();
            await using var command = new NpgsqlCommand(queryBuilder.ToString(), connection);

            if (filtering.MovieId != null)
            {
                command.Parameters.AddWithValue("@MovieId", filtering.MovieId);
            }

            if (!string.IsNullOrEmpty(filtering.Genre))
            {
                command.Parameters.AddWithValue("@Genre", filtering.Genre);
            }

            if (!string.IsNullOrEmpty(filtering.Language))
            {
                command.Parameters.AddWithValue("@Language", filtering.Language);
            }

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var movie = new MovieGet
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

                movies.Add(movie);
            }
            return movies;
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            
            try
            {
                var addMovieCommandText = @"
                    INSERT INTO ""Movie"" 
                    (""Id"", ""Title"", ""Genre"", ""Description"", ""Duration"", ""Language"", 
                     ""CoverUrl"", ""TrailerUrl"", ""IsActive"", ""DateCreated"", ""DateUpdated"", 
                     ""CreatedByUserId"", ""UpdatedByUserId"")
                    VALUES 
                    (@Id, @Title, @Genre, @Description, @Duration, @Language, 
                     @CoverUrl, @TrailerUrl, @IsActive, @DateCreated, @DateUpdated, 
                     @CreatedByUserId, @UpdatedByUserId)";
                
                await using var addMovieCommand = new NpgsqlCommand(addMovieCommandText, connection);
                addMovieCommand.Parameters.AddWithValue("@Id", movie.Id);
                addMovieCommand.Parameters.AddWithValue("@Title", movie.Title);
                addMovieCommand.Parameters.AddWithValue("@Genre", movie.Genre);
                addMovieCommand.Parameters.AddWithValue("@Description", movie.Description);
                addMovieCommand.Parameters.AddWithValue("@Duration", movie.Duration);
                addMovieCommand.Parameters.AddWithValue("@Language", movie.Language);
                addMovieCommand.Parameters.AddWithValue("@CoverUrl", movie.CoverUrl);
                addMovieCommand.Parameters.AddWithValue("@TrailerUrl", movie.TrailerUrl);
                addMovieCommand.Parameters.AddWithValue("@IsActive", movie.IsActive);
                addMovieCommand.Parameters.AddWithValue("@DateCreated", movie.DateCreated);
                addMovieCommand.Parameters.AddWithValue("@DateUpdated", movie.DateUpdated);
                addMovieCommand.Parameters.AddWithValue("@CreatedByUserId", movie.CreatedByUserId);
                addMovieCommand.Parameters.AddWithValue("@UpdatedByUserId", movie.UpdatedByUserId);
                
                await addMovieCommand.ExecuteNonQueryAsync();
                
                foreach (var actorId in movie.ActorId)
                {
                    var addMovieActorCommandText = @"
                        INSERT INTO ""MovieActor"" 
                        (""MovieId"", ""ActorId"", ""CreatedByUserId"")
                        VALUES 
                        (@MovieId, @ActorId, @CreatedByUserId)";
                    
                    await using var addMovieActorCommand = new NpgsqlCommand(addMovieActorCommandText, connection);
                    addMovieActorCommand.Parameters.AddWithValue("@MovieId", movie.Id);
                    addMovieActorCommand.Parameters.AddWithValue("@ActorId", actorId);
                    addMovieActorCommand.Parameters.AddWithValue("@CreatedByUserId", movie.CreatedByUserId);
                    
                    await addMovieActorCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding movie", ex);
            }
        }
        
        public async Task<bool> MovieExistsAsync(string title)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
    
            var commandText = @"SELECT COUNT(*) FROM ""Movie"" WHERE ""Title"" = @Title";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Title", title);
    
            var count = (long)await command.ExecuteScalarAsync();
            return count > 0;
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
    }
}