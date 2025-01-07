using AutoMapper;
using BlogFlow.Auth.Application.DTO;
using BlogFlow.Auth.Domain.Entities;

namespace BlogFlow.Auth.Application.UseCases.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
