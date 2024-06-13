using Cinema.Model;

namespace Cinema.Repository.Common;

public interface IUserRepository
{
    Task<User> RegisterUserAsync(User user);
    //Task<UserLogin> LoginUserAsync(UserLogin userLogin);
    Task<User> GetUserByEmailAsync(string email);
}