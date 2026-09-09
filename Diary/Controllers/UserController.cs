using Diary.DTO;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Controllers
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

        [HttpPost("registration")]
        public async Task<ActionResult> Registration(UserRegistrationDTO user)
        {
            await _userService.Registration(user);
            return Ok();

        }

        [HttpPost("authorization")]
        public async Task<ActionResult> Authorization(UserRegistrationDTO user)
        {
            return Ok();
        }

    }
}
