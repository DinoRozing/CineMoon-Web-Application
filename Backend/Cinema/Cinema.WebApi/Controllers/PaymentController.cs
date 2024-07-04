using Cinema.Model;
using Cinema.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public async Task<IActionResult> AddPaymentAsync([FromBody] CreatePayment createPayment)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                TotalPrice = createPayment.TotalPrice,
                PaymentDate = DateTime.UtcNow,
                IsActive = true,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                CreatedByUserId = createPayment.UserId,
                UpdatedByUserId = createPayment.UserId
            };
            
            var paymentId = await _paymentService.AddPaymentAsync(payment);
            return Ok(new { PaymentId = paymentId });
        }
        

    }
}