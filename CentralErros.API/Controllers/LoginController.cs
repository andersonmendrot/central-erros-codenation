using CentralErros.Domain.Repositories;
using CentralErros.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CentralErros.Infrastructure.DTOs;
using AutoMapper;

namespace CentralErros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedUserRepository _loggedUserRepository;

        public LoginController(IAuthenticationRepository authenticationService, IUserRepository userRepository, IMapper mapper, ILoggedUserRepository loggedUserRepository)
        {
            _authenticationRepository = authenticationService;
            _userRepository = userRepository;
            _mapper = mapper;
            _loggedUserRepository = loggedUserRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<AuthenticationResult> PostAsync([FromBody] UserLoginDTO loginUser)
        {
            var authorization = _userRepository.Authorize(_mapper.Map<User>(loginUser));
            if (!authorization.Success)
                return Ok(authorization);

            return Ok(_authenticationRepository.Authenticate(authorization.Data));
        }
    }
}
