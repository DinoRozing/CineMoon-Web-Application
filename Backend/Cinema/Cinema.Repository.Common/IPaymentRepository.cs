using Cinema.Model;
using System.Threading.Tasks;

namespace Cinema.Repository.Common
{
    public interface IPaymentRepository
    {
        Task CreatePaymentAsync(Payment payment);
    }
}
