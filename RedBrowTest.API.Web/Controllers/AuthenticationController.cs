using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedBrowTest.Core.Application.Constants;
using RedBrowTest.Core.Application.Contracts.Identity;
using RedBrowTest.Core.Application.Models.Identity;
using System.Security.Claims;

namespace RedBrowTest.API.Web.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("Login", Name = "Login")]
        [ProducesResponseType(typeof(AuthenticatedUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var loginResponse = await authenticationService.Login(loginRequest);
            if (loginResponse == null)
            {
                return Unauthorized();
            }

            return Ok(loginResponse);
        }

        [HttpPost("Refresh", Name = "Refresh")]
        [ProducesResponseType(typeof(AuthenticatedUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            var refreshResponse = await authenticationService.Refresh(refreshRequest);
            if (refreshResponse == null)
            {
                return Unauthorized();
            }

            return Ok(refreshResponse);
        }

        [Authorize]
        [HttpPost("Logout", Name = "Logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout()
        {
            string IdUsuario = HttpContext.User.FindFirstValue(CustomClaimTypes.IdUser)!;
            await authenticationService.Logout(IdUsuario);

            return NoContent();
        }
    }
}
