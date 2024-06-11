using Cinema.Model;

namespace Cinema.Repository.Common;

public interface ITicketRepository
{
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<Ticket> GetTicketByIdAsync(Guid id);
    Task DeleteTicketAsync(Guid id);

}