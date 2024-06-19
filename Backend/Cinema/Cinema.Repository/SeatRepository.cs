using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Model;
using Cinema.Repository.Common;
using Npgsql;

namespace Cinema.Repository
{
    public class SeatRepository : ISeatRepository
    {
        private readonly string _connectionString;

        public SeatRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Seat>> GetAllSeatsAsync()
        {
            var seats = new List<Seat>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var commandText = "SELECT * FROM \"Seat\"";
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var seat = new Seat
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                SeatNumber = reader.IsDBNull(reader.GetOrdinal("SeatNumber")) ? -1 : reader.GetInt32(reader.GetOrdinal("SeatNumber")),
                                HallId = reader.GetGuid(reader.GetOrdinal("HallId")),
                            };
                            seats.Add(seat);
                        }
                    }
                }
            }

            return seats;
        }

        public async Task<Seat> GetSeatByIdAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(@"SELECT * FROM ""Seat"" WHERE ""Id"" = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Seat
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                SeatNumber = reader.IsDBNull(reader.GetOrdinal("SeatNumber")) ? -1 : reader.GetInt32(reader.GetOrdinal("SeatNumber")),
                                HallId = reader.GetGuid(reader.GetOrdinal("HallId")),
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task AddSeatAsync(Seat seat)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var commandText = @"INSERT INTO ""Seat"" (""Id"", ""SeatNumber"", ""HallId"") 
                                    VALUES (@Id, @SeatNumber, @HallId)";
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Id", seat.Id);
                    command.Parameters.AddWithValue("@SeatNumber", (object?)seat.SeatNumber ?? DBNull.Value);
                    command.Parameters.AddWithValue("@HallId", seat.HallId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateSeatAsync(Seat seat)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var commandText = @"UPDATE ""Seat"" SET ""SeatNumber"" = @SeatNumber, ""HallId"" = @HallId WHERE ""Id"" = @Id";
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Id", seat.Id);
                    command.Parameters.AddWithValue("@SeatNumber", (object?)seat.SeatNumber ?? DBNull.Value);
                    command.Parameters.AddWithValue("@HallId", seat.HallId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteSeatAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var commandText = @"DELETE FROM ""Seat"" WHERE ""Id"" = @Id";
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
