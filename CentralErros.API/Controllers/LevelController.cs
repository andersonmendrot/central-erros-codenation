using System;
using System.Collections.Generic;
using AutoMapper;
using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;
using CentralErros.Infrastructure.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace CentralErros.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class LevelController : ControllerBase
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IMapper _mapper;
        public LevelController(ILevelRepository levelRepository, IMapper mapper)
        {
            _levelRepository = levelRepository;
            _mapper = mapper; 
        }

        /// <summary>
        /// Retorna todos os níveis
        /// </summary>
        /// <returns>Uma listagem dos níveis cadastrados</returns>
        /// <response code="200">Níveis retornados com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="404">Níveis não encontrados</response>
        /// <response code="500">Não foi possível fazer a listagem dos níveis</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<LevelDTO>> GetLevels()
        {
            var service = _levelRepository.GetAll();

            if(service.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<LevelDTO>>(service));
        }

        /// <summary>
        /// Cadastra um nível
        /// </summary>
        /// <param level="string"></param>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST 
        ///     {
        ///        "name": "Trace",
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo nível cadastrado</returns>
        /// <response code="200">Nível cadastrado com sucesso</response>
        /// <response code="400">Entrada inválida (nula ou espaço em branco)</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="500">Não foi possível cadastrar um novo nível</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<LevelDTO> CreateLevel(LevelDTO level)
        {
            if (String.IsNullOrWhiteSpace(level.Name))
            {
                return StatusCode(400);
            }
            var entity = _mapper.Map<Level>(level);
            _levelRepository.Save(entity);

            var levelDto = _mapper.Map<LevelDTO>(_levelRepository.GetById(entity.Id));
            
            return Ok(_mapper.Map<LevelDTO>(levelDto));
        }
    }
}
