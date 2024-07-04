using AutoMapper;
using DTO;
using Cinema.Model;
using Cinema.Service.Common;
using Microsoft.AspNetCore.Mvc;
using DTO.TicketModel;
using MailKit.Net.Smtp;
using MimeKit;

namespace Cinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IPaymentService _paymentService;
        

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddTicketAsync([FromBody] CreateTicket createTicket)
        {
            try
            {
                var ticket = new Ticket
                {
                    Id = Guid.NewGuid(),
                    Price = createTicket.Price,
                    PaymentId = createTicket.PaymentId,
                    ProjectionId = createTicket.ProjectionId,
                    UserId = createTicket.UserId,
                    IsActive = true,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    CreatedByUserId = createTicket.UserId,
                    UpdatedByUserId = createTicket.UserId
                };

                var ticketId = await _ticketService.AddTicketAsync(ticket);

                return Ok(new { TicketId = ticketId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmailAsync([FromBody] EmailRequest emailRequest)
        {
            try
            {
                // SMTP postavke za Outlook
                string smtpServer = "smtp-mail.outlook.com";
                int smtpPort = 587;
                string smtpUsername = "tickets-cinemoon@outlook.com";
                string smtpPassword = "Testpassword123456";
                
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Cine Moon", smtpUsername));
                message.To.Add(new MailboxAddress(emailRequest.UserEmail, emailRequest.UserEmail));
                message.Subject = "Your Ticket Reservation Details";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $"<p>Dear customer,</p><p>Here are your reserved seats:</p><ul>{string.Join("", emailRequest.TicketIds.Select(id => $"<li>Ticket ID: {id}</li>"))}</ul>"
                };
                message.Body = bodyBuilder.ToMessageBody();
                
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpServer, smtpPort, false);
                    await client.AuthenticateAsync(smtpUsername, smtpPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}