using CentralErros.Domain.Repositories;
using CentralErros.Domain.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IAuthorizationService = CentralErros.Domain.Repositories.IAuthorizationService;
using CentralErros.Domain.Models;

namespace CentralErros.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    //[Authorize("Bearer")]
    public sealed class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizationService _authorizationService;

        public LoginController(
            IAuthenticationService authenticationService,
            IAuthorizationService authorizationService)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromBody] LoginUser loginUser)
        {
            var authorization = await _authorizationService.AuthorizeAsync(loginUser);
            if (!authorization.Success)
            {
                return Ok(authorization);
            }

            var authentication = _authenticationService.Authenticate(authorization.Data);
            if (!authentication.Success)
            {
                return Ok(authentication);
            }

            return Ok(authentication);
        }
    }
}
