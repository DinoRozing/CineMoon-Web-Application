using Cinema.Model;
using Cinema.Repository.Common;
using Cinema.Service.Common;

namespace Cinema.Service;

public class TicketService: ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    public TicketService(ITicketRepository ticketRepository)
    {
        this._ticketRepository = ticketRepository;
    }
    public async Task<Ticket> CreateTicketAsync(Ticket ticket)
    {
        ticket.Id = Guid.NewGuid(); 
        ticket.DateCreated = DateTime.UtcNow;
        ticket.DateUpdated = DateTime.UtcNow;
        await _ticketRepository.CreateTicketAsync(ticket);
        return ticket;
    }
    
    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        return await _ticketRepository.GetAllTicketsAsync();
    }
    
    public async Task<Ticket> GetTicketByIdAsync(Guid id)
    {
        return await _ticketRepository.GetTicketByIdAsync(id);
    }
    
    public async Task DeleteTicketAsync(Guid id)
    {
        await _ticketRepository.DeleteTicketAsync(id);
    }
    
}