using System;
using System.Threading.Tasks;
using Cinema.Model;
using Cinema.Repository.Common;
using Npgsql;

namespace Cinema.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreatePaymentAsync(Payment payment)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var commandText = @"
                INSERT INTO ""Payment"" (""Id"", ""TotalPrice"", ""IsActive"", ""DateCreated"", ""DateUpdated"", ""CreatedByUserId"", ""UpdatedByUserId"")
                VALUES (@Id, @TotalPrice, @IsActive, @DateCreated, @DateUpdated, @CreatedByUserId, @UpdatedByUserId);";

            using var command = new NpgsqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@Id", payment.Id);
            command.Parameters.AddWithValue("@TotalPrice", payment.TotalPrice);
            command.Parameters.AddWithValue("@IsActive", payment.IsActive);
            command.Parameters.AddWithValue("@DateCreated", payment.DateCreated);
            command.Parameters.AddWithValue("@DateUpdated", payment.DateUpdated);
            command.Parameters.AddWithValue("@CreatedByUserId", payment.CreatedByUserId);
            command.Parameters.AddWithValue("@UpdatedByUserId", payment.UpdatedByUserId);

            await command.ExecuteNonQueryAsync();
        }
    }
}
