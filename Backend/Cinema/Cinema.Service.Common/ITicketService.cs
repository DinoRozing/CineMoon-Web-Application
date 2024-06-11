using Cinema.Model;

namespace Cinema.Service.Common;

public interface ITicketService
{
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<Ticket> GetTicketByIdAsync(Guid id);
    Task DeleteTicketAsync(Guid id);
}