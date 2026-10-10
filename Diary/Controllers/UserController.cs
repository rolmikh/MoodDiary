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
            var result = await _userService.Registration(user);
            if (!result)
            {
                return BadRequest(new { error = "Пользователь с таким Email существует" });
            }
            return Ok();

        }

        [HttpPost("authorization")]
        public async Task<ActionResult> Authorization(AuthorizationDTO authUser)
        {
           var result = await _userService.Authorization(authUser);
            if (!result)
            {
                return BadRequest(new { error = "Ошибка авторизации" });
            }
            return Ok();
        }

    }
}
