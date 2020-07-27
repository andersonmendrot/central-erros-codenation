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
    public class ErrorController : ControllerBase
    {
        private readonly IErrorRepository _errorRepository;
        private readonly IMapper _mapper;

        public ErrorController(IErrorRepository errorRepository, IMapper mapper)
        {
            _errorRepository = errorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Cadastra um erro
        /// </summary>
        /// <returns>Um novo erro cadastrado</returns>
        /// <response code="200">Erro cadastrado com sucesso</response>
        /// <response code="400">Entrada inválida</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="500">Não foi possível cadastrar um novo erro</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<ErrorDTO> CreateError([FromBody] ErrorDTO value)
        {
            var error = _mapper.Map<Error>(value);
            _errorRepository.Save(error);

            var errorDto = _mapper.Map<ErrorDTO>(_errorRepository.GetById(error.Id));
            return Ok(errorDto);
        }

        /// <summary>
        /// Retorna um erro por identificador
        /// </summary>
        /// <response code="200">Erro retornado com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="404">Erro não encontrado</response>
        /// <response code="500">Não foi possível o erro com o identificador informado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ErrorDTO> GetErrorById(int id)
        {
            var error = _mapper.Map<ErrorDTO>(_errorRepository.GetById(id));

            if(error == null)
            {
                return NotFound();
            }

            return Ok(error);
        }

        /// <summary>
        /// Remove um erro por identificador
        /// </summary>
        /// <response code="200">Erro removido com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="404">Erro não encontrado</response>
        /// <response code="500">Não foi possível remover o erro com identificador informado</response>
        [HttpDelete("{id}")]
        public ActionResult<ErrorDTO> RemoveErrorById(int id)
        {
            var error = _errorRepository.GetById(id);

            if(error == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ErrorDTO>(_errorRepository.Remove(error)));
        }

        /// <summary>
        /// Altera status do erro
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT 
        ///     {
        ///        "id": "2",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Status de erro alterado com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="404">Erro não encontrado</response>
        /// <response code="500">Não foi possível alterar o status do erro</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}/status")]
        public ActionResult<ErrorDTO> ChangeStatusById(int id)
        {
            var error = _errorRepository.GetById(id);

            if(error == null)
            {
                return NotFound();
            }

            _errorRepository.ChangeStatus(error);

            return Ok(_mapper.Map<ErrorDTO>(error));
        }

        /// <summary>
        /// Retorna um erro e a sua quantidade de ocorrencias 
        /// </summary>
        /// <response code="200">Retorno de erro e sua quantidade de ocorrências realizada com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="404">Erro não encontrado</response>
        /// <response code="500">Não foi possível retornar o erro e sua quantidade de ocorrências</response>
        [HttpGet("{id}/ocurrences")]
        public ActionResult<ErrorQuantityDTO> GetErrorAndOcurrences(int id)
        {
            var errorToGet = _errorRepository.GetById(id);

            if (errorToGet == null)
            {
                return NotFound();
            }

            ErrorQuantityDTO errorQuantityDTO = _mapper.Map<ErrorQuantityDTO>(errorToGet);
            errorQuantityDTO.Ocurrences = _errorRepository.GetQuantity(errorToGet);

            return Ok(errorQuantityDTO);
        }


        /// <summary>
        /// Retorna uma lista de erros após filtros e ordenação 
        /// </summary>
        /// <response code="200">Retorno de lista de erros filtrada realizada com sucesso</response>
        /// <response code="401">Usuário sem autorização para acesso</response>
        /// <response code="422">Entrada de dados para filtro inválida</response>
        /// <response code="500">Não foi possível retornar a lista de erros filtrada</response>
        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<ErrorDTO>> GetErrors(string environmentName, int? orderField, int? searchField, string orderDirection = "Descending", string searchValue = null)
        {
            // orderField : 1 = Level, 2 = Status
            // searchField : 1 = ApplicationLayer, 2 = Language, 3 = Level, 4 = Origin, 5 = Title
            // orderDirection : "Ascending": ordena em ordem decrescente, "Descending": ordena em ordem crescente
            // searchValue : Valor a ser pesquisado

            // Caso nao haja nenhuma pesquisa, serão retornados todos os itens 
            // ordenados de forma descendente por numero de ocorrencias
            var errors = _errorRepository.GetAll();
            if (orderField == null && searchField == null && searchValue == null)
            {
                return Ok(_mapper.Map<IEnumerable<ErrorDTO>>(errors));
            }
            
            if(environmentName == null)
            {
                return StatusCode(422);
            }

            // Caso a busca por ambientes seja nula, então o ambiente informado pelo usuário é inválido
            if (_errorRepository.SearchByEnvironmentName(errors, environmentName) == null)
            {
                return StatusCode(422); 
            }

            // Filtro por ambiente 

            if (!String.IsNullOrWhiteSpace(environmentName))
            {
                errors = _errorRepository.SearchByEnvironmentName(errors, environmentName);
            }
                
            // Ordenação

            if ((orderField < 1 || orderField > 3) || 
                ((orderDirection != "Ascending" && orderDirection != "Descending")))
            {
                return StatusCode(422);
            }

            switch (orderField)
            {
                case 1:
                    errors = _errorRepository.OrderByLevel(errors, orderDirection);
                    break;
                case 2:
                    errors = _errorRepository.OrderByStatus(errors, orderDirection);
                    break;
            }

            // Pesquisa

            if ((searchField < 1 || searchField > 5) || 
                (searchField == null && !String.IsNullOrWhiteSpace(searchValue)) ||
                (searchField != null && String.IsNullOrWhiteSpace(searchValue)))
            {
                return StatusCode(422);
            }

            if(!String.IsNullOrWhiteSpace(searchValue))
            {
                switch (searchField)
                {
                    case 1:
                        errors = _errorRepository.SearchByApplicationLayerName(errors, searchValue);
                        break;
                    case 2:
                        errors = _errorRepository.SearchByLanguageName(errors, searchValue);
                        break;
                    case 3:
                        errors = _errorRepository.SearchByLevelName(errors, searchValue);
                        break;
                    case 4:
                        errors = _errorRepository.SearchByOrigin(errors, searchValue);
                        break;
                    case 5:
                        errors = _errorRepository.SearchByTitle(errors, searchValue);
                        break;
                }
            }

            var errorsDto = _mapper.Map<IEnumerable<ErrorDTO>>(errors);
            return Ok(errorsDto);
        }
    }
}