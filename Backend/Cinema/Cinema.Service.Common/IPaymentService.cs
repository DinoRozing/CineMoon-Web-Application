using Cinema.Model;
using System.Threading.Tasks;

namespace Cinema.Service.Common
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(decimal totalPrice, Guid userId);
    }
}
