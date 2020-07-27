using System;
using System.Collections.Generic;
using AutoMapper;
using CentralErros.Domain.Models;
using CentralErros.Domain.Repositories;
using CentralErros.Infrastructure.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CentralErros.API.Controllers
{
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

        [HttpGet]
        public ActionResult<IEnumerable<LevelDTO>> GetLevels()
        {
            var service = _levelRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<LevelDTO>>(service));
        }

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

        [HttpDelete]
        public ActionResult<LevelDTO> RemoveLevel(int levelId)
        {
            //se houver algum registro de erro com o level que se deseja remover
            //entao deve-se retornar um codigo de erro
            if (_levelRepository.HasErrorsWithLevelId(levelId))
            {
                return StatusCode(404); 
            }

            if(_levelRepository.GetById(levelId) == null)
            {
                return StatusCode(404);
            }

            return Ok(_mapper.Map<ErrorDTO>(_levelRepository.Remove(levelId)));
        }
    }
}
