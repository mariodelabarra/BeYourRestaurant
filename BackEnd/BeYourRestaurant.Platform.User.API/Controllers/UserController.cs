using BeYourRestaurant.Platform.User.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int userId)
        {
            var user = await _userService.ReadById(userId);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Domain.User user)
        {
            var id = await _userService.InsertAsync(user);

            return Ok(id);
        }
    }
}
