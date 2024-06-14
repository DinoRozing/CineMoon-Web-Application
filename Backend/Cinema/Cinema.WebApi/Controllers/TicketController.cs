using AutoMapper;
using DTO;
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
        private readonly IMapper _mapper;
        public TicketController(ITicketService ticketService, IMapper mapper)
        {
            this._ticketService = ticketService;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTicketAsync([FromBody] TicketRest ticketRest)
        {
            try
            {
                var ticket = _mapper.Map<Ticket>(ticketRest);
                var createdTicket = await _ticketService.CreateTicketAsync(ticket);
                var createdTicketRest = _mapper.Map<TicketRest>(createdTicket);
                return Ok(createdTicketRest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllTicketsAsync()
        {
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                var ticketRest = _mapper.Map<IEnumerable<TicketRest>>(tickets);
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
                var ticketRest = _mapper.Map<TicketRest>(ticket);
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