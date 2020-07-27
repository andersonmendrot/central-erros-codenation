using CentralErros.Domain.Repositories;
using CentralErros.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CentralErros.Infrastructure.DTOs;
using AutoMapper;

namespace CentralErros.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginController(IAuthenticationRepository authenticationService, IUserRepository userRepository, IMapper mapper)
        {
            _authenticationRepository = authenticationService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Realiza login (autenticação e autorização)
        /// </summary>
        /// <returns>Resultado de autenticação</returns>
        /// <response code="200">Login realizado</response>
        /// <response code="500">Não foi possível fazer login</response>
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
