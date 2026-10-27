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
        private readonly IEmailService _emailService;

        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost("registration")]
        public async Task<ActionResult> Registration(UserRegistrationDTO user)
        {
            var result = await _userService.Registration(user);
            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.Error});
            }
            return Ok();

        }

        [HttpPost("authorization")]
        public async Task<ActionResult> Authorization(AuthorizationDTO authUser)
        {
           var result = await _userService.Authorization(authUser);
            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.Error});
            }
            return Ok();
        }

        //[HttpPost("send_email")]
        //public async Task<ActionResult> SendEmail(string email)
        //{
        //    var result = await _emailService.SendEmailMessage(email);


        //    return Ok();

        //}

        //[HttpPost("Generate_code")]
        //public async Task<ActionResult> GetCode()
        //{
        //    var result =  _userService.GenerateCode();


        //    return Ok(result);

        //}

        [HttpPost("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string code, string email)
        {
            var result = await _userService.ConfirmEmail(code, email);

            return Ok(result);
        }

    }
}
