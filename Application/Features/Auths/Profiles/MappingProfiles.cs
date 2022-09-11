using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auths.Commands.LoginCommand;
using Application.Features.Auths.Commands.RegisterCommand;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.Auths.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserForRegisterDto>().ReverseMap();
            CreateMap<User, RegisterCommand>().ReverseMap();
            CreateMap<AccessToken, UserForRegisterDto>().ReverseMap();

            CreateMap<User, UserForLoginDto>().ReverseMap();
            CreateMap<User, LoginCommand>().ReverseMap();
            CreateMap<AccessToken, UserForLoginDto>().ReverseMap();
        }
    }
}