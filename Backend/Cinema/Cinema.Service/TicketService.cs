using Cinema.Model;
using Cinema.Repository.Common;
using Cinema.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Service
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IPaymentService _paymentService;

        public TicketService(ITicketRepository ticketRepository, IPaymentService paymentService)
        {
            _ticketRepository = ticketRepository;
            _paymentService = paymentService;
        }

        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            var payment = await _paymentService.CreatePaymentAsync(ticket.Price, ticket.UserId);
            ticket.PaymentId = payment.Id;

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
}
