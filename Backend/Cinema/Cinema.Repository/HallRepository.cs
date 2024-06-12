using Cinema.Model;
using Cinema.Repository.Common;
using Npgsql;

namespace Cinema.Repository
{
    public class HallRepository : IHallRepository
    {
        private readonly string connectionString;
        public HallRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task AddHallAsync(Hall hall)
        {
            await using var connection = new NpgsqlConnection(connectionString);
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

            await using var connection = new NpgsqlConnection(connectionString);
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
            await using var connection = new NpgsqlConnection(connectionString);
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

        public async Task UpdateHallAsync(Hall hall)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            var commandText = "UPDATE \"Hall\" SET \"HallNumber\" = @HallNumber WHERE \"Id\" = @Id;";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", hall.Id);
            command.Parameters.AddWithValue("@HallNumber", hall.HallNumber);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteHallAsync(Guid id)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            var commandText = "DELETE FROM \"Hall\" WHERE \"Id\" = @Id;";
            await using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
