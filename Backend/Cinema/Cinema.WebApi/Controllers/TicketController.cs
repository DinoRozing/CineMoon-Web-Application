using Cinema.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;
        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllTicketsAsync()
        {
            var tickets = await ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketByIdAsync(Guid id)
        {
            var ticket = await ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound(); 
            }
            return Ok(ticket); 
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            try
            {
                await ticketService.DeleteTicketAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // 500 Internal Server Error
            }
        }
        
        
        
        
        
    }
    
}
