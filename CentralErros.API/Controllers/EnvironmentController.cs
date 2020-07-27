using System;
using System.Collections.Generic;
using AutoMapper;
using CentralErros.Domain.Repositories;
using CentralErros.Infrastructure.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Environment = CentralErros.Domain.Models.Environment;
using Microsoft.AspNetCore.Http;

namespace CentralErros.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class EnvironmentController : ControllerBase
    {
        private readonly IEnvironmentRepository _environmentRepository;
        private readonly IMapper _mapper;
        public EnvironmentController(IEnvironmentRepository environmentRepository, IMapper mapper)
        {
            _environmentRepository = environmentRepository;
            _mapper = mapper; 
        }

        /// <summary>
        /// Retorna todos os ambientes
        /// </summary>
        /// <returns>Uma listagem dos ambientes cadastrados</returns>
        /// <response code="200">Ambientes retornados com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="404">Ambientes não encontrados</response>
        /// <response code="500">Não foi possível fazer a listagem dos ambientes</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<EnvironmentDTO>> GetEnvironments()
        {
            var service = _environmentRepository.GetAll();

            if(service.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<EnvironmentDTO>>(service));
        }

        /// <summary>
        /// Cadastra um ambiente
        /// </summary>
        /// <param environment="string"></param>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST 
        ///     {
        ///        "name": "Homologação",
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo ambiente cadastrado</returns>
        /// <response code="200">Ambiente cadastrado com sucesso</response>
        /// <response code="400">Entrada inválida (nula ou espaço em branco)</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="500">Não foi possível cadastrar um novo ambiente</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<EnvironmentDTO> CreateEnvironment(EnvironmentDTO environment)
        {
            if (String.IsNullOrWhiteSpace(environment.Name))
            {
                return BadRequest();
            }
            var entity = _mapper.Map<Environment>(environment);
            _environmentRepository.Save(entity);

            var EnvironmentDTO = _mapper.Map<EnvironmentDTO>(_environmentRepository.GetById(entity.Id));
            
            return Ok(_mapper.Map<EnvironmentDTO>(EnvironmentDTO));
        }
    }
}
