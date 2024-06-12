using Cinema.Model;

namespace Cinema.Repository.Common;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
}