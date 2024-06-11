using Npgsql;
using Cinema.Model;
using Cinema.Repository.Common;

namespace Cinema.Repository
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly string connectionString;
        public ProjectionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task AddProjection(Projection projection)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            var commandText = "INSERT INTO \"Projection\" (\"Id\", \"Date\", \"Time\", \"MovieId\", \"UserId\", \"IsActive\", \"DateCreated\", \"DateUpdated\", \"CreatedByUserId\", \"UpdatedByUserId\") " +
                              "VALUES (@Id, @Date, @Time, @MovieId, @HallId, @IsActive, @DateCreated, @DateUpdated, @CreatedByUserId, @UpdatedByUserId);";

            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", projection.Id);
            command.Parameters.AddWithValue("@Date", projection.Date);
            command.Parameters.AddWithValue("@Time", projection.Time);
            command.Parameters.AddWithValue("@MovieId", projection.MovieId);
            command.Parameters.AddWithValue("@UserId", projection.UserId);
            command.Parameters.AddWithValue("@IsActive", projection.IsActive);
            command.Parameters.AddWithValue("@DateCreated", projection.DateCreated);
            command.Parameters.AddWithValue("@DateUpdated", projection.DateUpdated);
            command.Parameters.AddWithValue("@CreatedByUserId", projection.CreatedByUserId);
            command.Parameters.AddWithValue("@UpdatedByUserId", projection.UpdatedByUserId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Projection>> GetAllProjections()
        {
            var projections = new List<Projection>();

            await using var connection = new NpgsqlConnection(connectionString);
            var commandText = "SELECT * FROM \"Projection\";";

            await using var command = new NpgsqlCommand(commandText, connection);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var projection = new Projection
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")).Date,
                    Time = reader.GetTimeSpan(reader.GetOrdinal("Time")),
                    MovieId = reader.GetGuid(reader.GetOrdinal("MovieId")),
                    UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                    DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                    CreatedByUserId = reader.GetGuid(reader.GetOrdinal("CreatedByUserId")),
                    UpdatedByUserId = reader.GetGuid(reader.GetOrdinal("UpdatedByUserId"))
                };
                projections.Add(projection);
            }

            return projections;
        }

        public async Task<Projection?> GetProjection(Guid id)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            var commandText = "SELECT * FROM \"Projection\" WHERE \"Id\" = @Id;";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Projection
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")).Date,
                    Time = reader.GetTimeSpan(reader.GetOrdinal("Time")),
                    MovieId = reader.GetGuid(reader.GetOrdinal("MovieId")),
                    UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                    DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                    CreatedByUserId = reader.GetGuid(reader.GetOrdinal("CreatedByUserId")),
                    UpdatedByUserId = reader.GetGuid(reader.GetOrdinal("UpdatedByUserId"))
                };
            }

            return null;
        }

        public async Task UpdateProjection(Projection projection)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            var commandText = "UPDATE \"Projection\" SET \"Date\" = @Date, \"Time\" = @Time, \"MovieId\" = @MovieId, \"UserId\" = @USerId, " +
                              "\"IsActive\" = @IsActive, \"DateUpdated\" = @DateUpdated, \"UpdatedByUserId\" = @UpdatedByUserId WHERE \"Id\" = @Id;";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", projection.Id);
            command.Parameters.AddWithValue("@Date", projection.Date);
            command.Parameters.AddWithValue("@Time", projection.Time);
            command.Parameters.AddWithValue("@MovieId", projection.MovieId);
            command.Parameters.AddWithValue("@UserId", projection.Id);
            command.Parameters.AddWithValue("@IsActive", projection.IsActive);
            command.Parameters.AddWithValue("@DateUpdated", projection.DateUpdated);
            command.Parameters.AddWithValue("@UpdatedByUserId", projection.UpdatedByUserId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteProjection(Guid id)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            var commandText = "DELETE FROM \"Projection\" WHERE \"Id\" = @Id;";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
