using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.Common.DTO.User;
using OnlineBankingApp.Common.Interface;
using OnlineBankingApp.Service;


namespace OnlineBankingApp.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
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
            var login = await _userService.LoginUserAsync(request);

            if (!login)
            {
                return Unauthorized();
            }

            var token = _jwtService.GenerateSecurityToken(request.Username);

            return string.IsNullOrEmpty(token) ? Unauthorized() : Ok(new { Token = token });
           
        }
    }
}
