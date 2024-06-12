using Cinema.Model;
using Cinema.Repository.Common;

namespace Cinema.Repository;

public class UserRepository: IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        this._connectionString = connectionString;
    }
    
    public async Task<User> CreateUserAsync(User user)
    {
        return user;
    }
    
    
    
}