using AutoMapper;
using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;
using CentralErros.Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CentralErros.API.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [ApiController]
    [Authorize("Bearer")]
    public class UserController : ControllerBase
    {
        private readonly ILoggedUserRepository _loggedUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(ILoggedUserRepository loggedUserRepository, IUserRepository userRepository, IMapper mapper)
        {
            _loggedUserRepository = loggedUserRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Cadastra um usuario
        /// </summary>
        /// <param name="string"></param>
        /// <param email="string"></param>
        /// <param password="string"></param>
        /// <param createdAt="timestamp"></param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST 
        ///     {
        ///         "name": "Anderson Mendrot",
        ///         "email": "anderson.mendrot@gmail.com",
        ///         "password": "6grN7n",
        ///         "createdAt": "2020-02-26T15:25:50"
        ///    }
        ///
        /// </remarks>
        /// <returns>Um novo usuário cadastrado</returns>
        /// <response code="200">Usuário cadastrado com sucesso</response>
        /// <response code="400">Entrada inválida</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="500">Não foi possível cadastrar um novo ambiente</response>
        [HttpPost]
        public ActionResult<UserDTO> CreateUser([FromBody] UserDTO value)
        {
            var user = _mapper.Map<User>(value);
            _userRepository.Save(user);

            var userDto = _mapper.Map<UserDTO>(_userRepository.GetById(user.Id));
            return Ok(userDto);
        }

        /// <summary>
        /// Retorna a lista de todos os usuários
        /// </summary>
        /// <response code="200">Usuários retornados com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="404">Usuários não encontrados</response>
        /// <response code="500">Não foi possível fazer a listagem dos usuários</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _userRepository.GetAll();

            if (users.Count == 0)
            {
                return NotFound();
            }

            return users;
        }

        /// <summary>
        /// Retorna um usuário por identificador
        /// </summary>
        /// <response code="200">Usuário retornado com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="500">Não foi possível retornar o usuário com o identificador informado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Retorna o usuário logado
        /// </summary>
        /// <response code="200">Usuário logado retornado com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="500">Não foi possível retornar o usuário logado</response>
        [HttpGet("logged")]
        public ActionResult<BaseResult<User>> GetLogged()
        {
            var user = _loggedUserRepository.GetLoggedUser();
            return Ok(user);
        }
    }
}