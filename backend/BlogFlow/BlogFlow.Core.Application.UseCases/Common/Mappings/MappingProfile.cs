﻿using AutoMapper;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Core.Application.UseCases.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserResponseDTO>().ReverseMap();
            CreateMap<User, AuthUserRequestDTO>().ReverseMap();
            CreateMap<Blog, BlogDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Domain.Entities.Content, ContentDTO>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenDTO>().ReverseMap();
        }
    }
}
