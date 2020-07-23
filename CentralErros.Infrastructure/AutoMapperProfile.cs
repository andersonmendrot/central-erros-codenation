using AutoMapper;
using CentralErros.Domain.Models;
using CentralErros.Infrastructure.DTOs;
using Environment = CentralErros.Domain.Models.Environment;

namespace CentralErros.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationLayer, ApplicationLayerDTO>().ReverseMap();
            CreateMap<Environment, EnvironmentDTO>().ReverseMap();
            CreateMap<Error, ErrorDTO>().ReverseMap();
            CreateMap<Language, LanguageDTO>().ReverseMap();
            CreateMap<Level, LevelDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Level, LevelForDeleteDTO>().ReverseMap();
        }
    }
}
