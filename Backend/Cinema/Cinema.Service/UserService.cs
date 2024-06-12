using Cinema.Model;
using Cinema.Repository.Common;
using Cinema.Service.Common;

namespace Cinema.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; 

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            
            user.Id = Guid.NewGuid();
            user.DateCreated = DateTime.UtcNow;
            user.DateUpdated = user.DateCreated;
            
            return await _userRepository.CreateUserAsync(user);
        }
    }
}