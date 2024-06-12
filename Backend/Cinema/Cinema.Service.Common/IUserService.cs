using Cinema.Model;

namespace Cinema.Service.Common;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
}