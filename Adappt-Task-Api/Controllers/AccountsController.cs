using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.DTO;
using Api.Services;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private IAuthService _authService;
        public AccountsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(EmploeeDto emploeeDto)
        {
            var token = await _authService.RegisterAsync(emploeeDto);
            if (token == null)
                return BadRequest("User already exists");

            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(TokenRequestDto tokenRequestDto)
        {
            var token = await _authService.GetTokenAsync(tokenRequestDto);
            if (token == null)
                return BadRequest();

            return Ok(token);
        }

    }
}
