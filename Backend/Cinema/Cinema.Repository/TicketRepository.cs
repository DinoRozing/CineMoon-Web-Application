using Cinema.Model;
using Cinema.Repository.Common;
using Npgsql;

namespace Cinema.Repository;

public class TicketRepository: ITicketRepository
{
    private readonly string _connectionString;

    public TicketRepository(string connectionString)
    {
        this._connectionString = connectionString;
    }
    
    public async Task CreateTicketAsync(Ticket ticket)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        var commandText = @"INSERT INTO ""Ticket"" (""Id"", ""Price"", ""PaymentId"", ""UserId"", ""ProjectionId"", ""IsActive"", ""DateCreated"", ""DateUpdated"", ""CreatedByUserId"", ""UpdatedByUserId"")
                                VALUES (@Id, @Price, @PaymentId, @UserId, @ProjectionId, @IsActive, @DateCreated, @DateUpdated, @CreatedByUserId, @UpdatedByUserId);";
        using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("@Id", ticket.Id);
        command.Parameters.AddWithValue("@Price", ticket.Price);
        command.Parameters.AddWithValue("@PaymentId", (object)ticket.PaymentId ?? DBNull.Value);
        command.Parameters.AddWithValue("@UserId", (object)ticket.UserId ?? DBNull.Value);
        command.Parameters.AddWithValue("@ProjectionId", (object)ticket.ProjectionId ?? DBNull.Value);
        command.Parameters.AddWithValue("@IsActive", ticket.IsActive);
        command.Parameters.AddWithValue("@DateCreated", ticket.DateCreated);
        command.Parameters.AddWithValue("@DateUpdated", ticket.DateUpdated);
        command.Parameters.AddWithValue("@CreatedByUserId", ticket.CreatedByUserId);
        command.Parameters.AddWithValue("@UpdatedByUserId", ticket.UpdatedByUserId);
        await command.ExecuteNonQueryAsync();
    }
    
    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        var tickets = new List<Ticket>();

        await using var connection = new NpgsqlConnection(_connectionString);
        var commandText = "SELECT * FROM \"Ticket\";";

        await using var command = new NpgsqlCommand(commandText, connection);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
            
        while (await reader.ReadAsync())
        {
            var ticket = new Ticket
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                PaymentId = reader.GetGuid(reader.GetOrdinal("PaymentId")),
                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                ProjectionId = reader.GetGuid(reader.GetOrdinal("ProjectionId")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                CreatedByUserId = reader.GetGuid(reader.GetOrdinal("CreatedByUserId")),
                UpdatedByUserId = reader.GetGuid(reader.GetOrdinal("UpdatedByUserId"))
                
            };
            tickets.Add(ticket);
        }

        return tickets;
        
    }
    
    public async Task<Ticket> GetTicketByIdAsync(Guid id)
    {
        Ticket ticket = null;

        await using var connection = new NpgsqlConnection(_connectionString);
        var commandText = "SELECT * FROM \"Ticket\" WHERE \"Id\" = @TicketId;";
            
        await using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("TicketId", id);
            
        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            ticket = new Ticket
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                PaymentId = reader.GetGuid(reader.GetOrdinal("PaymentId")),
                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                ProjectionId = reader.GetGuid(reader.GetOrdinal("ProjectionId")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated")),
                CreatedByUserId = reader.GetGuid(reader.GetOrdinal("CreatedByUserId")),
                UpdatedByUserId = reader.GetGuid(reader.GetOrdinal("UpdatedByUserId"))
            };
        }

        return ticket;
    }
    
    public async Task DeleteTicketAsync(Guid id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        var commandText = "DELETE FROM \"Ticket\" WHERE \"Id\" = @id;";
        using var command = new NpgsqlCommand(commandText, connection);
        command.Parameters.AddWithValue("@id", id);
        await command.ExecuteNonQueryAsync();
    }
    
}