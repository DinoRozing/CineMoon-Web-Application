using Cinema.Model;
using Cinema.Repository.Common;
using Cinema.Service.Common;

namespace Cinema.Service;

public class TicketService: ITicketService
{
    private readonly ITicketRepository ticketRepository;
    public TicketService(ITicketRepository ticketRepository)
    {
        this.ticketRepository = ticketRepository;
    }
    
    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        return await ticketRepository.GetAllTicketsAsync();
    }
    
    public async Task<Ticket> GetTicketByIdAsync(Guid id)
    {
        return await ticketRepository.GetTicketByIdAsync(id);
    }
    
    public async Task DeleteTicketAsync(Guid id)
    {
        await ticketRepository.DeleteTicketAsync(id);
    }
    
}