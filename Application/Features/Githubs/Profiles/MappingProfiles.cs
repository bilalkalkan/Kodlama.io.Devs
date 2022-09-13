using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Githubs.Commands.CreateGithub;
using Application.Features.Githubs.Commands.DeleteGithub;
using Application.Features.Githubs.Commands.UpdateGithub;
using Application.Features.Githubs.Dtos;
using Application.Features.Githubs.Models;
using Application.Features.Githubs.Queries.GetByIdGithub;
using Application.Features.Githubs.Queries.GetListGithub;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Githubs.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Github, CreateGithubDto>().ReverseMap();
            CreateMap<Github, CreateGithubCommand>().ReverseMap();
            CreateMap<Github, DeleteGithubDto>().ReverseMap();
            CreateMap<Github, DeleteGithubCommand>().ReverseMap();
            CreateMap<Github, UpdateGithubDto>().ReverseMap();
            CreateMap<Github, UpdateGithubCommand>().ReverseMap();
            CreateMap<IPaginate<Github>, GithubListModel>().ReverseMap();
            CreateMap<Github, GithubListDto>().ForMember(g => g.UserFullName,
                opt => opt.MapFrom(g => g.User.FirstName + " " + g.User.LastName))
                .ReverseMap();

            CreateMap<Github, GetByIdGithubDto>().ForMember(g => g.UserFullName,
                    opt => opt.MapFrom(g => g.User.FirstName + " " + g.User.LastName))
                .ReverseMap();
            CreateMap<Github, GetByIdGithubQuery>().ReverseMap();
        }
    }
}