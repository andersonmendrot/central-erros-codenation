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
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;
        public LanguageController(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper; 
        }

        /// <summary>
        /// Retorna todas as linguagens
        /// </summary>
        /// <returns>Uma listagem das linguagens cadastradas</returns>
        /// <response code="200">Linguagens retornadas com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="404">Linguagens não encontradas</response>
        /// <response code="500">Não foi possível fazer a listagem das linguagens</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<LanguageDTO>> GetLanguages()
        {
            var service = _languageRepository.GetAll();

            if(service.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<LanguageDTO>>(service));
        }

        /// <summary>
        /// Cadastra uma linguagem
        /// </summary>
        /// <param language="string"></param>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST 
        ///     {
        ///        "name": "Python",
        ///     }
        ///
        /// </remarks>
        /// <returns>Uma nova linguagem cadastrada</returns>
        /// <response code="200">Linguagem cadastrada com sucesso</response>
        /// <response code="400">Entrada inválida (nula ou espaço em branco)</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="500">Não foi possível cadastrar uma nova linguagem</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<LanguageDTO> CreateLanguage(LanguageDTO language)
        {
            if (String.IsNullOrWhiteSpace(language.Name))
            {
                return StatusCode(400);
            }
            var entity = _mapper.Map<Language>(language);
            _languageRepository.Save(entity);

            var LanguageDTO = _mapper.Map<LanguageDTO>(_languageRepository.GetById(entity.Id));
            
            return Ok(_mapper.Map<LanguageDTO>(LanguageDTO));
        }
    }
}
