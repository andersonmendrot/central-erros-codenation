using System;
using System.Collections.Generic;
using AutoMapper;
using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;
using CentralErros.Infrastructure.DTOs;
using Microsoft.AspNetCore.Mvc;
using CentralErros.Infrastructure.Repositories;
using System.Linq;
using Environment = CentralErros.Domain.Models.Environment;

namespace CentralErros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        private readonly IEnvironmentRepository _environmentRepository;
        private readonly IMapper _mapper;
        public EnvironmentController(IEnvironmentRepository environmentRepository, IMapper mapper)
        {
            _environmentRepository = environmentRepository;
            _mapper = mapper; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<EnvironmentDTO>> Getenvironments()
        {
            var service = _environmentRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<EnvironmentDTO>>(service));
        }

        [HttpPost]
        public ActionResult<EnvironmentDTO> CreateEnvironment(EnvironmentDTO environment)
        {
            if (String.IsNullOrWhiteSpace(environment.Name))
            {
                return StatusCode(400);
            }
            var entity = _mapper.Map<Environment>(environment);
            _environmentRepository.Save(entity);

            var EnvironmentDTO = _mapper.Map<EnvironmentDTO>(_environmentRepository.GetById(entity.Id));
            
            return Ok(_mapper.Map<EnvironmentDTO>(EnvironmentDTO));
        }

        /*[HttpDelete]
        public ActionResult<EnvironmentDTO> RemoveEnvironment(int environmentId)
        {
            //se houver algum registro de erro com o environment que se deseja remover
            //entao deve-se retornar um codigo de erro
            if (_environmentRepository.HasErrorsWithEnvironmentId(environmentId))
            {
                return StatusCode(404); 
            }

            if(_environmentRepository.GetById(environmentId) == null)
            {
                return StatusCode(404);
            }

            return Ok(_mapper.Map<EnvironmentDTO>(_environmentRepository.Remove(environmentId)));
        }*/
    }
}
