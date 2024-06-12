using Cinema.Model;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Cinema.Service.Common;
using DTO.UserModel;

namespace Cinema.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            _mapper = mapper;
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserPost userPost)
        {
            try
            {
                
                var user = _mapper.Map<User>(userPost);
                user.Id = Guid.NewGuid();
                user.DateCreated = DateTime.UtcNow;
                user.DateUpdated = user.DateCreated;

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }


        }
    }
}