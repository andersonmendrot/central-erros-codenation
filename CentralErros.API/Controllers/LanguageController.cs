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
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;
        public LanguageController(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<LanguageDTO>> Getlanguages()
        {
            var service = _languageRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<LanguageDTO>>(service));
        }

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

        /*[HttpDelete]
        public ActionResult<LanguageDTO> RemoveLanguage(int languageId)
        {
            //se houver algum registro de erro com o language que se deseja remover
            //entao deve-se retornar um codigo de erro
            if (_languageRepository.HasErrorsWithLanguageId(languageId))
            {
                return StatusCode(404); 
            }

            if(_languageRepository.GetById(languageId) == null)
            {
                return StatusCode(404);
            }

            return Ok(_mapper.Map<LanguageDTO>(_languageRepository.Remove(languageId)));
        }*/
    }
}
