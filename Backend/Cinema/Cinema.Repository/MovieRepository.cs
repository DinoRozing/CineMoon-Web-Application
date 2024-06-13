using Cinema.Model;
using Cinema.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string connectionString;

        public MovieRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<Movie> GetMovieByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT * FROM ""Movie"" WHERE ""Id"" = @Id", connection))
                {
                    command.Parameters.AddWithValue("Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Movie
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                Language = reader.IsDBNull(reader.GetOrdinal("Language")) ? null : reader.GetString(reader.GetOrdinal("Language")),
                                CoverUrl = reader.IsDBNull(reader.GetOrdinal("CoverUrl")) ? null : reader.GetString(reader.GetOrdinal("CoverUrl")),
                                TrailerUrl = reader.IsDBNull(reader.GetOrdinal("TrailerUrl")) ? null : reader.GetString(reader.GetOrdinal("TrailerUrl")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                DateUpdated = reader.IsDBNull(reader.GetOrdinal("DateUpdated")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                                CreatedByUserId = reader.IsDBNull(reader.GetOrdinal("CreatedByUserId")) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal("CreatedByUserId")),
                                UpdatedByUserId = reader.IsDBNull(reader.GetOrdinal("UpdatedByUserId")) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal("UpdatedByUserId")),
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            var movies = new List<Movie>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var commandText = "SELECT * FROM \"Movie\"";
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var movie = new Movie
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                Language = reader.IsDBNull(reader.GetOrdinal("Language")) ? null : reader.GetString(reader.GetOrdinal("Language")),
                                CoverUrl = reader.IsDBNull(reader.GetOrdinal("CoverUrl")) ? null : reader.GetString(reader.GetOrdinal("CoverUrl")),
                                TrailerUrl = reader.IsDBNull(reader.GetOrdinal("TrailerUrl")) ? null : reader.GetString(reader.GetOrdinal("TrailerUrl")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                DateUpdated = reader.IsDBNull(reader.GetOrdinal("DateUpdated")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                                CreatedByUserId = reader.IsDBNull(reader.GetOrdinal("CreatedByUserId")) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal("CreatedByUserId")),
                                UpdatedByUserId = reader.IsDBNull(reader.GetOrdinal("UpdatedByUserId")) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal("UpdatedByUserId")),
                            };
                            movies.Add(movie);
                        }
                    }
                }
            }

            return movies;
        }

        public async Task AddMovieAsync(Movie movie)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var commandText = @"INSERT INTO ""Movie"" (""Id"", ""Title"", ""Description"", ""Duration"", ""Language"", ""CoverUrl"", ""TrailerUrl"", ""IsActive"", ""DateCreated"", ""DateUpdated"", ""CreatedByUserId"", ""UpdatedByUserId"") 
                                    VALUES (@Id, @Title, @Description, @Duration, @Language, @CoverUrl, @TrailerUrl, @IsActive, @DateCreated, @DateUpdated, @CreatedByUserId, @UpdatedByUserId)";
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
                    command.Parameters.AddWithValue("@DateCreated", movie.DateCreated);
                    command.Parameters.AddWithValue("@DateUpdated", movie.DateUpdated);
                    command.Parameters.AddWithValue("@CreatedByUserId", movie.CreatedByUserId);
                    command.Parameters.AddWithValue("@UpdatedByUserId", movie.UpdatedByUserId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            using (var connection = new NpgsqlConnection(connectionString))
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
            using (var connection = new NpgsqlConnection(connectionString))
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

        public async Task<Movie> GetMovieWithActorsAsync(Guid movieId)
        {
            Movie movie = null;
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var movieCommand = new NpgsqlCommand(@"SELECT * FROM ""Movie"" WHERE ""Id"" = @MovieId", connection))
                {
                    movieCommand.Parameters.AddWithValue("MovieId", movieId);
                    using (var reader = await movieCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            movie = new Movie
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                Language = reader.IsDBNull(reader.GetOrdinal("Language")) ? null : reader.GetString(reader.GetOrdinal("Language")),
                                CoverUrl = reader.IsDBNull(reader.GetOrdinal("CoverUrl")) ? null : reader.GetString(reader.GetOrdinal("CoverUrl")),
                                TrailerUrl = reader.IsDBNull(reader.GetOrdinal("TrailerUrl")) ? null : reader.GetString(reader.GetOrdinal("TrailerUrl")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                DateUpdated = reader.IsDBNull(reader.GetOrdinal("DateUpdated")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                                CreatedByUserId = reader.IsDBNull(reader.GetOrdinal("CreatedByUserId")) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal("CreatedByUserId")),
                                UpdatedByUserId = reader.IsDBNull(reader.GetOrdinal("UpdatedByUserId")) ? Guid.Empty : reader.GetGuid(reader.GetOrdinal("UpdatedByUserId")),
                            };
                        }
                    }
                }

                if (movie == null)
                {
                    return null;
                }

                using (var actorsCommand = new NpgsqlCommand(
                    @"SELECT a.""Id"", a.""Name"", a.""IsActive"", a.""DateCreated"", a.""DateUpdated"", a.""CreatedByUserId"", a.""UpdatedByUserId"" 
                      FROM ""Actor"" a 
                      INNER JOIN ""MovieActor"" ma ON a.""Id"" = ma.""ActorId"" 
                      WHERE ma.""MovieId"" = @MovieId", connection))
                {
                    actorsCommand.Parameters.AddWithValue("MovieId", movieId);
                    using (var reader = await actorsCommand.ExecuteReaderAsync())
                    {
                        var actors = new List<Actor>();
                        while (await reader.ReadAsync())
                        {
                            var actor = new Actor
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                                CreatedByUserId = reader.GetGuid(reader.GetOrdinal("CreatedByUserId")),
                                UpdatedByUserId = reader.GetGuid(reader.GetOrdinal("UpdatedByUserId"))
                            };
                            actors.Add(actor);
                        }
                        movie.Actors = actors;
                    }
                }
            }
            return movie;
        }
    }
}