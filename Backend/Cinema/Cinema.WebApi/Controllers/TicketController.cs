using Cinema.Model;
using Cinema.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            this._ticketService = ticketService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTicketAsync([FromBody] Ticket ticket)
        {
            try
            {
                var createdTicket = await _ticketService.CreateTicketAsync(ticket);
                return Ok(createdTicket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // 500 Internal Server Error
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllTicketsAsync()
        {
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketByIdAsync(Guid id)
        {
            try
            {
                var ticket = await _ticketService.GetTicketByIdAsync(id);
                if (ticket == null)
                {
                    return NotFound(); 
                }
                return Ok(ticket); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            } 
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            try
            {
                await _ticketService.DeleteTicketAsync(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }
        
        
        
        
        
    }
    
}
