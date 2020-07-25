using System;
using System.Collections.Generic;
using AutoMapper;
using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;
using CentralErros.Infrastructure.DTOs;
using Microsoft.AspNetCore.Mvc;
using CentralErros.Infrastructure.Repositories;
using System.Linq;

namespace CentralErros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationLayerController : ControllerBase
    {
        private readonly IApplicationLayerRepository _applicationLayerRepository;
        private readonly IMapper _mapper;
        public ApplicationLayerController(IApplicationLayerRepository applicationLayerRepository, IMapper mapper)
        {
            _applicationLayerRepository = applicationLayerRepository;
            _mapper = mapper; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<ApplicationLayerDTO>> GetApplicationLayers()
        {
            var service = _applicationLayerRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<ApplicationLayerDTO>>(service));
        }

        [HttpPost]
        public ActionResult<ApplicationLayerDTO> CreateApplicationLayer(ApplicationLayerDTO applicationLayer)
        {
            if (String.IsNullOrWhiteSpace(applicationLayer.Name))
            {
                return StatusCode(400);
            }
            var entity = _mapper.Map<ApplicationLayer>(applicationLayer);
            _applicationLayerRepository.Save(entity);

            var applicationLayerDto = _mapper.Map<ApplicationLayerDTO>(_applicationLayerRepository.GetById(entity.Id));
            
            return Ok(_mapper.Map<ApplicationLayerDTO>(applicationLayerDto));
        }

        /*[HttpDelete]
        public ActionResult<ApplicationLayerDTO> RemoveApplicationLayer(int applicationLayerId)
        {
            //se houver algum registro de erro com a camada de aplicacao que se deseja remover
            //entao deve-se retornar um codigo de erro
            if (_applicationLayerRepository.HasErrorsWithApplicationLayerId(applicationLayerId))
            {
                return StatusCode(404); 
            }

            if(_applicationLayerRepository.GetById(applicationLayerId) == null)
            {
                return StatusCode(404);
            }

            return Ok(_mapper.Map<ApplicationLayerDTO>(_applicationLayerRepository.Remove(applicationLayerId)));
        }*/
    }
}
