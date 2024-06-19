using Cinema.Model;
using Cinema.Repository.Common;
using DTO.HallModel;
using Npgsql;

namespace Cinema.Repository
{
    public class HallRepository : IHallRepository
    {
        private readonly string _connectionString;
        public HallRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task AddHallAsync(Hall hall)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var commandText = "INSERT INTO \"Hall\" (\"Id\", \"HallNumber\") VALUES (@Id, @HallNumber);";

            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", hall.Id);
            command.Parameters.AddWithValue("@HallNumber", hall.HallNumber);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Hall>> GetAllHallsAsync()
        {
            var halls = new List<Hall>();

            await using var connection = new NpgsqlConnection(_connectionString);
            var commandText = "SELECT * FROM \"Hall\";";

            await using var command = new NpgsqlCommand(commandText, connection);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var hall = new Hall
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    HallNumber = reader.GetInt32(reader.GetOrdinal("HallNumber"))
                };
                halls.Add(hall);
            }

            return halls;
        }

        public async Task<Hall?> GetHallByIdAsync(Guid id)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var commandText = "SELECT * FROM \"Hall\" WHERE \"Id\" = @Id;";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Hall
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    HallNumber = reader.GetInt32(reader.GetOrdinal("HallNumber"))
                };
            }

            return null;
        }
        
        public async Task<List<AvailableHallGet>> GetAvailableHallsAsync(DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            var halls = new List<AvailableHallGet>();

            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var commandText = @"
                SELECT h.*
                FROM ""Hall"" h
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM ""ProjectionHall"" ph
                    INNER JOIN ""Projection"" p ON ph.""ProjectionId"" = p.""Id""
                    WHERE ph.""HallId"" = h.""Id""
                    AND (
                        p.""Date"" = @Date
                        AND (
                            p.""Time"" >= @StartTime
                            AND p.""Time"" <= @EndTime
                        )
                    )
                );
            ";

            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Date", date);
            command.Parameters.AddWithValue("@StartTime", startTime);
            command.Parameters.AddWithValue("@EndTime", endTime);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var hall = new AvailableHallGet
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    HallNumber = reader.GetInt32(reader.GetOrdinal("HallNumber")),
                };

                halls.Add(hall);
            }

            return halls;
        }
    


        public async Task UpdateHallAsync(Hall hall)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var commandText = "UPDATE \"Hall\" SET \"HallNumber\" = @HallNumber WHERE \"Id\" = @Id;";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", hall.Id);
            command.Parameters.AddWithValue("@HallNumber", hall.HallNumber);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteHallAsync(Guid id)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            var commandText = "DELETE FROM \"Hall\" WHERE \"Id\" = @Id;";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
