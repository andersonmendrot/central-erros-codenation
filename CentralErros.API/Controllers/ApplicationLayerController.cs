using System;
using System.Collections.Generic;
using AutoMapper;
using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;
using CentralErros.Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralErros.API.Controllers
{
    [Produces("apllication/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ApplicationLayerController : ControllerBase
    {
        private readonly IApplicationLayerRepository _applicationLayerRepository;
        private readonly IMapper _mapper;
        public ApplicationLayerController(IApplicationLayerRepository applicationLayerRepository, IMapper mapper)
        {
            _applicationLayerRepository = applicationLayerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todas as camadas de aplicação
        /// </summary>
        /// <returns>Uma listagem das camadas de aplicação cadastradas</returns>
        /// <response code="200">Camadas de aplicação retornadas com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="404">Camadas de aplicação não encontradas</response>
        /// <response code="500">Não foi possível fazer a listagem das camadas de aplicação</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<ApplicationLayerDTO>> GetApplicationLayers()
        {
            var service = _applicationLayerRepository.GetAll();

            if(service.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ApplicationLayerDTO>>(service));
        }

        /// <summary>
        /// Cadastra uma camada de aplicação
        /// </summary>
        /// <param applicationLayer="string"></param>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST 
        ///     {
        ///        "name": "Web",
        ///     }
        ///
        /// </remarks>
        /// <returns>Uma nova camada de aplicação cadastrada</returns>
        /// <response code="200">Camada de aplicação cadastrada com sucesso</response>
        /// <response code="400">Entrada inválida (nula ou espaço em branco)</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="500">Não foi possível cadastrar uma nova camada de aplicação</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<ApplicationLayerDTO> CreateApplicationLayer(ApplicationLayerDTO applicationLayer)
        {
            if (String.IsNullOrWhiteSpace(applicationLayer.Name))
            {
                return BadRequest();
            }
            var entity = _mapper.Map<ApplicationLayer>(applicationLayer);
            _applicationLayerRepository.Save(entity);

            var applicationLayerDto = _mapper.Map<ApplicationLayerDTO>(_applicationLayerRepository.GetById(entity.Id));
            
            return Ok(_mapper.Map<ApplicationLayerDTO>(applicationLayerDto));
        }
    }
}
