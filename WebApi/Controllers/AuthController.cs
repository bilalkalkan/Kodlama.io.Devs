using Application.Features.Auths.Commands.LoginCommand;
using Application.Features.Auths.Commands.RegisterCommand;
using Core.Security.Dtos;
using Core.Security.JWT;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            AccessToken accessToken = await Mediator.Send(registerCommand);

            return Created("", accessToken);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            AccessToken accessToken = await Mediator.Send(loginCommand);

            return Created("", accessToken);
        }
    }
}