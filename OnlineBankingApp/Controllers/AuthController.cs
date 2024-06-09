using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.Common.DTO.User;
using OnlineBankingApp.Common.Interface;


namespace OnlineBankingApp.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequest request)
        {
            var result = await _userService.RegisterUserAsync(request);

            return result ? Ok() : BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest request)
        {
            var token = await _userService.LoginUserAsync(request);

            if (token == string.Empty)
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
        }
    }
}
