using Cinema.Model;
using Cinema.Repository.Common;
using Npgsql;


namespace Cinema.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string connectionString =
            "Host=localhost;Port=5432;Database=Football;Username=postgres;Password=lozinka;";


            public async Task<Movie> GetMovieByIdAsync(Guid id)
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT * FROM ""Movie"" WHERE ""Id"" = @Id";
                        command.Parameters.AddWithValue("Id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Movie
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Title = reader.GetString(reader.GetOrdinal("Title")),
                                    Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("Description")),
                                    Duration = reader.GetTimeSpan(reader.GetOrdinal("Duration")),
                                    Language = reader.IsDBNull(reader.GetOrdinal("Language"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("Language")),
                                    CoverUrl = reader.IsDBNull(reader.GetOrdinal("CoverUrl"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("CoverUrl")),
                                    TrailerUrl = reader.IsDBNull(reader.GetOrdinal("TrailerUrl"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("TrailerUrl")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                    DateUpdated = reader.IsDBNull(reader.GetOrdinal("DateUpdated"))
                                        ? DateTime.MinValue
                                        : reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                                    CreatedByUserId = reader.IsDBNull(reader.GetOrdinal("CreatedByUserId"))
                                        ? Guid.Empty
                                        : reader.GetGuid(reader.GetOrdinal("CreatedByUserId")),
                                    UpdatedByUserId = reader.IsDBNull(reader.GetOrdinal("UpdatedByUserId"))
                                        ? Guid.Empty
                                        : reader.GetGuid(reader.GetOrdinal("UpdatedByUserId")),
                                };
                            }
                        }
                    }
                }

                return null;
            }
        
    }
}
